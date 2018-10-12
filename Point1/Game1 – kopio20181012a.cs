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
        Vector2 paikka; // sijainnin koordinaatit

        /*  //nämä ei käytössä vielä
        int n = 8; // kokonaislukumuuttuja sijainnin X-koordinaatin muuttamiseksi
        int m = 1; // kokonaislukumuuttuja sijainnin Y-koordinaatin muuttamiseksi
        int yynhidastaja = 0; // 
        int yynhidastajanraja = 6; //
        //*/

        int naytonLeveys;
        int naytonKorkeus;
        private KeyboardState oldKeyboardState;
        SpriteFont omaFontti;

        Ritari sankari; // ei käytössä vielä

        int ritari_x = 0; //spritesheet-animaation muuttuv koordinaatti

        //animaaation hidastuslaskurin muuttujat
        int ritarinHidastaja;
        int ritarinHidastajaRaja = 5;

        //liikkumisen tilamuuttujat
        bool eteenpain = true; //ohjaus F-näppäin
        bool peruutus = false; // ohjaus B-näppäin
        bool liikkeella = false; //true kun vasen tai oikea nuolinäppäin on painettuna

        int n = 1; //nopeusmuuttuja

        Random rnd; //satunnaisluku

        //rotaation kääntöpisteen arvot, ohjataan X, Z, Y ja T näppäimillä
        //aluksi keskipiste 80x120 kokoiselle osaspritelle
        float xpoint = 40f; 
        float ypoint = 60f;

        float rot = 0f; //asteina, ohjataan R ja E näppäimillä

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
            sankari = new Ritari();


            naytonLeveys = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            naytonKorkeus = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //aseta peli-ikkunalle koko tarvittaessa

            if (naytonLeveys >= 600)
            {
                naytonLeveys = 600;
            }
            if (naytonKorkeus >= 400)
            {
                naytonKorkeus = 400;
            }
            graphics.PreferredBackBufferWidth = naytonLeveys;
            graphics.PreferredBackBufferHeight = naytonKorkeus;
            graphics.ApplyChanges();

            rnd = new Random(); //luodaan satunnaisluku


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


            KeyboardState newKeyboardState = Keyboard.GetState();
            //käsittelee näppäimistön tilan   

            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                //hahmo.LiikuVasemmalle
                liikkeella = true;
                if (!peruutus) { paikka.X -= n; eteenpain = true; }
                if (peruutus) { paikka.X -= n; eteenpain = false; }

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
                rot -= 3f; //degrees
            }

            if (newKeyboardState.IsKeyDown(Keys.E))
            {
                rot += 3f; //degrees
            }

            //nopeus
            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                n += 1;
                if (n > 10) n = 10;
            }

            //nopeus
            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                n -= 1;
                if (n < 0) n = 0;
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


            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //spriteBatch.Draw(ritari_anim, paikka, Color.White); //testattu että kuva näkyy yleensä
            string viesti = "Tervehdys!";
            Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            //spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2), Color.White); //tekstin tulostus


            //spriteBatch.Draw(ritari_anim, new Rectangle((int) paikka.X, (int) paikka.Y, 160, 240), new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //koko suurennettu 2-kertaiseksi

            //spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //spritetsheet-animaatio yksinkertaisesti
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
                spriteBatch.Draw(ritari_anim, paikka, new Rectangle(0, 0, 80, 120), Color.White,
                        MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), 1f, SpriteEffects.None, 0f);


            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
