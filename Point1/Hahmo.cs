using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Point1
{
    class Hahmo : DrawableGameComponent

    {
        SpriteBatch spriteBatch;

        int intNopeus = 10;
        int intTerveys = 100;
        bool elossa = true;

        public Hahmo(Game game) : base(game)
        {
        }

        
    }
}
