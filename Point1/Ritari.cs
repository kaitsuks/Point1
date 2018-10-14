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
    class Ritari : Hahmo

    {
        

        Miekka miekka; //bool? Miekka-luokan ilmentymä?, int = miekan kunto?
        private KeyboardState oldKeyboardState;

        public SpriteBatch spriteBatch;

        public Texture2D prinsessa; //spritesheet, 4 kuvaa a 80x120
        public Texture2D ritari_anim; //spritesheet, 4 kuvaa a 80x120
        public Vector2 paikka; // sijainnin koordinaatit
        int ritari_x = 0; //spritesheet-animaation muuttuv koordinaatti
        //animaaation hidastuslaskurin muuttujat
        int ritarinHidastaja;
        int ritarinHidastajaRaja = 4;
        //liikkumisen tilamuuttujat
        bool eteenpain = true; //ohjaus F-näppäin
        bool peruutus = false; // ohjaus B-näppäin
        bool liikkeella = false; //true kun vasen tai oikea nuolinäppäin on painettuna
        bool ylos = false;
        bool alas = false;
        int n = 1; //nopeusmuuttuja
        //rotaation kääntöpisteen arvot, ohjataan X, Z, Y ja T näppäimillä
        //aluksi keskipiste 80x120 kokoiselle osaspritelle
        float xpoint = 0f;
        float ypoint = 0f;
        float rot = 0f; //asteina, ohjataan R ja E näppäimillä
        public float skaala = 1f;
        public float perusskaala = 2f;



        public Ritari(Game game) : base(game)
        {
        }

        

            public void InitRitari()
        {
            prinsessa = new Texture2D(GraphicsDevice, 800, 600);
            ritari_anim = new Texture2D(GraphicsDevice, 800, 600);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            paikka = new Vector2(300f, 580f);
        }

        public void Liiku()
        {


        }

        public override void Update(GameTime gameTime)
        {
            
            ritarinHidastaja++;
            if (eteenpain)
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

            liikkeella = false;
            ylos = false;
            alas = false;

            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                //hahmo.LiikuVasemmalle
                liikkeella = true;
                if (!peruutus) { paikka.X -= n; eteenpain = true; }
                if (peruutus) { paikka.X -= n; eteenpain = false; }

            }
            
                

            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                //hahmo.LiikuOikealle
                liikkeella = true;
                if (!peruutus) { paikka.X += n; eteenpain = true; }
                if (peruutus) { paikka.X += n; eteenpain = false; }

            }
            

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
            if (newKeyboardState.IsKeyDown(Keys.M))
            {
                if (ritarinHidastaja == 0)
                {
                    n += 1; ritarinHidastajaRaja--;
                    if (n > 10) { n = 10; ritarinHidastajaRaja = 0; }
                }
            }

            //nopeus
            if (newKeyboardState.IsKeyDown(Keys.L))
            {
                if (ritarinHidastaja == 0)
                {
                    n -= 1; ritarinHidastajaRaja++;
                    if (n < 0) { n = 0; ritarinHidastajaRaja = 5; }
                }
            }
            //ylös
            if (newKeyboardState.IsKeyDown(Keys.Up))
            {
                paikka.Y -= 1 * n;
                if (paikka.Y < 50) paikka.Y = 50;
                ylos = true;
                skaala -= 0.001f * n;
                if (skaala < 0.7f) skaala = 0.7f;
            }

            //alas
            if (newKeyboardState.IsKeyDown(Keys.Down))
            {
                paikka.Y += 1 * n;
                if (paikka.Y > 600) paikka.Y = 600;
                alas = true;
                skaala += 0.001f * n;
                if (skaala > 1f) skaala = 1f; 
            }

            //rect.X = (int) (paikka.X * skaala);
            //rect.Y = (int) (paikka.Y * skaala);
            rect.X = (int)paikka.X;
            rect.Y = (int)paikka.Y;
            /*
            Console.WriteLine("paikka.X = " + paikka.X);
            Console.WriteLine("paikka.Y = " + paikka.Y);
            Console.WriteLine("rect.Width = " + rect.Width);
            Console.WriteLine("rect.Height = " + rect.Height);
            */
            rect.Width = (int) (80 * skaala * perusskaala);
            rect.Height = (int)(120 * skaala * perusskaala);

            oldKeyboardState = newKeyboardState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //spriteBatch.Draw(ritari_anim, paikka, Color.White); //testattu että kuva näkyy yleensä
            string viesti = "Tervehdys!";
            //Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            //spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2), Color.White); //tekstin tulostus


            //spriteBatch.Draw(ritari_anim, new Rectangle((int) paikka.X, (int) paikka.Y, 160, 240), new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //koko suurennettu 2-kertaiseksi

            //spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //spritetsheet-animaatio yksinkertaisesti
            if (liikkeella || ylos || alas)
            {
                if (eteenpain)
                    spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White, MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), perusskaala * skaala, SpriteEffects.FlipHorizontally, 0f);
                if (!eteenpain)
                    spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White, MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), perusskaala * skaala, SpriteEffects.None, 0f);
            }
            else if (eteenpain)
                spriteBatch.Draw(ritari_anim, paikka, new Rectangle(0, 0, 80, 120), Color.White,
                        MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), perusskaala * skaala, SpriteEffects.FlipHorizontally, 0f);
            else
                spriteBatch.Draw(ritari_anim, paikka, new Rectangle(0, 0, 80, 120), Color.White,
                        MathHelper.ToRadians(rot), new Vector2(xpoint, ypoint), perusskaala * skaala, SpriteEffects.None, 0f);



            spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
