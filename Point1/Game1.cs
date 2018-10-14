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
        Texture2D piste;

        Vector2 paikka; // sijainnin koordinaatit
        
        int naytonLeveys;
        int naytonKorkeus;

        private KeyboardState oldKeyboardState;

        SpriteFont omaFontti;

        Ritari sankari;
        Prinsessa sankaritar;
        
        Random rnd; //satunnaisluku

        bool collisionDetected;

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
            piste = Content.Load<Texture2D>("valkopiste");


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
            collisionDetected = false;
            sankari.Update(gameTime);
            sankaritar.Update(gameTime);
            //if(Rectangle.Intersect(sankari.rect, sankaritar.rect) !Empty) ;
            //Rectangle.Empty
            Rectangle r = new Rectangle();
            r = Rectangle.Intersect(sankari.rect, sankaritar.rect);
            //if (r != Rectangle.Empty) collisionDetected = true;
            //if (r != Rectangle.Empty) collisionDetected = true;
            if (r.Width > 1 || r.Height > 1)
            {
                collisionDetected = true;
                Console.WriteLine("r.Width = " + r.Width);
                Console.WriteLine("r.Height = " + r.Height);
            }

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
            string viesti3 = "Lahemmaksi ja kauemmaksi: Yla- ja alanuoli. Rotaatiot: E, R, T, Z, X";
            Vector2 alkupaikka3 = omaFontti.MeasureString(viesti3);
            spriteBatch.DrawString(omaFontti, viesti3, new Vector2((naytonLeveys - alkupaikka3.X) / 2, naytonKorkeus / 2 - 300 + 80), textColor); //tekstin tulostus

            ///*
            string viesti4 = "LOPETUS = ESC       ";
            Vector2 alkupaikka4 = new Vector2(20f, 20f);
            if (!collisionDetected)
            {
                
                
                spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
            }
            //*/

            if(collisionDetected)
            {
                ///*
                viesti4 = "TORMAYS HAVAITTU!";
                alkupaikka4 = new Vector2(20f, 20f);
                spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
                sankaritar.viestilaskuri--;
                if(sankaritar.viestilaskuri < 0) { sankaritar.viestilaskuri = 30; collisionDetected = false; }
                //*/
            }
            //spriteBatch.Draw(piste, sankari.rect, Color.White);
            spriteBatch.Draw(piste, new Rectangle((int) sankari.paikka.X,
                (int) sankari.paikka.Y, //80, 120),
                sankari.rect.Width, sankari.rect.Height),
                //sankari.rect.Width, // * sankari.skaala * sankari.perusskaala),
                //sankari.rect.Height), // * sankari.skaala * sankari.perusskaala)),
                Color.GreenYellow);
            //Console.WriteLine("sankari.paikka.X = " + sankari.paikka.X);
            //Console.WriteLine("sankari.paikka.Y = " + sankari.paikka.Y);
            Console.WriteLine("sankari.rect.Width = " + sankari.rect.Width);
            Console.WriteLine("sankari.rect.Height = " + sankari.rect.Height);

            spriteBatch.Draw(piste, sankaritar.rect, Color.Red);
            spriteBatch.End();

            //Draw Hahmot
            sankaritar.Draw(gameTime);
            sankari.Draw(gameTime);






            base.Draw(gameTime);
        }
    }
}
