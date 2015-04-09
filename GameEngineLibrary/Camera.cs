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
    public class Camera
    {
        #region Field Region
        Vector2 position;
        float speed;
        float zoom;
        Rectangle viewportRectangle;
        #endregion

        #region Property Region
        public Vector2 Position
        {
            get { return position; }
            private set { position = value; }
        }

        public float Speed
        {
            get { return speed; }
            set
            {
                speed = (float)MathHelper.Clamp(speed, 1f, 16f);
            }
        }

        public float Zoom
        {
            get { return zoom; }
        }
        #endregion

        #region Constructor Region
        public Camera(Rectangle viewportRect)
        {
            speed = 4f;
            zoom = 1f;
            viewportRectangle = viewportRect;
        }

        public Camera(Rectangle viewportRect, Vector2 position)
        {
            speed = 4f;
            zoom = 1f;
            viewportRectangle = viewportRect;
            Position = position;
        }
        #endregion

        #region Method Region
        public void Update(GameTime gameTime)
        {
           
        }

        private void LockCamera()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - viewportRectangle.Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - viewportRectangle.Height);
        }

        public void LockToSprite(PlayerSprite sprite)
        {
            position.X = sprite.Position.X + sprite.Width / 2
            - (viewportRectangle.Width / 2);
            position.Y = sprite.Position.Y + sprite.Height / 2
            - (viewportRectangle.Height / 2);
            LockCamera();
        }
        #endregion
    }
}
