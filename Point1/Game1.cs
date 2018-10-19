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
        CollisionCheck cc;
        bool bCollision = false;

        Texture2D prinsessa; //spritesheet, 4 kuvaa a 80x120
        Texture2D ritari_anim; //spritesheet, 4 kuvaa a 80x120
        Texture2D taustakuva;
        Texture2D piste;

        Vector2 paikka; // sijainnin koordinaatit

        //nappulaan liittyvät muuttujat
        private MouseState curMouseState;
        private MouseState lastMouseState;
        Rectangle button; //painikkeen suorakaide
        Rectangle mouseRect;
        int paikka_x = 200; //buttonin paikka
        int paikka_y = 200; //buttonin paikka
        int leveys = 300; //buttonin mitta
        int korkeus = 100; //buttonin mitta
        bool boolNappiaPainettu = false;

        int n = 8; // kokonaislukumuuttuja sijainnin X-koordinaatin muuttamiseksi
        int m = 1; // kokonaislukumuuttuja sijainnin Y-koordinaatin muuttamiseksi

        //Non-Player Character AI
        int princessX = 100;
        int princessY = 100;
        Random rnd; //satunnaisluku
        //Random rnd2; //satunnaisluku
        int intPrincessNopeus = 1;
        int princessSuunta = 1;
        int prinsessanHidastaja = 0; // 
        int prinsessanHidastajanraja = 10; //
                                           //bool koillinen, ita, kaakko, etela, lounas, luode, pohjoinen;
                                           //bool vasen, oikea;

        BoundsCheck bc;

        int naytonLeveys;
        int naytonKorkeus;

        private KeyboardState oldKeyboardState;

        SpriteFont omaFontti;

        Ritari sankari;
        Prinsessa sankaritar;
        
        bool collisionDetected;
        bool pixelCollision;

        //Ritari sankari; // ei käytössä vielä

        int ritari_x = 0; //spritesheet-animaation muuttuv koordinaatti

        //animaaation hidastuslaskurin muuttujat
        int ritarinHidastaja;
        int ritarinHidastajaRaja = 5;

        //liikkumisen tilamuuttujat
        bool eteenpain = true; //ohjaus F-näppäin
        bool peruutus = false; // ohjaus B-näppäin
        bool liikkeella = false; //true kun vasen tai oikea nuolinäppäin on painettuna



        //rotaation kääntöpisteen arvot, ohjataan X, Z, Y ja T näppäimillä
        float xpoint = 0f;
        float ypoint = 0f;

        float rot = 0f; //asteina, ohjataan R ja E näppäimillä
        private float fn;

        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            cs = new CollisionChecker(this);

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
            bc = new BoundsCheck();
            paikka = new Vector2(300f, 200f);
            //sankari = new Ritari(this); // drawable game component voidaan luoda vasta tässä
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
            button = new Rectangle(paikka_x, paikka_y, leveys, korkeus);

            //Mouse.SetCursor(MouseCursor.Crosshair); //ei toimi
            Mouse.PlatformSetCursor(MouseCursor.Hand);
            IsMouseVisible = true;
            cc = new CollisionCheck();
            //sankari.InitRitari();
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
            //sankari.ritari_anim = this.ritari_anim;
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
            /*
           //Non-Player Character AI
       int princessX = 100;
       int princessY = 100;
       Random rnd; //satunnaisluku
       int intPrincessNopeus = 1;
       int princessSuunta    = 0;
           */

            bCollision = false;

            prinsessanHidastaja++;
            if (prinsessanHidastaja > prinsessanHidastajanraja)
            {
                prinsessanHidastaja = 0;
            }
            if (prinsessanHidastaja == 0)
            {
                intPrincessNopeus = rnd.Next(1, 4);
                int muutos = rnd.Next(1, 3);
                if (muutos == 1) princessSuunta++;
                if (muutos == 2) princessSuunta--;
                if (princessSuunta > 8) princessSuunta = 1;
                if (princessSuunta < 1) princessSuunta = 8;

            }


            if (princessSuunta == 1)
            { //KOILLINEN
                int bcx = princessX;
                int bcy = princessY;
                bcx += intPrincessNopeus;
                bcy -= intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    princessX += intPrincessNopeus;
                    princessY -= intPrincessNopeus;
                }
            }
            if (princessSuunta == 2)
            //if (ita)
            { //ITÄ
                int bcx = princessX;
                int bcy = princessY;
                bcx += intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    princessX += intPrincessNopeus;
                    //princessY--;
                }
            }
            if (princessSuunta == 3)
            //if (kaakko)
            { //KAAKKO
                int bcx = princessX;
                int bcy = princessY;
                bcx += intPrincessNopeus;
                bcy += intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    princessX += intPrincessNopeus;
                    princessY += intPrincessNopeus;
                }
            }

            if (princessSuunta == 4)
            //if (etela)
            { //ETELÄ
                int bcx = princessX;
                int bcy = princessY;
                //bcx += intPrincessNopeus;
                bcy += intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    //princessX;
                    princessY += intPrincessNopeus;
                }
            }
            if (princessSuunta == 5)
            //if (lounas)
            { //LOUNAS
                int bcx = princessX;
                int bcy = princessY;
                bcx -= intPrincessNopeus;
                bcy += intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    princessX -= intPrincessNopeus;
                    princessY += intPrincessNopeus;
                }
            }
            if (princessSuunta == 6)
            //if (lansi)
            { //LÄNSI
                int bcx = princessX;
                int bcy = princessY;
                bcx -= intPrincessNopeus;
                //bcy -= intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    princessX -= intPrincessNopeus;
                    //princessY--;
                }
            }
            if (princessSuunta == 7)
            //if (luode)
            { //LUODE
                int bcx = princessX;
                int bcy = princessY;
                bcx -= intPrincessNopeus;
                bcy -= intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    princessX -= intPrincessNopeus;
                    princessY -= intPrincessNopeus;
                }
            }
            if (princessSuunta == 8)
            //if (pohjoinen)
            { //POHJOINEN
                int bcx = princessX;
                int bcy = princessY;
                //bcx += intPrincessNopeus;
                bcy -= intPrincessNopeus;
                if (bc.Check(naytonLeveys, naytonKorkeus, bcx, bcy))
                {
                    //princessX++;
                    princessY -= intPrincessNopeus;
                }
            }

            if (cc.Check(princessX, princessY, (int)paikka.X, (int)paikka.Y))
            {
                Console.WriteLine("Törmäys havaittu");
                bCollision = true;
            }

            //sankari.Update(gameTime);
            sankaritar.Update(gameTime);
            //if(Rectangle.Intersect(sankari.rect, sankaritar.rect) !Empty) ;
            //Rectangle.Empty
            Rectangle r = new Rectangle();
            //r = Rectangle.Intersect(sankari.rect, sankaritar.rect);
            //if (r != Rectangle.Empty) collisionDetected = true;
            //if (r != Rectangle.Empty) collisionDetected = true;
            /*
            if (r.Width > 1 || r.Height > 1)
            {
                collisionDetected = true;
                pixelCollision =  cs.Check(GraphicsDevice, ritari_anim, prinsessa, sankari.paikka, sankaritar.paikka, sankari.rect);

                //Console.WriteLine("r.Width = " + r.Width);
                //Console.WriteLine("r.Height = " + r.Height);
            }
            */
            ritarinHidastaja++;
            if (!peruutus)
            {
                if (ritarinHidastaja > ritarinHidastajaRaja)
                {
                    ritari_x -= 80;
                    if (ritari_x < 80) ritari_x = 320;
                    ritarinHidastaja = 0;
                }
            }
            else
            ///*
            {
                if (ritarinHidastaja > ritarinHidastajaRaja)
                {
                    ritari_x += 80;
                    if (ritari_x > 320) ritari_x = 80;
                    ritarinHidastaja = 0;
                }
            }

            //*/
            boolNappiaPainettu = false;
            curMouseState = Mouse.GetState(); //haetaan hiiren tila
            if (curMouseState.LeftButton == ButtonState.Pressed)
            // && lastMouseState.LeftButton == ButtonState.Released)  
            {
                Point mousePositionPoint = curMouseState.Position;
                mouseRect = new Rectangle(mousePositionPoint, new Point(300, 100));
                if (mouseRect.Intersects(button))
                {
                    boolNappiaPainettu = true; //tai false 
                    Console.WriteLine("Nappia painettu!");
                }
            }
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                //hahmo.LiikuVasemmalle
                liikkeella = true;
                if (!peruutus) { paikka.X -= n; eteenpain = true; }
                if (peruutus) { paikka.X -= n; eteenpain = false; }

            }
            else
                liikkeella = false;

            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                //hahmo.Liiku ylös
                liikkeella = true;
                if (!peruutus) { paikka.Y -= n; eteenpain = true; }
                if (peruutus) { paikka.Y -= n; eteenpain = false; }

            }
            else
                liikkeella = false;

            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                //hahmo.Liiku ylös
                liikkeella = true;
                if (!peruutus) { paikka.Y += n; eteenpain = true; }
                if (peruutus) { paikka.Y += n; eteenpain = false; }

            }
            else
                liikkeella = false;

            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                //hahmo.LiikuOikealle
                liikkeella = true;
                if (!peruutus) { paikka.X += n; eteenpain = true; }
                if (peruutus) { paikka.X += n; eteenpain = false; }

            }
            else
                liikkeella = false;

            if (newKeyboardState.IsKeyDown(Keys.B))
            {
                peruutus = true;
            }

            if (newKeyboardState.IsKeyDown(Keys.F))
            {
                peruutus = false;
            }

            //rotaationäppäimet
            if (newKeyboardState.IsKeyDown(Keys.X))
            {
                xpoint += 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.Z))
            {
                xpoint -= 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.Y))
            {
                ypoint += 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.T))
            {
                xpoint -= 10f;
            }

            if (newKeyboardState.IsKeyDown(Keys.R))
            {
                rot -= 30f; //degrees
            }

            if (newKeyboardState.IsKeyDown(Keys.E))
            {
                rot += 30f; //degrees
            }

            oldKeyboardState = newKeyboardState;   //tallenna vanha tila, jos tarpeen 

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

            spriteBatch.Draw(taustakuva, new Rectangle(0,0,1200, 800),  Color.White);

            spriteBatch.End();

            //Draw Hahmot
            //sankaritar.Draw(gameTime);
            //sankari.Draw(gameTime);

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
                if (collisionDetected) viesti4 = "TORMAYS HAVAITTU!";
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

            spriteBatch.Draw(piste, sankaritar.rect, Color.Red);
            //spriteBatch.Draw(cs.uusi, new Vector2(400f,400f), Color.White);
            //spriteBatch.Draw(cs.uusiGhost, new Vector2(400f, 200f), Color.White);

            if (liikkeella)
            {
                if (eteenpain)
                    spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White,
                        MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), 1f, SpriteEffects.FlipHorizontally, 0f);
                if (!eteenpain)
                    spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White,
                        MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), 1f, SpriteEffects.None, 0f);
            }

            else
                spriteBatch.Draw(ritari_anim, paikka, new Rectangle(0, 0, 80, 120), Color.White);

            spriteBatch.Draw(ritari_anim, button, new Rectangle(0, 130, 1, 1), Color.White);

            if (boolNappiaPainettu)
            {
                spriteBatch.Draw(ritari_anim, button, new Rectangle(0, 130, 1, 1), Color.Red);
            }

            spriteBatch.Draw(prinsessa, new Vector2((float)princessX, (float)princessY), new Rectangle(0, 0, 80, 120), Color.White);
            if (bCollision)
                spriteBatch.Draw(prinsessa, new Vector2((float)princessX, (float)princessY), new Rectangle(0, 0, 80, 120), Color.Red);

            spriteBatch.End();

            //Draw Hahmot
           // sankaritar.Draw(gameTime);
           // sankari.Draw(gameTime);






            base.Draw(gameTime);
        }
    }
}
