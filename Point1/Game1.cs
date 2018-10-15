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
        CollisionChecker cs;
        GameScreen gs1;
        OhjeScreen ohjeruutu;

        Texture2D prinsessa; //spritesheet, 4 kuvaa a 80x120
        Texture2D ritari_anim; //spritesheet, 4 kuvaa a 80x120
        Texture2D taustakuva;
        Texture2D taustakuva2;
        Texture2D piste;

        Vector2 paikka; // sijainnin koordinaatit
        
        int naytonLeveys;
        int naytonKorkeus;

        private KeyboardState oldKeyboardState;
        private MouseState curMouseState;
        private MouseState lastMouseState;
        

        bool bOhjeet = false;

        SpriteFont omaFontti;
        Color textColor;

        Ritari sankari;
        Prinsessa sankaritar;

        string viesti0, viesti, viesti2, viesti3, viesti4;
        Vector2 alkupaikka0, alkupaikka, alkupaikka2, alkupaikka3, alkupaikka4;
        
        Random rnd; //satunnaisluku

        bool collisionDetected;
        bool pixelCollision;

        Rectangle button;

        #endregion

        /*
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //cs = new CollisionChecker(this);
            cs = new CollisionChecker();

        }
        */

        public static Game1 Instance;

        public Game1()
        {     //
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //cs = new CollisionChecker(this);
            this.IsMouseVisible = true;
            cs = new CollisionChecker();
            Mouse.PlatformSetCursor(MouseCursor.Arrow);
            Instance = this;
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

            cs.Init();
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

            GP.naytonLeveys = naytonLeveys;
            GP.naytonKorkeus = naytonKorkeus;
            

            rnd = new Random(); //luodaan satunnaisluku

            sankari.InitRitari();
            sankaritar.InitPrinsessa();

            //gs1 = new GameScreen(this);
            gs1 = new GameScreen(this, graphics);
            gs1.Initialize();

            ohjeruutu = new OhjeScreen(this);
            ohjeruutu.Initialize();

            textColor = new Color(Color.OrangeRed, 1f);
            viesti4 = "LOPETUS = ESC       ";
            alkupaikka4 = new Vector2(20f, 20f);

            button = new Rectangle(880, 80, 300, 100);

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
            taustakuva2 = Content.Load<Texture2D>("WP_000207");
            //lataa fontti
            omaFontti = Content.Load<SpriteFont>("Arial20");
            piste = Content.Load<Texture2D>("valkopiste");


            LoadGraphics();
        }

        public void LoadGraphics()
        {
            sankaritar.prinsessa = this.prinsessa;
            sankari.ritari_anim = this.ritari_anim;
            gs1.Initialize();
            gs1.taustakuva = taustakuva2;
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
            bOhjeet = false;
            //haetaan hiiren tila
            curMouseState = Mouse.GetState();
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
                pixelCollision =  cs.Check(GraphicsDevice, ritari_anim, prinsessa, sankari.paikka, sankaritar.paikka, sankari.rect);

                //Console.WriteLine("r.Width = " + r.Width);
                //Console.WriteLine("r.Height = " + r.Height);
            }

            oldKeyboardState = newKeyboardState;   //tallenna vanha tila, jos tarpeen
            if (curMouseState.LeftButton == ButtonState.Pressed) // && lastMouseState.LeftButton == ButtonState.Released)
            {
                Point mousePositionPoint = curMouseState.Position;

                Rectangle mouseRect = new Rectangle(mousePositionPoint, new Point(300, 100));
                //Vector2 mousePos = mousePositionPoint.ToVector2();
                //Rectangle mouseRect = new Rectangle()
                if(mouseRect.Intersects(button))
                {

                    bOhjeet = true;
                }
            }
            //Rectangle Button = new Rectangle(880, 80, 300, 100);


            lastMouseState = curMouseState;

            //muuta logiikkaa

            int kortti = rnd.Next(52);     // tilapäismuuttuja kortti saa arvon välillä 0 - 51

            //cs.Check(this.GraphicsDevice, ritari_anim, prinsessa, sankari.paikka, sankaritar.paikka, sankari.rect);

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

            if (bOhjeet)
            {

                //spriteBatch.Draw(taustakuva2, new Rectangle(0, 0, naytonLeveys, naytonKorkeus), Color.White);
                //spriteBatch.Draw(gs1.taustakuva, new Rectangle(0, 0, naytonLeveys, naytonKorkeus), Color.White);
                gs1.Draw(gameTime);
                ohjeruutu.Draw(gameTime);
            }
            else
            {
                spriteBatch.Draw(taustakuva, new Rectangle(0, 0, 1200, 800), Color.White);
                viesti0 = "Pelasta Prinsessa Mary!";
                alkupaikka0 = omaFontti.MeasureString(viesti0);
                //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
                //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
                spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X - 300) / 2, naytonKorkeus / 2 - 400), Color.AliceBlue, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);


                //Draw Hahmot
                sankaritar.Draw(gameTime);
                sankari.Draw(gameTime);
                
            }
            spriteBatch.End();

            spriteBatch.Begin();

            if (!collisionDetected)
            {
              spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
            }
            //*/

            if(collisionDetected)
            {
                ///*
                if (collisionDetected) {
                    viesti4 = "TORMAYS HAVAITTU!";
                }
                if (pixelCollision)
                {
                    viesti4 = "PIKSELITORMAYS!";
                    spriteBatch.Draw(cs.uusi, new Vector2(400f, 400f), Color.White);
                    spriteBatch.Draw(cs.uusiGhost, new Vector2(400f, 200f), Color.White);
                }

                alkupaikka4 = new Vector2(20f, 20f);
                spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
                sankaritar.viestilaskuri--;
                if (sankaritar.viestilaskuri < 0)
                {
                    sankaritar.viestilaskuri = 30; collisionDetected = false;
                    pixelCollision = false;
                }
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
            //Console.WriteLine("sankari.rect.Width = " + sankari.rect.Width);
            //Console.WriteLine("sankari.rect.Height = " + sankari.rect.Height);

            spriteBatch.Draw(piste, sankaritar.rect, Color.Red);
            //spriteBatch.Draw(cs.uusi, new Vector2(400f,400f), Color.White);
            //spriteBatch.Draw(cs.uusiGhost, new Vector2(400f, 200f), Color.White);

            //hiiren nappula
            spriteBatch.Draw(piste, button, new Rectangle(0, 0, 1, 1), Color.Black);
            string nappulateksti  = "Ohjeet!";
            alkupaikka0 = omaFontti.MeasureString(nappulateksti);
            //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
            //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
            spriteBatch.DrawString(omaFontti, nappulateksti, new Vector2(900, 85), Color.Red, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);


            spriteBatch.End();

            //Draw Hahmot
            sankaritar.Draw(gameTime);
            sankari.Draw(gameTime);






            base.Draw(gameTime);
        }
    }
}
