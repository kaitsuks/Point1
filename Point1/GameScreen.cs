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
    class GameScreen : DrawableGameComponent
    {
        //jäsenmuuttujat
        Game game1;
        new public Game Game { get { return base.Game; } }
        public SpriteBatch spriteBatch;
        public Texture2D taustakuva;
        public int naytonLeveys;
        public int naytonKorkeus;
        public SpriteFont omaFontti;

        //konstruktori
        public GameScreen(Game game) : base(game)
        {
            //spriteBatch = new SpriteBatch(GraphicsDevice);
            
        }

        public GameScreen(Game game, GraphicsDeviceManager graphicsDeviceManager) : base(game)         { }

        //protected override void LoadContent() { foreach (GameScreen screen in _screens) { screen.LoadContent(); } }

        //protected override void UnloadContent() { foreach (GameScreen screen in _screens) { screen.UnloadContent(); } }

        protected override void LoadContent()
        {
            //taustakuva = Game1.Instance.Content.Load<Texture2D>("WP_000303");

        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            naytonLeveys = GP.naytonLeveys;
            naytonKorkeus = GP.naytonKorkeus;
            //taustakuva = new Texture2D(GraphicsDevice, naytonLeveys, naytonKorkeus);
            omaFontti = Game1.Instance.Content.Load<SpriteFont>("Arial20");
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, naytonLeveys, naytonKorkeus), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
