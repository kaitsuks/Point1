using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Point1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region luokkamuuttujat

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D prinsessa; //spritesheet, 4 kuvaa a 80x120
        Texture2D ritari_anim; //spritesheet, 4 kuvaa a 80x120
        Texture2D taustakuva;

        Vector2 paikka; // sijainnin koordinaatit
        
        int naytonLeveys;
        int naytonKorkeus;

        private KeyboardState oldKeyboardState;

        SpriteFont omaFontti;

        Ritari sankari;
        Prinsessa sankaritar;
        
        Random rnd; //satunnaisluku
        
        #endregion


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
            paikka = new Vector2(300f, 200f);
            sankari = new Ritari(this); // drawable game component voidaan luoda vasta tässä
            sankaritar = new Prinsessa(this);

            naytonLeveys = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            naytonKorkeus = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //aseta peli-ikkunalle koko tarvittaessa

            if (naytonLeveys >= 1200)
            {
                naytonLeveys = 1200;
            }
            if (naytonKorkeus >= 800)
            {
                naytonKorkeus = 800;
            }
            graphics.PreferredBackBufferWidth = naytonLeveys;
            graphics.PreferredBackBufferHeight = naytonKorkeus;
            graphics.ApplyChanges();

            rnd = new Random(); //luodaan satunnaisluku

            sankari.InitRitari();
            sankaritar.InitPrinsessa();

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

            // TODO: use this.Content to load your game content here

            prinsessa = Content.Load<Texture2D>("Prinsessa_animaatio1");
            ritari_anim = Content.Load<Texture2D>("RitariKavely1_4kuvaa");
            taustakuva = Content.Load<Texture2D>("tausta");
            //lataa fontti
            omaFontti = Content.Load<SpriteFont>("Arial20");

            LoadGraphics();
        }

        public void LoadGraphics()
        {
            sankaritar.prinsessa = this.prinsessa;
            sankari.ritari_anim = this.ritari_anim;
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

            //käsittelee näppäimistön tilan 
            KeyboardState newKeyboardState = Keyboard.GetState();

            sankari.Update(gameTime);
            sankaritar.Update(gameTime);

            oldKeyboardState = newKeyboardState;   //tallenna vanha tila, jos tarpeen 

            //muuta logiikkaa

            int kortti = rnd.Next(52);     // tilapäismuuttuja kortti saa arvon välillä 0 - 51
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Taustan väri 
            GraphicsDevice.Clear(Color.Black);

            // Draw background
            spriteBatch.Begin();

            spriteBatch.Draw(taustakuva, new Rectangle(0,0,1200, 800),  Color.White);

            spriteBatch.End();

            //Draw Hahmot
            sankaritar.Draw(gameTime);
            sankari.Draw(gameTime);

            // Draw texts
            spriteBatch.Begin();
            Color textColor = new Color(Color.OrangeRed, 1f);

            string viesti0 = "Pelasta Prinsessa Mary!";
            Vector2 alkupaikka0 = omaFontti.MeasureString(viesti0);
            //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
            //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
            spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X -300) / 2, naytonKorkeus / 2 - 400), Color.AliceBlue, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);


            string viesti = "Liikkuminen sivulle: nuolet vasemmalle ja oikealle";
            Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2 -300), textColor); //tekstin tulostus
            ///*
            string viesti2 = "Suunnanmuutos: B ja F, nopeus M ja L";
            Vector2 alkupaikka2 = omaFontti.MeasureString(viesti2);
            spriteBatch.DrawString(omaFontti, viesti2, new Vector2((naytonLeveys - alkupaikka2.X) / 2, naytonKorkeus / 2 -300 + 40), textColor); //tekstin tulostus
            //*/
            ///*
            string viesti3 = "Lahemmaksi ja kauemmaksi: Yla- ja alanuoli. Rotaatiot: E, R, T, Z, X";
            Vector2 alkupaikka3 = omaFontti.MeasureString(viesti3);
            spriteBatch.DrawString(omaFontti, viesti3, new Vector2((naytonLeveys - alkupaikka3.X) / 2, naytonKorkeus / 2 -300 + 80), textColor); //tekstin tulostus
            //*/
            spriteBatch.End();






            base.Draw(gameTime);
        }
    }
}
