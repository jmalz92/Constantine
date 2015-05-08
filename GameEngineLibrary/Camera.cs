using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameEngineLibrary.TileEngine;
using GameEngineLibrary.Sprites;

namespace GameEngineLibrary
{
    /// <summary>
    /// Camera class that locks to a players position
    /// </summary>
    public class Camera
    {
        #region Field Region
        Vector2 _position;
        float _speed;
        float _zoom;
        Rectangle _viewportRectangle;
        #endregion

        #region Property Region
        public Vector2 Position
        {
            get { return _position; }
            private set { _position = value; }
        }

        public float Speed
        {
            get { return _speed; }
            set
            {
                _speed = (float)MathHelper.Clamp(_speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return _zoom; }
        }
        #endregion

        #region Constructor Region
        public Camera(Rectangle viewportRect)
        {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRect;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            _speed = 4f;
            _zoom = 1f;
            _viewportRectangle = viewportRect;
            Position = position;
        }
        #endregion

        #region Method Region
        public void Update(GameTime gameTime)
        {
           
        }

        /// <summary>
        /// Locks the game camera in position
        /// </summary>
        private void LockCamera()
        {
            _position.X = MathHelper.Clamp(_position.X, 0, TileMap.WidthInPixels - _viewportRectangle.Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, TileMap.HeightInPixels - _viewportRectangle.Height);
        }

        /// <summary>
        /// Locks the camera to the players position
        /// </summary>
        /// <param name="sprite">the player sprite to lock to</param>
        public void LockToSprite(PlayerSprite sprite)
        {
            _position.X = sprite.Position.X + sprite.Width / 2
            - (_viewportRectangle.Width / 2);
            _position.Y = sprite.Position.Y + sprite.Height / 2
            - (_viewportRectangle.Height / 2);
            LockCamera();
        }
        #endregion
    }
}
