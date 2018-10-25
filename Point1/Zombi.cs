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
    class Zombi : Hahmo

    {

        public static int zombiNr;
        public int numero; //zombin järjestysnumero
        //Miekka miekka; //bool? Miekka-luokan ilmentymä?, int = miekan kunto?
        //private KeyboardState oldKeyboardState;
        GraphicsDevice gd;
        public new SpriteBatch spriteBatch;
        Automove am;
        public Texture2D prinsessa; //spritesheet, 4 kuvaa a 80x120
        public Texture2D ritari_anim;
        Sounder sounder;
        CollisionChecker cs;
        public Vector2 paikka; // sijainnin koordinaatit
        int ritari_x = 0; //spritesheet-animaation muuttuv koordinaatti
        //animaaation hidastuslaskurin muuttujat
        int ritarinHidastaja;
        int ritarinHidastajaRaja = 5;
        private bool collisionDetected;
        private bool pixelCollision;
        private bool crashPlayed;

        //private Func<Prinsessa> luoPrinsessa;

        //liikkumisen tilamuuttujat
        //bool eteenpain = true; //ohjaus F-näppäin
        //bool peruutus = false; // ohjaus B-näppäin
        //bool liikkeella = false; //true kun vasen tai oikea nuolinäppäin on painettuna
        //int n = 1; //nopeusmuuttuja
        //rotaation kääntöpisteen arvot, ohjataan X, Z, Y ja T näppäimillä
        //aluksi keskipiste 80x120 kokoiselle osaspritelle
        //float xpoint = 40f;
        //float ypoint = 60f;
        //float rot = 0f; //asteina, ohjataan R ja E näppäimillä

        public Zombi(Game game) : base(game)
        {

            InitZombi();
        }

        //public Prinsessa(Func<Prinsessa> luoPrinsessa);
        //{
        //    this.luoPrinsessa = luoPrinsessa;
        //}

        public void InitZombi()
        {
            //gd = new GraphicsDevice(GraphicsDevice.Adapter, GraphicsProfile.HiDef, PresentationParameters( );
            gd = Game1.Instance.GraphicsDevice;
            //GraphicsDevice.Adapter
            //prinsessa = new Texture2D(GraphicsDevice, 800, 600);
            sounder = new Sounder();
            sounder.InitSounder();
            cs = new CollisionChecker();
            spriteBatch = new SpriteBatch(gd);
            paikka = new Vector2(0f, 0f);
            am = new Automove(paikka);
            //prinsessa = Game1.Instance.Content.Load<Texture2D>("Prinsessa_animaatio1");
            prinsessa = Game1.Instance.Content.Load<Texture2D>("AnimOgre128x512");
            ritari_anim = Game1.Instance.Content.Load<Texture2D>("RitariKavely1_4kuvaa");
        }

        public void Liiku()
        {
            paikka =  am.Wander();
        }

        public override void Update(GameTime gameTime)
        {
            ritarinHidastaja++;
            {
                if (ritarinHidastaja > ritarinHidastajaRaja)
                {
                    ritari_x -= 128;
                    if (ritari_x < 128) ritari_x = 512 - 128;
                    ritarinHidastaja = 0;
                }
            }

            rect.X = (int)paikka.X;
            rect.Y = (int)paikka.Y;
            //Console.WriteLine("paikka.X = " + paikka.X);
            //Console.WriteLine("paikka.Y = " + paikka.Y);
            //Console.WriteLine("rect.Width = " + rect.Width);
            //Console.WriteLine("rect.Height = " + rect.Height);

            Rectangle r = new Rectangle();
            r = Rectangle.Intersect(GP.sankarirect, this.rect);
            //if (r != Rectangle.Empty) collisionDetected = true;
            if (r != Rectangle.Empty) //collisionDetected = true;
                Console.WriteLine("ZOMBITÖRMÄYS rectanglella  " + numero+ " /" + Zombi.zombiNr);
            //if (r.Width > 1 || r.Height > 1)
            {
                collisionDetected = true;
                //pixelCollision = cs.Check(GraphicsDevice, ritari_anim, prinsessa, paikka, GP.sankaripaikka, GP.sankarirect);
                pixelCollision = cs.Check(gd, ritari_anim, prinsessa, paikka, GP.sankaripaikka, GP.sankarirect);
                if (pixelCollision && !crashPlayed)
                {
                    sounder.Crash(); crashPlayed = true;
                    Console.WriteLine("ZOMBITÖRMÄYS pikseleillä " + GP.sankariterveys);
                    GP.sankariterveys--;
                }
                else
                    crashPlayed = false;
                //Console.WriteLine("r.Width = " + r.Width);
                //Console.WriteLine("r.Height = " + r.Height);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Taustan väri 
            //GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //spriteBatch.Draw(ritari_anim, paikka, Color.White); //testattu että kuva näkyy yleensä
            string viesti = "Tervehdys!";
            //Vector2 alkupaikka = omaFontti.MeasureString(viesti);
            //spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2), Color.White); //tekstin tulostus
            //spriteBatch.Draw(ritari_anim, new Rectangle((int) paikka.X, (int) paikka.Y, 160, 240), new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //koko suurennettu 2-kertaiseksi
            //spriteBatch.Draw(ritari_anim, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White); //spritetsheet-animaatio yksinkertaisesti
            spriteBatch.Draw(prinsessa, paikka, new Rectangle(ritari_x - 128, 0, 128, 128), Color.White); //spritetsheet-animaatio yksinkertaisesti 
            //spriteBatch.Draw(prinsessa, paikka, new Rectangle(ritari_x - 80, 0, 80, 120), Color.White, 0, new Vector2(xpoint, ypoint), 1f, SpriteEffects.None, 0f);
               
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
