using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        Sounder sounder;
        //Automove am;
        string tulos = "";

        Texture2D prinsessa; //spritesheet, 4 kuvaa a 80x120
        Texture2D ritari_anim; //spritesheet, 4 kuvaa a 80x120
        Texture2D taustakuva;
        Texture2D taustakuva2;
        Texture2D piste;
        ObjectCreator oc;

        Vector2 paikka; // sijainnin koordinaatit
        
        int naytonLeveys;
        int naytonKorkeus;

        private KeyboardState oldKeyboardState;
        //private MouseState newMouseState;
        private MouseState oldMouseState;
        

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
        bool crashPlayed;

        Kartta kartta;
        Tarkistus tarkistus;

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

        private bool songPlayed;

        public Game1()
        {     //
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            IsMouseVisible = true;
            cs = new CollisionChecker();
            sounder = new Sounder();
            //am = new Automove();
            //Mouse.PlatformSetCursor(MouseCursor.Arrow);
            //Mouse.GetState(this)
            //Mouse
            Mouse.PlatformSetCursor(MouseCursor.Hand);
            Instance = this;
            oc = new ObjectCreator(this);
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
            //oc.game = this;
            tarkistus = new Tarkistus();
            cs.Init();
            sounder.InitSounder();
            paikka = new Vector2(300f, 200f);
            sankari = new Ritari(this); // drawable game component voidaan luoda vasta tässä
            sankaritar = new Prinsessa(this);
            naytonLeveys = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            naytonKorkeus = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //aseta peli-ikkunalle koko tarvittaessa
            if (naytonLeveys >= 1600)
            {
                naytonLeveys = 1600;
            }
            if (naytonKorkeus >= 930)
            {
                naytonKorkeus = 930;
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
            kartta = new Kartta(this);
            kartta.Initialize();
            textColor = new Color(Color.OrangeRed, 1f);
            viesti4 = "LOPETUS = ESC       ";
            alkupaikka4 = new Vector2(20f, 20f);
            button = new Rectangle(1200, 750, 300, 100);
            tarkistus.kartta = this.kartta;
            sankari.tarkistus = this.tarkistus;

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
            //sankaritar.prinsessa = this.prinsessa;
            sankari.ritari_anim = this.ritari_anim;
            gs1.Initialize();
            //gs1.taustakuva = taustakuva2;
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
            //newMouseState = Mouse.GetState();
            MouseState newMouseState = Mouse.GetState();
            oldMouseState = newMouseState;

            if (newMouseState.LeftButton == ButtonState.Pressed) // && oldMouseState.LeftButton == ButtonState.Released)
            {
                //Console.WriteLine("Hiiren nappulaa painettu! ");
                Point mousePositionPoint = newMouseState.Position;
                Rectangle mouseRect = new Rectangle(mousePositionPoint, new Point(30, 30));
                //Vector2 mousePos = mousePositionPoint.ToVector2();
                //Rectangle mouseRect = new Rectangle()
                if (mouseRect.Intersects(button))
                {

                    bOhjeet = true;
                }
            }
                //käsittelee näppäimistön tilan 
                KeyboardState newKeyboardState = Keyboard.GetState();
            collisionDetected = false;
            pixelCollision = false;
            sankari.Update(gameTime);
            sankaritar.Liiku();
            if (sankari.tulos == "aarre")
            {
                kartta.p = tarkistus.kartta.p;
                //kartta.p = kartta.p2;
                kartta.p = tarkistus.SetKuoppa(sankari.x, sankari.y);
            }
            sankaritar.Update(gameTime);
            //if(Rectangle.Intersect(sankari.rect, sankaritar.rect) !Empty) ;
            //Rectangle.Empty
            Rectangle r = new Rectangle();
            r = Rectangle.Intersect(sankari.rect, sankaritar.rect);
            //if (r != Rectangle.Empty) collisionDetected = true;
            if (r != Rectangle.Empty) //collisionDetected = true;
            //if (r.Width > 1 || r.Height > 1)
            {
                collisionDetected = true;
                pixelCollision =  cs.Check(GraphicsDevice, ritari_anim, prinsessa, sankari.paikka, sankaritar.paikka, sankari.rect);
                if (pixelCollision && !crashPlayed)
                {
                    sounder.Crash(); crashPlayed = true;
                    Console.WriteLine("RÄJÄHDYSEFEKTI!");
                }
                else
                    crashPlayed = false;
                //Console.WriteLine("r.Width = " + r.Width);
                //Console.WriteLine("r.Height = " + r.Height);
            }
            if (newKeyboardState.IsKeyDown(Keys.I))
            {
                //oc.LuoPrinsessa();
                //oc.LisaaPrinsessa();
                oc.LisaaZombi();

            }

                oldKeyboardState = newKeyboardState;   //tallenna vanha tila, jos tarpeen

            
            
            //Rectangle Button = new Rectangle(880, 80, 300, 100);

            //newMouseState = Mouse.GetState();
            //oldMouseState = newMouseState;

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
            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, GP.naytonLeveys, GP.naytonKorkeus), Color.White);
            spriteBatch.End();

            spriteBatch.Begin();

            if (bOhjeet)
            {
                if (!songPlayed) { sounder.Sing(); songPlayed = true; }
                else
                    songPlayed = false;

                //spriteBatch.Draw(taustakuva2, new Rectangle(0, 0, naytonLeveys, naytonKorkeus), Color.White);
                //spriteBatch.Draw(gs1.taustakuva, new Rectangle(0, 0, naytonLeveys, naytonKorkeus), Color.White);
                //gs1.Draw(gameTime);
                ohjeruutu.Draw(gameTime);
                //kartta.Draw(gameTime);
                
            }
            else
            {
                kartta.naytaKartta();
                
                viesti0 = "Pelasta Prinsessa Mary!";
                alkupaikka0 = omaFontti.MeasureString(viesti0);
                //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
                //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
                spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2 + 270, naytonKorkeus / 2 - 400), Color.Blue, 0f, new Vector2(0, 0), 2f, SpriteEffects.None, 0f);


                //Draw Hahmot
                sankaritar.Draw(gameTime);
                sankari.Draw(gameTime);
                
            }
            spriteBatch.End();

            spriteBatch.Begin();

            //if (!collisionDetected)
            //{
            //  spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
            //}
            //*/

            if(collisionDetected) viesti4 = "TORMAYS HAVAITTU!";
            {
                ///*
                
                    
                
                if (pixelCollision)
                {
                    viesti4 = "PIKSELITORMAYS!";
                    spriteBatch.Draw(cs.uusi, new Vector2(1400f, 400f), Color.White);
                    spriteBatch.Draw(cs.uusiGhost, new Vector2(1400f, 200f), Color.White);
                    
                }
                if (collisionDetected || pixelCollision)
                {
                    alkupaikka4 = new Vector2(20f, 20f);
                    spriteBatch.DrawString(omaFontti, viesti4, alkupaikka4, textColor); //tekstin tulostus
                    sankaritar.viestilaskuri--;
                    if (sankaritar.viestilaskuri < 0)
                    {
                        sankaritar.viestilaskuri = 30; collisionDetected = false;
                        pixelCollision = false;
                        viesti4 = "LOPETUS = ESC ";
                    }
                }
                //*/
            }
            //spriteBatch.Draw(piste, sankari.rect, Color.White);
            /*
            spriteBatch.Draw(piste, new Rectangle((int) sankari.paikka.X,
                (int) sankari.paikka.Y, //80, 120),
                sankari.rect.Width, sankari.rect.Height),
                //sankari.rect.Width, // * sankari.skaala * sankari.perusskaala),
                //sankari.rect.Height), // * sankari.skaala * sankari.perusskaala)),
                Color.GreenYellow);
            */
            //Console.WriteLine("sankari.paikka.X = " + sankari.paikka.X);
            //Console.WriteLine("sankari.paikka.Y = " + sankari.paikka.Y);
            //Console.WriteLine("sankari.rect.Width = " + sankari.rect.Width);
            //Console.WriteLine("sankari.rect.Height = " + sankari.rect.Height);

            /*
            spriteBatch.Draw(piste, sankaritar.rect, Color.Red);
            //spriteBatch.Draw(cs.uusi, new Vector2(400f,400f), Color.White);
            //spriteBatch.Draw(cs.uusiGhost, new Vector2(400f, 200f), Color.White);
            */

            //hiiren nappula
            spriteBatch.Draw(piste, button, new Rectangle(0, 0, 1, 1), Color.Black);
            string nappulateksti  = "Ohjeet!";
            alkupaikka0 = omaFontti.MeasureString(nappulateksti);
            //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
            //spriteBatch.DrawString(omaFontti, viesti0, new Vector2((naytonLeveys - alkupaikka0.X) / 2, naytonKorkeus / 2 - 300), Color.Black); //tekstin tulostus
            spriteBatch.DrawString(omaFontti, nappulateksti, new Vector2(1220, 760), Color.Red, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);


            spriteBatch.End();

            //Draw Hahmot
            sankaritar.Draw(gameTime);
            sankari.Draw(gameTime);


            //List<Prinsessa> list = oc.psList;

            //foreach (Prinsessa s in list)
            //{
            //    // process
            //    s.Draw(gameTime);
            //    s.Liiku();
            //    s.Update(gameTime);
            //}
            //List<Prinsessa> list = oc.psList;

            List<Zombi> list = oc.zoList;
            foreach (Zombi s in list)
            {
                // process
                s.Draw(gameTime);
                s.Liiku();
                s.Update(gameTime);
            }
            //List<Zombi> list = oc.zoList;

            base.Draw(gameTime);
        }

        //private Action<Hahmo> Draw()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
