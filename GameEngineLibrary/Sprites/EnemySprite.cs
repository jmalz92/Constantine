using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameEngineLibrary.Sprites
{
    abstract class EnemySprite
    {
         // Stuff needed to draw the sprite
        Texture2D textureImage;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;
        
        // Collision data
        int collisionOffset;
        
        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 100;

        // Movement data
        protected Vector2 speed;
        protected Vector2 position;

        // Abstract definition of direction property
        public abstract Vector2 direction
        {
            get;
        }

        public string collisionCueName { get; private set; }

        public EnemySprite(Texture2D textureImage, Vector2 position, Point frameSize,
        int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, string collisionCueName)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame, collisionCueName)
        {
        }

        public EnemySprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, string collisionCueName)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.collisionCueName = collisionCueName;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //TODO: Change to animation class implementation
            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 destination = position;
            destination.X -= (int)camera.Position.X;
            destination.Y -= (int)camera.Position.Y;

            // Draw the sprite
            spriteBatch.Draw(textureImage,
                destination,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

        // Gets the collision rect based on position, framesize and collision offset
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }
    }
}
