using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Point1
{
   public class Pala : DrawableGameComponent
    {
        public Color vari;
        public String laji;
        public enum laatu { vesi, kivi, hiekka };
        public Texture2D iVesi;
        public Texture2D iKivi;
        public Texture2D iHiekka;
        public Texture2D pic;
        private GraphicsDevice graphicsDevice;

        public Pala(Game game) : base(game)
        {
            //pic = new Texture2D(graphicsDevice, 30, 30);

        }

        protected override void LoadContent()
        {
            
            iVesi = Game1.Instance.Content.Load<Texture2D>("tilewater");
            iKivi = Game1.Instance.Content.Load<Texture2D>("tilestone");
            iHiekka = Game1.Instance.Content.Load<Texture2D>("tilesand");
        }



    }
}
