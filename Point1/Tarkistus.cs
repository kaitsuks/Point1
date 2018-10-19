using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Point1;

namespace Point1
{
    class Tarkistus
    {
        public string _suunta;
        public Kartta kartta;
        //public Vector2 _paikka;
        public int _x;
        public int _y;

        public Tarkistus()
        {
           // kartta = new Kartta(game);
           // kartta.teeKartta();
        }

        public static Game game { get; private set; }

        public string Check(string suunta, int x, int y){

            
            _suunta = suunta;
            _x = x;
            _y = y;
            if ((_y * 30 + _x) < kartta.p.Length)
            {
                string tulos = kartta.p.Substring(_y * 30 + _x, 1);
                Console.WriteLine("kartan stringi on  " + kartta.p.Substring(_y * 30 + _x, 1));
                if (kartta.p2.Substring(_y * 30 + _x, 1) == "A") tulos = "aarre";
                return tulos;
            }
            else
                return "Ulkona kartalta";

            //if (kartta.p.Substring(_x * _y, 1) == "X")
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
            string uusi; // = kartta.p;
            //uusi = kartta.p.Insert(_y * 30 + _x, "A");
            //kartta.p.
            //String toka = 
            StringBuilder toka = new StringBuilder(kartta.p);
            toka.Replace("O", "A", _y * 30 + _x, 1);
            uusi = toka.ToString();
            return uusi;

        }

        public bool CheckObstacles(string suunta, int x, int y)
        {
            if (suunta == "right" && kartta.p.Substring((y * 30) + x + 1, 1) == "X")
                return false;
            else
            if (suunta == "left" && kartta.p.Substring((y * 30) + x - 1, 1) == "X")
                return false;
            else
                if (suunta == "up" && kartta.p.Substring((y -1) * 30  + x, 1) == "X")
                return false;
            else
                if (suunta == "down" && kartta.p.Substring((y +1) * 30 + x, 1) == "X")
                return false;
            else
                return true;
        }
    }

    
}
