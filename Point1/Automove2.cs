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
        public int princessX { get; private set; }
        public int princessY { get; private set; }
        BoundsCheck bc;
        CollisionCheck cc;
        int prinsessanHidastaja = 0;
        int prinsessanHidastajanraja = 10;
        Random rnd;
        Vector2 paikka;

        public Automove2(Vector2 paikka)
        {
            this.paikka = paikka;
            princessX = (int)paikka.X;
            princessY = (int)paikka.Y;
            rnd = new Random();
            bc = new BoundsCheck();
            cc = new CollisionCheck();
        }

        public Vector2 Wander2(Vector2 paikka)
        {

            this.paikka = paikka;
            princessX = (int)paikka.X;
            princessY = (int)paikka.Y;

            //testiarvot
            //princessX = 199;
            //princessY = 99;

            bc = new BoundsCheck();
            cc = new CollisionCheck();
            //prinsessanHidastaja++;
            //if (prinsessanHidastaja > prinsessanHidastajanraja)
            //{
            //    prinsessanHidastaja = 0;
            //}
            //if (prinsessanHidastaja == 0)
            {
                intPrincessNopeus = rnd.Next(1, 4);
                //int muutos = rnd.Next(1, 3);
                //if (muutos == 1) princessSuunta++;
                //if (muutos == 2) princessSuunta--;
                princessSuunta--; // testiä varten
                if (princessSuunta > 8) princessSuunta = 1;
            }


            if (princessSuunta == 1)
            { //KOILLINEN
                int bcx = princessX;
                int bcy = princessY;
                bcx += intPrincessNopeus;
                bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    princessX += intPrincessNopeus;
                    princessY -= intPrincessNopeus;
                }
            }
            if (princessSuunta == 2)
            //if (ita)
            { //ITÄ
                int bcx = princessX;
                int bcy = princessY;
                bcx += intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    princessX += intPrincessNopeus;
                    //princessY--;
                }
            }
            if (princessSuunta == 3)
            //if (kaakko)
            { //KAAKKO
                int bcx = princessX;
                int bcy = princessY;
                bcx += intPrincessNopeus;
                bcy += intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    princessX += intPrincessNopeus;
                    princessY += intPrincessNopeus;
                }
            }

            if (princessSuunta == 4)
            //if (etela)
            { //ETELÄ
                int bcx = princessX;
                int bcy = princessY;
                //bcx += intPrincessNopeus;
                bcy += intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    //princessX;
                    princessY += intPrincessNopeus;
                }
            }
            if (princessSuunta == 5)
            //if (lounas)
            { //LOUNAS
                int bcx = princessX;
                int bcy = princessY;
                bcx -= intPrincessNopeus;
                bcy += intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    princessX -= intPrincessNopeus;
                    princessY += intPrincessNopeus;
                }
            }
            if (princessSuunta == 6)
            //if (lansi)
            { //LÄNSI
                int bcx = princessX;
                int bcy = princessY;
                bcx -= intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    princessX -= intPrincessNopeus;
                    //princessY--;
                }
            }
            if (princessSuunta == 7)
            //if (luode)
            { //LUODE
                int bcx = princessX;
                int bcy = princessY;
                bcx -= intPrincessNopeus;
                bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    princessX -= intPrincessNopeus;
                    princessY -= intPrincessNopeus;
                }
            }
            if (princessSuunta == 8)
            //if (pohjoinen)
            { //POHJOINEN
                int bcx = princessX;
                int bcy = princessY;
                //bcx += intPrincessNopeus;
                bcy -= intPrincessNopeus;
                //if (bc.Check(GP.naytonLeveys, GP.naytonKorkeus, bcx, bcy))
                {
                    //princessX++;
                    princessY -= intPrincessNopeus;
                }
            }
            return new Vector2(princessX, princessY);
        }
    }
}
