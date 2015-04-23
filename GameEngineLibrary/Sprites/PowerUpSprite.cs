using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using GameEngineLibrary.TileEngine;

namespace GameEngineLibrary.Sprites
{
    public class PowerUpSprite : Sprite
    {
        private static Random rand = new Random();
        private Texture2D Image;

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

        public PowerUpSprite(Texture2D image)
        {
            Image = image;
            Position = GetSpawnPosition();
        }

        private Vector2 GetSpawnPosition()
        {
            Vector2 pos = Vector2.Zero;

            pos.X = (float)(TileMap.WidthInPixels * rand.NextDouble());
            pos.Y = (float)(TileMap.HeightInPixels * rand.NextDouble());

            return pos;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 destination = Position;
            destination.X -= (int)camera.Position.X;
            destination.Y -= (int)camera.Position.Y;

            // Draw the sprite
            spriteBatch.Draw(Image, destination, Color.White);
        }

    }
}
