using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Point1
{
    class VesiPala : Pala
    {
        //GraphicsDevice graphicsDevice = Game1.Instance.GraphicsDevice;
        new Texture2D pic = new Texture2D(Game1.Instance.GraphicsDevice, 30, 30);
        public VesiPala(Game game) : base(game)
        {
            base.pic = iVesi;
            laji = "vesi";
        }
    }
}
