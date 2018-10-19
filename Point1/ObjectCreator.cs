using Microsoft.Xna.Framework;
using Point1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point1
{
    class ObjectCreator // : DrawableGameComponent
    {
        public Prinsessa ps;
        public List<Prinsessa> psList = new List<Prinsessa>();
        public Game game;
        public int princessCount;
        Random rnd;

        //public ObjectCreator(Game game) : base(game)
        public ObjectCreator()
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            game = new Game();
            rnd = new Random();
        }

        public void LisaaPrinsessa()
        {
            ps = new Prinsessa(game);
            ps.paikka = new Vector2(rnd.Next(100, 500), rnd.Next(100, 500));
            psList.Add(ps);
            princessCount++;
            Console.WriteLine("Lisätty prinsessa nr. " + princessCount);


        }

        public Prinsessa LuoPrinsessa()
        {
            return new Prinsessa(game);
        }
    }

   
}
