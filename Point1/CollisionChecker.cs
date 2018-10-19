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
    class CollisionChecker //: DrawableGameComponent
    {
        Color[][] playerData; // 
        Texture2D playerTexture; //ladattava jossain
        Rectangle pRect;
        Color[][] zombiData;
        Texture2D zombiTexture;
        GraphicsAdapter adapter; 
        GraphicsDevice newdevice;
        public Texture2D uusi;
        public Texture2D uusiGhost;
        public Matrix matrix2;
        public Matrix matrix1 { get; private set; }

       // public CollisionChecker(Game game) : base(game)
       // {
       // }

        public void Init()
        {
            /*
            PresentationParameters pp = new PresentationParameters();
            GraphicsProfile gp = new GraphicsProfile();
            adapter = new GraphicsAdapter();
            newdevice = new GraphicsDevice(adapter, gp, pp);
            //new GraphicsDevice()
            */
        }

        public bool Check(GraphicsDevice gd,  Texture2D player, Texture2D zombi, Vector2 playerPos, Vector2 zombiPos, Rectangle playerRect)
        {
            matrix1 = Matrix.CreateTranslation(new Vector3(-playerPos, 0.0f));
            matrix2 = Matrix.CreateTranslation(new Vector3(-zombiPos, 0.0f));
           //Console.WriteLine("Checking pixel collision ");
           newdevice = gd;
            pRect = playerRect;
            playerTexture = player;
            zombiTexture = zombi;
            playerData = new Color[4][];
            zombiData = new Color[4][];
            uusi = new Texture2D(newdevice, pRect.Width, pRect.Height);
            uusiGhost = new Texture2D(newdevice, 80, 120);

            Matrix transform = matrix1 * Matrix.Invert(matrix2);

            Vector2 rowX = Vector2.TransformNormal(Vector2.UnitX, transform); Vector2 rowY = Vector2.TransformNormal(Vector2.UnitY, transform);

            Vector2 yPos = Vector2.Transform(Vector2.Zero, transform);



            //värien haku
            for (int i = 0; i < 4; i++)
            //int ii = 0;
            {
                ///*
                playerData[i] = new Color[pRect.Width * pRect.Height];
                playerTexture.GetData(0, new Rectangle(i, 0, pRect.Width, pRect.Height),
                    playerData[i], 0, (pRect.Width) * (pRect.Height));
                //*/
                /*
                playerData[ii] = new Color[pRect.Width * pRect.Height];
                playerTexture.GetData(0, new Rectangle(ii, 0, 80, 120),
                    playerData[ii], 0, (80) * (120));
                */

                zombiData[i] = new Color[80 * 120];
                zombiTexture.GetData(0, new Rectangle(i, 0, 80, 120),
                    zombiData[i], 0, 80 * 120);
            }

            

            for (int i = 0; i < 4; i++)
            {
                
                //uusi.SetData(0, new Rectangle(i, 0, pRect.Width, pRect.Height),
                 //   playerData[i], 0, pRect.Width * pRect.Height);

                //uusi.SetData
               // uusi.SetData(0, new Rectangle(i, 0, 79, 119),
                 //   playerData[i], 0, 79 * 119);

                uusi.SetData(playerData[i]);
                uusiGhost.SetData(zombiData[i]);

            }

                //pikselipohjainen törmäystarkistus
                for (int i = 0; i < 120; i++)
            {
                Vector2 pos = yPos; //playerPos - zombiPos; //yPos;

                for (int j = 0; j < 80; j++)
                {
                    int i2 = (int)Math.Round(pos.X); int j2 = (int)Math.Round(pos.Y);

                    if (0 <= i2 && i2 < 80 && 0 <= j2 && j2 < 120)
                    {
                        Color color1 = playerData[0][i + j * 80]; Color color2 = zombiData[0][i2 + j2 * 80];

                        if (color1.A != 0 && color2.A != 0)
                            //Console.WriteLine("Pikselitörmäys!");
                            //color1.
                            return true;
                    }
                    pos +=  rowX;
                }
                yPos += rowY;
            }

            return false;
        }
        /*
        internal bool Check(Texture2D ritari_anim, Texture2D prinsessa, Vector2 paikka1, Vector2 paikka2, Rectangle rect)
        {
            throw new NotImplementedException();
        }
        */
    }
}
