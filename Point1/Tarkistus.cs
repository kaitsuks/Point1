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
            kartta = new Kartta(game);
            kartta.teeKartta();
        }

        public static Game game { get; private set; }

        public bool Check(string suunta, int x, int y){

            
            _suunta = suunta;
            _x = x;
            _y = y;
            //Console.WriteLine("kartan stringi" + kartta.p);
            if (kartta.p.Substring(_x * _y) == "X")

            return false;
            else
            return true;
        }
    }

    
}
