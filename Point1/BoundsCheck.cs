using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point1
{
    public class BoundsCheck
    {

        public bool Check(int leveys, int korkeus, int paikkax, int paikkay)
        {
            if(paikkax < leveys -100 && paikkax > 0 && paikkay < korkeus - 100 && paikkay > 0)
            return true;
            else
            return false;
        }
    }

    
}
