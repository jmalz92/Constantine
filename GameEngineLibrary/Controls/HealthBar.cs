using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngineLibrary.Controls
{
    public class HealthBar : Control
    {
        #region Fields and Properties
        int width;
        int height;
        Texture2D healthTexture;
        Texture2D borderTexture;
        int maxHealth;
        int playerHealth;

        public int ChangeUpperHealth
        {
            set { maxHealth = value; }
        }

        public Texture2D HealthTexture
        {
            get { return healthTexture; }
            set { healthTexture = value; }
        }
        public Texture2D BorderTexture
        {
            get { return borderTexture; }
            set { borderTexture = value; }
        }

        public bool IsEmpty
        {
            get { return playerHealth <= 0; }
        }
        #endregion

        #region Constructor Region
        public HealthBar(int initialHealth, int posX, int posY)
        {
            maxHealth = initialHealth;
            position.X = posX;
            position.Y = posY;
            tabStop = false;
        }
        #endregion

        #region Method Region
        
        public void UpdatePlayerHealth(int pHealth)
        {
            playerHealth = pHealth;
        }
        
        public override void Update(GameTime gameTime)
        {
        }

        public override void HandleInput()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(borderTexture, position, Color.White);

            int innerX = (((int)position.X + 210) / 2) - (200 / 2);
            int innerY = (((int)position.Y + 30) / 2) - (30 / 2);
            Rectangle srect = new Rectangle((int)position.X + 5, (int)position.Y + 5, (int)(((double)playerHealth / maxHealth) * 200), 30);
            Rectangle drect = new Rectangle((int)position.X + 5, (int)position.Y + 5, 200, 30);
            spriteBatch.Draw(healthTexture, srect, drect, Color.White);
            
        }        
        #endregion
    }
}
