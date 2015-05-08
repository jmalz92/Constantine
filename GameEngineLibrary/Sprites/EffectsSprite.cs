using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Sprites
{
    /// <summary>
    /// Sprite used for special effects
    /// </summary>
    public class EffectsSprite : Sprite
    {
        private Texture2D Image;
        private int _duration = 0;
        private int _elapsedGameTime = 0;

        public EffectsSprite(Texture2D image, Vector2 position, int duration)
        {
            Image = image;
            Position = position;
            _duration = duration;
        }

        public override void Update(GameTime gameTime, Vector2 pos)
        {
            _elapsedGameTime += gameTime.ElapsedGameTime.Milliseconds;

            if (_elapsedGameTime >= _duration)
                IsExpired = true;
            
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
