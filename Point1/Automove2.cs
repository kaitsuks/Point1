using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Point1
{
    public class Automove2
    {
        private int intPrincessNopeus =3;
        public int princessSuunta = 8;
        public int x;
        public int y;
        public string suunta; //right, down, left, up
        public int princessX { get; private set; }
        public int princessY { get; private set; }
        BoundsCheck bc;
        CollisionCheck cc;
        public Tarkistus tarkistus; //kartan esteiden tarkistus
        int prinsessanHidastaja = 0;
        int prinsessanHidastajanraja = 10;
        Random rnd;
        Vector2 paikka;
        Vector2 vanhaPaikka;

        public Automove2(Vector2 paikka, Random rnd)
        {
            this.paikka = paikka;
            vanhaPaikka = paikka;
            princessX = (int)paikka.X;
            princessY = (int)paikka.Y;
            this.rnd = rnd;
            bc = new BoundsCheck();
            cc = new CollisionCheck();
            tarkistus = new Tarkistus();
        }

        public Vector2 Wander(Vector2 paikka, ref int princessSuunta)
        {

            this.paikka = paikka;
            vanhaPaikka = paikka;
            princessX = (int)paikka.X;
            princessY = (int)paikka.Y;
            this.princessSuunta = princessSuunta;

            //testiarvot
            //princessX = 199;
            //princessY = 99;

            bc = new BoundsCheck();
            cc = new CollisionCheck();
            prinsessanHidastaja++;
            if (prinsessanHidastaja > prinsessanHidastajanraja)
            {
                prinsessanHidastaja = 0;
            }
            if (prinsessanHidastaja == 0)
            {
                intPrincessNopeus = rnd.Next(1, 4);
                int muutos = rnd.Next(1, 3);
                if (muutos == 1) princessSuunta++;
                if (muutos == 2) princessSuunta--;
                //princessSuunta--; // testiä varten
                if (princessSuunta > 8) princessSuunta = 1;
                if (princessSuunta < 1) princessSuunta = 8;
            }


            if (princessSuunta == 1)
            { //KOILLINEN
                //int bcx = princessX;
                //int bcy = princessY;
                //bcx += intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    suunta = "right";
                    princessX += intPrincessNopeus;
                    princessY -= intPrincessNopeus;
                }
            }
            if (princessSuunta == 2)
            //if (ita)
            { //ITÄ
                //int bcx = princessX;
                //int bcy = princessY;
                //bcx += intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    suunta = "right";
                    princessX += intPrincessNopeus;
                    //princessY--;
                }
            }
            if (princessSuunta == 3)
            //if (kaakko)
            { //KAAKKO
                //int bcx = princessX;
                //int bcy = princessY;
                //bcx += intPrincessNopeus;
                //bcy += intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    suunta = "down";
                    princessX += intPrincessNopeus;
                    princessY += intPrincessNopeus;
                }
            }

            if (princessSuunta == 4)
            //if (etela)
            { //ETELÄ
                //int bcx = princessX;
                //int bcy = princessY;
                ////bcx += intPrincessNopeus;
                //bcy += intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    //princessX;
                    suunta = "down";
                    princessY += intPrincessNopeus;
                }
            }
            if (princessSuunta == 5)
            //if (lounas)
            { //LOUNAS
                //int bcx = princessX;
                //int bcy = princessY;
                //bcx -= intPrincessNopeus;
                //bcy += intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    suunta = "down";
                    princessX -= intPrincessNopeus;
                    princessY += intPrincessNopeus;
                }
            }
            if (princessSuunta == 6)
            //if (lansi)
            { //LÄNSI
                //int bcx = princessX;
                //int bcy = princessY;
                //bcx -= intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    suunta = "left";
                    princessX -= intPrincessNopeus;
                    //princessY--;
                }
            }
            if (princessSuunta == 7)
            //if (luode)
            { //LUODE
                //int bcx = princessX;
                //int bcy = princessY;
                //bcx -= intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    suunta = "up";
                    princessX -= intPrincessNopeus;
                    princessY -= intPrincessNopeus;
                }
            }
            if (princessSuunta == 8)
            //if (pohjoinen)
            { //POHJOINEN
                //int bcx = princessX;
                //int bcy = princessY;
                ////bcx += intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    //princessX++;
                    suunta = "up";
                    princessY -= intPrincessNopeus;
                }
            }
            x = (int)princessX / 30;
            y = (int)princessY / 30;

            //tarkistetaan näytön rajat, kartan esteet ja "liikkuvat" kohteet törmyksen varalta
            // if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, (int) princessX, (int) princessY) && !tarkistus.CheckObstacles(suunta, x, y) && !cc.Check((int)princessX, (int)princessY, (int) GP.sankaripaikka.X, (int)GP.sankaripaikka.Y))

            //tarkistetaan näytön rajat ja kiinteät esteet kartalta
            //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, (int)princessX, (int)princessY) && !tarkistus.CheckObstacles(suunta, x, y))

            //vain näytön rajojen tarkistus
            // if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, (int)princessX, (int)princessY))

            Vector2 uusi = CheckRuutu();
            return uusi;

        }

public Vector2 CheckRuutu()
        {
            int bcx = (int)paikka.X;
            int bcy = (int)paikka.Y;
            if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy) &&
            tarkistus.CheckObstacles(suunta, x, y))

                //palautetaan uusi sijainti jos Check/it menneet läpi
                return new Vector2(princessX, princessY);
            else
            // tai muuten palautetaan vanha sijainti
            {
                //Console.WriteLine("Ei voi siirtyä! ");
                princessSuunta+=4; if (princessSuunta > 8) princessSuunta = 1;
                if (princessSuunta< 1) princessSuunta = 8;
                return vanhaPaikka;
                //return new Vector2(princessX, princessY);
            }
}

    }
}
