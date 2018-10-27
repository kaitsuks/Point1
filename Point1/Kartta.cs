using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Point1;

namespace Point1
{
   public class Kartta : DrawableGameComponent
    {
        new public Game Game { get { return base.Game; } }
        KarttaData k = new KarttaData();
        public  Pala pala;
        //public Pala[,] palat;
        public string[ ] palat;
        public string p;
        public string p2;
        int klev = 30;
        int kkork = 30;
        Texture2D sand;
        Texture2D stone;
        Texture2D water;
        Texture2D variton;
        SpriteBatch spriteBatch;
        int naytonLeveys;
        int naytonKorkeus;
        Texture2D taustakuva;
        SpriteFont omaFontti;
        Tarkistus tarkistus;
        
        public Kartta(Game game) : base(game)
        {
                    }
        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            naytonLeveys = GP.naytonLeveys;
            naytonKorkeus = GP.naytonKorkeus;
            omaFontti = Game1.Instance.Content.Load<SpriteFont>("Arial20");
            tarkistus = new Tarkistus();
           
            //palat = new Pala[klev, kkork];
            //palat = new string[klev, kkork];
            palat = new string[kkork * klev + 2];
           
            //teeKartta();
        
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            taustakuva = Game1.Instance.Content.Load<Texture2D>("WP_000207");
            sand = Game1.Instance.Content.Load<Texture2D>("tilesand");
            stone = Game1.Instance.Content.Load<Texture2D>("tilestone");
            water = Game1.Instance.Content.Load<Texture2D>("tilewater");
            variton = Game1.Instance.Content.Load<Texture2D>("valkopiste");
        }

        public bool teeKartta()
        {

            

            string s = "";
            int haku = 0;
            //string tulos = "";
            //Console.WriteLine("Merkkijonon p pituus == " + p.Length);
            for (int i = 0; i < kkork; i++)
                //for (int i = kkork; i > 0; i--)
                {
                //Console.WriteLine("i:n arvo " + i);
                //Console.WriteLine("j:n arvo on  " + j);
                
                for (int j = 0; j < (klev); j++)
                    //for (int j = klev; j > 0; j--)
                    {
                    
                    //tulos += "  " + haku.ToString();
                    //Console.WriteLine("haku == " + haku);
                    if (haku > k.p.Length - 2) { break; }
                    //if (haku > 100) { break; }
                    s = k.p.Substring( haku, 1);
                    haku++;
                    ///*
                    switch (s)
                    {
                        
                        //case "O": { palat[i, j] = new HiekkaPala(Game); } break;
                        //case "Z": { palat[i, j] = new VesiPala(Game); } break;
                        //case "X": { palat[i, j] = new KiviPala(Game); } break;
                        //case "O": { palat[i, j] = "hiekka"; } break;
                        //case "Z": { palat[i, j] = "vesi"; } break;
                        //case "X": { palat[i, j] = "kivi"; } break;
                        /*
                        case "O": { palat[haku] = "hiekka"; } break;
                        case "Z": { palat[haku] = "vesi"; } break;
                        case "X": { palat[haku] = "kivi"; } break;
                        default: { palat[haku] = "hiekka"; } break;
                        */
                    }
                    //*/
                }
                //Console.WriteLine("  " + );
            }
            //Console.WriteLine("Tulos == " + tulos);
            //Console.WriteLine("haku == " + haku);

            return true;
        }

        public bool naytaKartta()
        {
            
            spriteBatch.Begin();

            string s = "";
            int haku = 0;
            string spala = "";
            Color vari = Color.White;
            
            for (int i = 0; i < kkork; i++)
                //for (int i = kkork; i > 0; i--)
                {
               for (int j = 0; j < klev; j++)
                    //for (int j = klev; j > 0; j--)
                    {
                    //pala = palat[i * j + j];
                    //spala = palat[haku];
                    
                    //s = pala.laji;
                    //vari = pala.vari;
                    //
                        //if (spala == "hiekka")
                        if (k.p.Substring(haku, 1) == "O")
                            spriteBatch.Draw(sand, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);
                        else
                        //if (spala == "vesi")
                        if (k.p.Substring(haku, 1) == "Z")
                            spriteBatch.Draw(water, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);
                        else
                        //if (spala == "kivi")
                        if (k.p.Substring(haku, 1) == "X")
                            spriteBatch.Draw(stone, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);
                    else
                        //väritön
                        if (k.p.Substring(haku, 1) == "V")
                        spriteBatch.Draw(variton, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.Chocolate);

                    else
                        if (k.p.Substring(haku, 1) == "A")
                        spriteBatch.Draw(variton, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.Black);

                     else
                    spriteBatch.Draw(variton, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);

                    haku++; 
                }
            }
            spriteBatch.End();

            return true;
        }
    }
       
 }

