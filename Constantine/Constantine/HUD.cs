using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Constantine
{
    public class HUD
    {
        //TODO:  Refactor this out and make it inherit from the control class
        Texture2D rectangleTextureFill;
        Texture2D rectangleOuter;
        int maxHealth;
        int ScreenWidth, ScreenHeight;

        public HUD(int initialHealth)
        {
            maxHealth = initialHealth;
        }
        public void LoadContent(GraphicsDevice gd)
        {
            rectangleTextureFill = new Texture2D(gd, 200, 30, false, SurfaceFormat.Color);
            rectangleOuter = new Texture2D(gd, 220, 52, false, SurfaceFormat.Color);
            Color[] backColor = new Color[220 * 52];
            for (int i = 0; i < backColor.Length; i++)
            {
                backColor[i] = new Color(0, 0, 0);
            }
            rectangleOuter.SetData(backColor);

            Color[] textureColor = new Color[200 * 30];
            for (int i = 0; i < textureColor.Length; i++)
            {
                textureColor[i] = new Color(0, 0, 200);
            }
            rectangleTextureFill.SetData(textureColor);
            this.ScreenWidth = gd.Viewport.Width;
            this.ScreenHeight = gd.Viewport.Height;
        }

        public void Draw(Player p, SpriteBatch sb)
        {
            sb.Draw(rectangleOuter, new Vector2(ScreenWidth - 220, ScreenHeight - 52), Color.White);
            Rectangle srect = new Rectangle(ScreenWidth - 210, ScreenHeight - 41, (int)(((double)p.Health / maxHealth) * 200), 30);
            Rectangle drect = new Rectangle(ScreenWidth - 210, ScreenHeight - 41, 200, 30);
            sb.Draw(rectangleTextureFill, srect, drect, Color.White);
        }
    }
}
