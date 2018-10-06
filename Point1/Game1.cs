using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Point1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pallo;
        Vector2 paikka;
        int n = 1;
        int pallonLeveys;
        int pallonKorkeus;   
        SpriteFont omaFontti;
        int naytonLeveys;
        int naytonKorkeus;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            paikka = new Vector2(100f, 100f);
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            naytonLeveys = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            naytonKorkeus = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //aseta peli-ikkunalle koko tarvittaessa
            if (naytonLeveys >= 800)
            {
                naytonLeveys = 800;
            }
            if (naytonKorkeus >= 600)
            {
                naytonKorkeus = 600;
            }
            graphics.PreferredBackBufferWidth = naytonLeveys;
            graphics.PreferredBackBufferHeight = naytonKorkeus;
            graphics.ApplyChanges();

            // TODO: use this.Content to load your game content here
            pallo = Content.Load<Texture2D>("ad_board");
            //pallonLeveys = pallo.Bounds.Width;
            //pallonKorkeus = pallo.Bounds.Height;
            //tai
            pallonLeveys = pallo.Width;
            pallonKorkeus = pallo.Height;
            //lataa fontti
            omaFontti = Content.Load<SpriteFont>("Arial20");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            paikka.X+=n;
            if (paikka.X > 300) n = -n;
            if (paikka.X < 100) n = -n;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(pallo, paikka, Color.White);
            //spriteBatch.Draw()
            //pallo.Bounds.Width
            string viesti = "Tervehdys! "+ "leveys= " + pallonLeveys.ToString() + " korkeus= " + pallonLeveys.ToString();
            Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
