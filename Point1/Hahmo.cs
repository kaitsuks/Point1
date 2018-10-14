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

        public int intNopeus = 10;
        public int intTerveys = 100;
        public bool elossa = true;
        public Rectangle rect = new Rectangle(0, 0, 80, 120);
        public int viestilaskuri = 30;

        public Hahmo(Game game) : base(game)
        {
        }

        
    }
}
