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
    class Kartta : DrawableGameComponent
    {
        new public Game Game { get { return base.Game; } }
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
            //012345678901234567890123456789
            p = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";//0
            p += "XVVVVVVVOOOOOOOOOOOOOOOOZOOOOX";//1
            p += "XXOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//2
            p += "XXOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//3
            p += "XXOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//4
            p += "XXOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//5
            p += "XXOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//6
            p += "XXOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//7
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//8
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//9
            p += "XOXOOOOOOOOOOOOOOOOOOOOOOOOOOX";//10
            p += "XOXOOOOOOOOOOOOOOOOOOOOOOOOOOX";//11
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//12
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//13
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//14
            p += "XOOZZZOOXXXXXXXXXXXOOOOOOOOOZX";//15
            p += "XOOZZZOOXOOOOOOOOOOOOOOOOOZOOX";//16
            p += "XOOZZOOOXOOOOOOOOOOOOOOOOZOOOX";//17
            p += "XOOZZOOOXOOOOOOXXOOOXXXXXXXXXX";//18
            p += "XOOZOOOOXOOOOOOXXOOOOOVOOOOOOX";//19
            p += "XOOZOOOOOOOOOOOXXOOOOOOOOOOOOX";//20
            p += "XOOZOOOOOOOOOOOXXXXXXXXXXXOOOX";//21
            p += "XOOOZOOOOOOOOOOOOOOOOOOOOOOOOX";//22
            p += "XOOOOZOOOOOOOOOOOOOOOOZZZOOOOX";//23
            p += "XOOXXXXXXXXOOOOOOOVOZZZZZOOOOX";//24
            p += "XOOOOOOOOOOOXOOOOOOZZZZZZOOOOX";//25
            p += "XOOOOOOOOOOOOOOOOOOOOZZZZOOOOX";//26
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//27
            p += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//28
            p += "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";//29
            p += "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";//30

            //012345678901234567890123456789
            p2 =  "OOOOOOOOOOOOOOOOOOOOOOOOOOOOOO";//0
            p2 += "OOOOOOOOOOOOOOOOOOOOOOOOOOOOOO";//1
            p2 += "OOOOOOOOOOOOOOOOOAOOOOOOOOOOOO";//2
            p2 += "OOOOOOOOOOOOOOOOOOOOOOOOOOOOOO";//3
            p2 += "OOOOOOOOOOOAAAAAOOOOOOOOOOOOOO";//4
            p2 += "OOOOOOOOOOOAAAAAOOOOOOOOOOOOOO";//5
            p2 += "OOOOOOOOOOOOOOOOOOAOOOOOOOOOOO";//6
            p2 += "OOOOOOOOOOOOOOOOOOOOOOOOOOOOOO";//7
            p2 += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//8
            p2 += "XOOOOOOOOOOOAOOOOOAOOOOOOOOOOX";//9
            p2 += "XOXOOOOOOOOOOOOOOOOOOOOOOOOOOX";//10
            p2 += "XOXOOOOOOOOOOOOOOOOOOOOOOOOOOX";//11
            p2 += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//12
            p2 += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//13
            p2 += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//14
            p2 += "XOOZZZOOXXXXXXXXXXXOOOOOOOOOZX";//15
            p2 += "XOOZZZOOXOOOOOOOOOOOOOOOOOZOOX";//16
            p2 += "XOOZZOOOXOOOOOOOOOOOOOOOOZOOOX";//17
            p2 += "XOOZZOOOXOOOOOOXXOOOXXXXXXXXXX";//18
            p2 += "XOOZOOOOXOOOOOOXXOOOOOVOOOOOOX";//19
            p2 += "XOOZOOOOOOOOOOOXXOOOOOOOOOOOOX";//20
            p2 += "XOOZOOOOOOOOOOOXXXXXXXXXXXOOOX";//21
            p2 += "XOOOZOOOOOOOOOOOOOOOOOOOOOOOOX";//22
            p2 += "XOOOOZOOOOOOOOOOOOOOOOZZZOOOOX";//23
            p2 += "XOOXXXXXXXXOOOOOOOVOZZZZZOOOOX";//24
            p2 += "XOOOOOOOOOOOXOOOOOOZZZZZZOOOOX";//25
            p2 += "XOOOOOOOOOOOOOOOOOOOOZZZZOOOOX";//26
            p2 += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//27
            p2 += "XOOOOOOOOOOOOOOOOOOOOOOOOOOOOX";//28
            p2 += "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";//29
            p2 += "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";//30
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
            Console.WriteLine("Merkkijonon p pituus == " + p.Length);
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
                    if (haku > p.Length - 2) { break; }
                    //if (haku > 100) { break; }
                    s = p.Substring( haku, 1);
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
            Console.WriteLine("haku == " + haku);

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
                        if (p.Substring(haku, 1) == "O")
                            spriteBatch.Draw(sand, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);
                        else
                        //if (spala == "vesi")
                        if (p.Substring(haku, 1) == "Z")
                            spriteBatch.Draw(water, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);
                        else
                        //if (spala == "kivi")
                        if (p.Substring(haku, 1) == "X")
                            spriteBatch.Draw(stone, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.White);
                    else
                        //väritön
                        if (p.Substring(haku, 1) == "V")
                        spriteBatch.Draw(variton, new Rectangle(30 + j * 30, 10 + i * 30, 30, 30), Color.Chocolate);

                    else
                        if (p.Substring(haku, 1) == "A")
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

