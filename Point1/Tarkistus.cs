using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Point1;

namespace Point1
{
    public class Tarkistus
    {
        public string _suunta;
        public string suunta = "right";
        //public Kartta kartta;
        public KarttaData k = new KarttaData();
        //public Vector2 _paikka;
        public int _x;
        public int _y;
        public int x;
        public int y;

        public Tarkistus()
        {
           //kartta = new Kartta(game);
           // kartta.teeKartta();
        }

        public static Game game { get; private set; }

        public string Check(string suunta, int x, int y){

            
            _suunta = suunta;
            _x = x;
            _y = y;
            if ((_y * 30 + _x) < k.p.Length)
            {
                string tulos = k.p.Substring(_y * 30 + _x, 1);
                //Console.WriteLine("kartan stringi on  " + k.p.Substring(_y * 30 + _x, 1));
                if (k.p2.Substring(_y * 30 + _x, 1) == "A") tulos = "aarre";
                return tulos;
            }
            else
                return "Ulkona kartalta";

            //if (k.p.Substring(_x * _y, 1) == "X")
            /*
           return false;
           else
           return true;
           */
        }

        public string SetKuoppa(int x, int y)
        {
            _x = x;
            _y = y;
            //
            string uusi; // = k.p;
            //uusi = k.p.Insert(_y * 30 + _x, "A");
            //kartta.p.
            //String toka = 
            StringBuilder toka = new StringBuilder(k.p);
            toka.Replace("O", "A", _y * 30 + _x, 1);
            uusi = toka.ToString();
            return uusi;

        }

        public bool CheckObstacles(string suunta, int x, int y)
        {
            this.suunta = suunta;
            this.x = x;
            this.y = y;
            if (y < 1) y = 1; 
            int h = (y * 30) + x + 1;
            if (suunta == "right" && k.p.Substring(h, 1) == "X")
                return false;
            else
                h = (y * 30) + x - 1;
            if (suunta == "left" && k.p.Substring(h, 1) == "X")
                return false;
            else
                h = (y - 1) * 30 + x;
            if (suunta == "up" && k.p.Substring(h, 1) == "X")
                return false;
            else
                h = (y + 1) * 30 + x;
                if (suunta == "down" && k.p.Substring(h, 1) == "X")
                return false;
            else
                return true;
        }
    }

    
}
