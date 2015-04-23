using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngineLibrary.Controls
{
    public class PowerUpBar : Control
    {

        #region Fields and Properties
        Texture2D powerUpTexture;
        Texture2D borderTexture;
        float currentPower = 0;
        float maxPower = 100;
        float powerStep = 100.0f / 3.0f;
        bool powerUpComplete = false;
        TimeSpan lastGameTime;
        

        public Texture2D PowerUpTexture
        {
            get { return powerUpTexture; }
            set { powerUpTexture = value; }
        }
        public Texture2D BorderTexture
        {
            get { return borderTexture; }
            set { borderTexture = value; }
        }
        public bool IsPowerUpComplete
        {
            get { return powerUpComplete; }
        }
        #endregion

        #region Constructor
        public PowerUpBar(int positionX, int positionY)
        {
            position.X = positionX;
            position.Y = positionY;
            tabStop = false;
            lastGameTime = new TimeSpan();
        }
        #endregion

        #region Methods
        public void IncreasePowerUpBar()
        {
            if (!powerUpComplete)
            {
                currentPower += powerStep;
                if (currentPower >= 100)
                {
                    powerUpComplete = true;
                }
            }
        }
        public void DecreasePowerUpBar()
        {
            if (powerUpComplete)
            {
                currentPower--;
                if (currentPower < 1)
                {
                    powerUpComplete = false;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime - lastGameTime) > new TimeSpan(0, 0, 1))
            {
                lastGameTime = gameTime.TotalGameTime;
                if (powerUpComplete)
                {
                    DecreasePowerUpBar();
                }
            }
        }
        public override void HandleInput()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(borderTexture, position, Color.White);

            int innerX = (((int)position.X + 210) / 2) - (200 / 2);
            int innerY = (((int)position.Y + 30) / 2) - (30 / 2);
            Rectangle srect = new Rectangle((int)position.X + 5, (int)position.Y + 5, (int)((currentPower / maxPower) * 200), 30);
            Rectangle drect = new Rectangle((int)position.X + 5, (int)position.Y + 5, 200, 30);
            spriteBatch.Draw(powerUpTexture, srect, drect, Color.White);
        }
        #endregion

    }
}
