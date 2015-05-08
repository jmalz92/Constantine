using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Sprites
{
    /// <summary>
    /// Projectile sprite
    /// </summary>
    class BulletSprite : Sprite
    {
        private static Random _rand = new Random();
        private Texture2D Image;
        private float _distance = 0;
        private const int _maxDistance = 400;

        public Vector2 Size
        {
            get
            {
                return Image == null ? Vector2.Zero : new Vector2(Image.Width, Image.Height);
            }
        }

        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + CollisionOffset,
                    (int)Position.Y + CollisionOffset,
                    Image.Width - (CollisionOffset * 2),
                    Image.Height - (CollisionOffset * 2));
            }
        }

		public BulletSprite(Texture2D image, Vector2 position, Vector2 velocity)
		{
            Image = image;
            Position = position;
            Velocity = velocity;
            Orientation = (float)Math.Atan2(Velocity.Y, Velocity.X);;
            CollisionOffset = 8;
		}

		public override void Update(GameTime gameTime, Vector2 playerPos)
		{
            if (Velocity.LengthSquared() > 0)
                Orientation = (float)Math.Atan2(Velocity.Y, Velocity.X);;

            Position += Velocity;
            _distance += Velocity.Length();

            if (_distance > _maxDistance)
                IsExpired = true;
		}

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(Image, Position - camera.Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }

    }
}
