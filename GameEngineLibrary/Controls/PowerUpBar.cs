using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngineLibrary.Controls
{
    /// <summary>
    /// Power up bar control
    /// </summary>
    public class PowerUpBar : Control
    {

        #region Fields and Properties
        Texture2D _powerUpTexture;
        Texture2D _borderTexture;
        Label _label;
        float _currentPower = 0;
        float _maxPower = 100;
        float _powerStep = (100.0f / 3.0f);
        bool _powerUpComplete = false;
        int _elapsedGameTime = 0;
        

        public Texture2D PowerUpTexture
        {
            get { return _powerUpTexture; }
            set { _powerUpTexture = value; }
        }
        public Texture2D BorderTexture
        {
            get { return _borderTexture; }
            set { _borderTexture = value; }
        }
        public bool IsPowerUpComplete
        {
            get { return _powerUpComplete; }
        }
        #endregion

        #region Constructor
        public PowerUpBar(int positionX, int positionY)
        {
            position.X = positionX;
            position.Y = positionY;
            tabStop = false;
            _label = new Label();
            _label.Text = "Power";
            _label.Color = Color.White;
            _label.Position = new Vector2(this.position.X + 70, this.Position.Y);
            
        }
        #endregion

        #region Methods

        /// <summary>
        /// Increases the power up bar based on number of ultimate items held
        /// </summary>
        /// <param name="ItemCount"></param>
        public void IncreasePowerUpBar(int ItemCount)
        {
            if (!_powerUpComplete)
            {
                _currentPower = ItemCount * _powerStep;
                if (_currentPower >= 100)
                {
                    _powerUpComplete = true;
                }
            }
        }

        /// <summary>
        /// Slowly decreases the powerup bar over time when full
        /// </summary>
        public void DecreasePowerUpBar()
        {
            if (_powerUpComplete)
            {
                _currentPower--;
                if (_currentPower <= 0)
                {
                    _powerUpComplete = false;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            _elapsedGameTime += gameTime.ElapsedGameTime.Milliseconds;
            if (_elapsedGameTime >= 100)
            {
                _elapsedGameTime = 0;
                if (_powerUpComplete)
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
            
            spriteBatch.Draw(_borderTexture, position, Color.White);
            int innerX = (((int)position.X + 210) / 2) - (200 / 2);
            int innerY = (((int)position.Y + 30) / 2) - (30 / 2);
            Rectangle srect = new Rectangle((int)position.X + 5, (int)position.Y + 5, (int)((_currentPower / _maxPower) * 200), 30);
            Rectangle drect = new Rectangle((int)position.X + 5, (int)position.Y + 5, 200, 30);
            spriteBatch.Draw(_powerUpTexture, srect, drect, Color.White);

            _label.Draw(spriteBatch);
        }
        #endregion

    }
}
