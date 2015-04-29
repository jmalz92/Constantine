using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;


namespace GameEngineLibrary.Sprites
{
    public class EnemySprite : Sprite
    {
         // Stuff needed to draw the sprite
        Texture2D textureImage;
        Texture2D _deathImage;
        SoundEffect _deathSound;
        protected Point frameSize;
        Point currentFrame;
        Point sheetSize;

        const int POINT_AWARD = 5;
        
        // Collision data
        int collisionOffset;
        
        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 100;

        // Movement data
        protected float speed;
        protected Vector2 position; //redundant
        
        public string collisionCueName { get; private set; }

        public EnemySprite(Texture2D textureImage, Texture2D deathImage, SoundEffect deathSound,  Vector2 position, Point frameSize,
        int collisionOffset, Point currentFrame, Point sheetSize, float speed, string collisionCueName)
            : this(textureImage, deathImage, deathSound, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, defaultMillisecondsPerFrame, collisionCueName)
        {
        }

        public EnemySprite(Texture2D textureImage, Texture2D deathImage, SoundEffect deathSound, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, float speed,
            int millisecondsPerFrame, string collisionCueName)
        {
            this.textureImage = textureImage;
            this._deathSound = deathSound;
            this._deathImage = deathImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.collisionCueName = collisionCueName;
            this.millisecondsPerFrame = millisecondsPerFrame;
        }

        public void WasShot(PlayerSprite player, SpriteManager manager)
        {
            manager.Add(new EffectsSprite(_deathImage, position, 10000));
            IsExpired = true;
            _deathSound.Play();
            player.AccumulatedPoints += POINT_AWARD;
            
        }

        public virtual void Update(GameTime gameTime)
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
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

        protected virtual void MoveSprite(Vector2 playerPos)
        {
        } 
       
        // Gets the collision rect based on position, framesize and collision offset
        public override Rectangle CollisionRect
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
