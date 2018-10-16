using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Point1
{
    class OhjeScreen : GameScreen
    {

        string viesti0, viesti, viesti2, viesti3, viesti4;
        Vector2 alkupaikka0, alkupaikka, alkupaikka2, alkupaikka3, alkupaikka4;
        Color textColor;

        public OhjeScreen(Game game) : base(game)
        {
            textColor = Color.DodgerBlue;
        }

        protected override void LoadContent()
        {
            //taustakuva = Game1.Instance.Content.Load<Texture2D>("WP_000303");
            taustakuva = Game1.Instance.Content.Load<Texture2D>("WP_000207");

        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(taustakuva, new Rectangle(0, 0, naytonLeveys, naytonKorkeus), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
            spriteBatch.Begin();
            spriteBatch.DrawString(omaFontti, "OHJEET", new Vector2((naytonLeveys - 300) / 2, naytonKorkeus / 2 - 400), Color.AliceBlue, 0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0f);
            // Draw texts
            //Color textColor = new Color(Color.OrangeRed, 1f);
           viesti = "Liikkuminen sivulle: nuolet vasemmalle ja oikealle";
            alkupaikka = omaFontti.MeasureString(viesti);
            spriteBatch.DrawString(omaFontti, viesti, new Vector2((naytonLeveys - alkupaikka.X) / 2, naytonKorkeus / 2 +200 ), textColor); //tekstin tulostus
             viesti2 = "Suunnanmuutos: B ja F, nopeus M ja L";
            alkupaikka2 = omaFontti.MeasureString(viesti2);
            spriteBatch.DrawString(omaFontti, viesti2, new Vector2((naytonLeveys - alkupaikka2.X) / 2, naytonKorkeus / 2 +200 + 40), textColor); //tekstin tulostus
              viesti3 = "Lahemmaksi ja kauemmaksi: Yla- ja alanuoli. Rotaatiot: E, R, T, Z, X";
            alkupaikka3 = omaFontti.MeasureString(viesti3);
            spriteBatch.DrawString(omaFontti, viesti3, new Vector2((naytonLeveys - alkupaikka3.X) / 2, naytonKorkeus / 2 +200 + 80), textColor); //tekstin tulostus

            spriteBatch.End();

            //base.Draw(gameTime);
        }
    }
}
