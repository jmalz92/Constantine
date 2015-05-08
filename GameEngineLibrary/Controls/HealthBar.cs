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
    /// Health bar control
    /// </summary>
    public class HealthBar : Control
    {
        #region Fields and Properties
        int _width;
        int _height;
        Texture2D _healthTexture;
        Texture2D _borderTexture;
        Label _label;
        int _maxHealth;
        int _playerHealth;

        public int ChangeUpperHealth
        {
            set { _maxHealth = value; }
        }

        public Texture2D HealthTexture
        {
            get { return _healthTexture; }
            set { _healthTexture = value; }
        }
        public Texture2D BorderTexture
        {
            get { return _borderTexture; }
            set { _borderTexture = value; }
        }

        public bool IsEmpty
        {
            get { return _playerHealth <= 0; }
        }
        #endregion

        #region Constructor Region
        public HealthBar(int initialHealth, int posX, int posY)
        {
            _maxHealth = initialHealth;
            position.X = posX;
            position.Y = posY;
            tabStop = false;

            _label = new Label();
            _label.Text = "Health";
            _label.Color = Color.White;
            _label.Position = new Vector2(this.position.X + 60, this.Position.Y);
        }
        #endregion

        #region Method Region
        
        /// <summary>
        /// Updates the players health
        /// </summary>
        /// <param name="health">the new health value</param>
        public void UpdatePlayerHealth(int health)
        {
            _playerHealth = health;
        }
        
        public override void Update(GameTime gameTime)
        {
        }

        public override void HandleInput()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_borderTexture, position, Color.White);

            int innerX = (((int)position.X + 210) / 2) - (200 / 2);
            int innerY = (((int)position.Y + 30) / 2) - (30 / 2);
            Rectangle srect = new Rectangle((int)position.X + 5, (int)position.Y + 5, (int)(((double)_playerHealth / _maxHealth) * 200), 30);
            Rectangle drect = new Rectangle((int)position.X + 5, (int)position.Y + 5, 200, 30);
            spriteBatch.Draw(_healthTexture, srect, drect, Color.White);

            _label.Draw(spriteBatch);
        }        
        #endregion
    }
}
