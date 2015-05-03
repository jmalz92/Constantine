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

        const int POINT_AWARD = 10;
        int _damage;
        
        // Collision data
        int collisionOffset;
        
        // Framerate stuff
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 100;

        // Movement data
        protected float speed;

        public int Damage { get { return _damage; } }

        public EnemySprite(Texture2D textureImage, Texture2D deathImage, SoundEffect deathSound, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, float speed, int damage)
        {
            this.textureImage = textureImage;
            this._deathSound = deathSound;
            this._deathImage = deathImage;
            this.Position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this._damage = damage;
            this.millisecondsPerFrame = defaultMillisecondsPerFrame;
        }

        public void WasShot(PlayerSprite player, SpriteManager manager)
        {
            manager.Add(new EffectsSprite(_deathImage, Position, 10000));
            IsExpired = true;
            _deathSound.Play();
            player.AccumulatedPoints += POINT_AWARD;
            
        }

        public static EnemySprite CreateEnemy(Texture2D textureImage, Texture2D deathImage, SoundEffect deathSound, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, float speed, int damage)
        {
            return new EnemySprite(textureImage, deathImage, deathSound, position, frameSize, collisionOffset, currentFrame, sheetSize, speed, damage);
        }

        

        public override void Update(GameTime gameTime, Vector2 playerPos)
        {
            
            Vector2 movement = MoveSprite(playerPos);

            //TODO: Change to animation class implementation
            // Update frame if time to do so based on framerate
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // Increment to next frame
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                    currentFrame.X = 0;

                //move up
                if (movement.Y > .5)
                    currentFrame.Y = 0;

                //move down
                else if (movement.Y < -.5)
                    currentFrame.Y = 3;
                //move left
                else if (movement.X <= 0)
                    currentFrame.Y = 1;

                else
                    currentFrame.Y = 2;
               
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 destination = Position;
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

        protected Vector2 MoveSprite(Vector2 playerPos)
        {
            Vector2 movement = new Vector2();

            movement.X = playerPos.X - Position.X;
            movement.Y = playerPos.Y - Position.Y;

            movement.Normalize();
            Position += movement * speed;

            return movement;
        } 
       
        // Gets the collision rect based on position, framesize and collision offset
        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + collisionOffset,
                    (int)Position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }
    }
}
