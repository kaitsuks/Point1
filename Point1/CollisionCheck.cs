using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point1
{
    class CollisionCheck
    {

        public bool Check(int x1, int y1, int x2, int y2)
        {
            if(x2 > x1 && x2 < (x1+80) && y2 > y1 && y2 < (y1+120))
            return true;
            else
            return false;
        }
    }
}
