using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Random = Microsoft.Xna.Framework.Random;
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
        Texture2D pallo; // tai mikä vain kuvan nimi
        Texture2D sign; // tai mikä vain kuvan nimi
        Texture2D laatta; //
        Vector2 paikka; // sijainnin koordinaatit
        int n = 8; // kokonaislukumuuttuja sijainnin X-koordinaatin muuttamiseksi
        int m = 1; // kokonaislukumuuttuja sijainnin Y-koordinaatin muuttamiseksi
        int yynhidastaja = 0; // 
        int yynhidastajanraja = 6; //
        int signy, signx;
        int signykoko;
        int signxkoko;
        int naytonLeveys;
        int naytonKorkeus;
        private KeyboardState oldKeyboardState;
        SpriteFont omaFontti;
        Ritari sankari;
        Random rnd;
        float xpoint = 0f;
        float ypoint = 0f;
        float rot = 0f;

        int month;
        int dice;
        int card;

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
            paikka = new Vector2(0f, 0f);
            sankari = new Ritari();
            signy = 300; signx = 100;
            signykoko = 500;
            signxkoko = 100;

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

            rnd = new Random();
            month = rnd.Next(1, 13); // creates a number between 1 and 12
            dice = rnd.Next(1, 7);   // creates a number between 1 and 6
            card = rnd.Next(52);     // creates a number between 0 and 51


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
            //pallo = Content.Load<Texture2D>("ad_board");
            pallo = Content.Load<Texture2D>("antjeankkuti3d");
            laatta = Content.Load<Texture2D>("boat2");
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
            //paikka.X += n;
            //if (paikka.X > 800) n = -n;
            //if (paikka.X < 0) n = -n;

            yynhidastaja++;
            if (yynhidastaja > yynhidastajanraja)
            {
                paikka.Y += m;
                if (paikka.Y > 440) m = -m;
                if (paikka.Y < 0) m = -m;
                yynhidastaja = 0;
            }

            KeyboardState newKeyboardState = Keyboard.GetState();
            //käsittelee näppäimistön tilan                           
            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                //hahmo.LiikuVasemmalle);
                paikka.X -= n;
                //if (paikka.X > 800) n = -n;
                //if (paikka.X < 0) n = -n;
            }
            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                //hahmo.LiikuOikealle();
                paikka.X += n;
                //if (paikka.X > 800) n = -n;
                //if (paikka.X < 0) n = -n;
            }

            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                //hahmo.LiikuVasemmalle);
                paikka.Y -= n;
                //if (paikka.X > 800) n = -n;
                //if (paikka.X < 0) n = -n;
            }

            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                //hahmo.LiikuVasemmalle);
                paikka.Y += n;
                //if (paikka.X > 800) n = -n;
                //if (paikka.X < 0) n = -n;
            }

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
                rot -= 0.1f;
            }

            if (newKeyboardState.IsKeyDown(Keys.E))
            {
                rot += 0.1f;
            }

            //newKeyboardState.IsKeyDown(Keys.???)
            oldKeyboardState = newKeyboardState;   //tallenna vanha tila, jos tarpeen    
            //Random rnd = new Random();
            n = rnd.Next(6);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Taustan väri Color.Gold - Mikan mielestä paras
            GraphicsDevice.Clear(Color.Gold);
            

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            // Kokeillut värit: Gold, Fuchsia...
            //spriteBatch.Draw(pallo, paikka, Color.Gold);
            //spriteBatch.Draw(sign, new Vector2(100,100), Color.White);
            //spriteBatch.Draw(pallo, paikka, Color.Gold);
            /*
            (Texture2D texture,
Vector2 position,
Rectangle? SourceRectangle, nullable
Color color,
float rotation,
Vector2 origin,
float scale,
SpriteEffects effects,
float layerDepth)
*/

            spriteBatch.Draw(pallo, //Texture2D texture,
            paikka, //Vector2 position,
            null, //Rectangle? SourceRectangle, nullable
            Color.White, //Color color,
            rot, //float rotation,
            //new Vector2(xpoint, ypoint), //Vector2 origin,
            new Vector2(pallo.Width/2, pallo.Height/2),
            0.1f, //float scale,
            SpriteEffects.None, //SpriteEffects effects,
            0f); //float layerDepth)
           
            string viesti = "Tervehdys!";
            Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2), Color.White);

            //spriteBatch.Draw(laatta, new Vector2(300, 300), Color.Gold);
            //spriteBatch.Draw(sign, new Rectangle(signx, signy, signxkoko, signykoko), Color.AntiqueWhite);
            //spriteBatch.Draw(pallo, new Rectangle(0, 0, 600, 400), new Rectangle(0, 0, 64, 36), Color.White);
            //spriteBatch.Draw(pallo, new Vector2(0f, 0f), new Rectangle(0, 0, 100, 100), Color.White, 0.3f, new Vector2(0,0), 0.0f, null, 1f);
            //spriteBatch.Draw(pallo, new Vector2(0f, 0f), new Rectangle(0,0,100,100), Color.Black, 0.0f, new Vector2(1,1), 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw()
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
