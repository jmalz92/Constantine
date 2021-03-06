﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Sprites
{
    class BulletSprite : Sprite
    {
        private static Random rand = new Random();
        private Texture2D Image;
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

			// delete bullets that go off-screen
            //if (!GameRoot.Viewport.Bounds.Contains(Position.ToPoint()))
            //    IsExpired = true;
		}

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            //spriteBatch.Draw(texture, position - camera.Position, animations[currentAnimation].CurrentFrameRect, Color.White);
            spriteBatch.Draw(Image, Position - camera.Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }

    }
}
