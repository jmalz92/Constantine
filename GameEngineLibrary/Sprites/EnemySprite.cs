using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;


namespace GameEngineLibrary.Sprites
{
    /// <summary>
    /// Sprite used to define an enemy
    /// </summary>
    public class EnemySprite : Sprite
    {
         // Stuff needed to draw the sprite
        Texture2D textureImage;
        Texture2D _deathImage;
        SoundEffect _deathSound;
        protected Point _frameSize;
        Point _currentFrame;
        Point _sheetSize;

        const int POINT_AWARD = 10;
        int _damage;
        
        // Collision data
        int _collisionOffset;
        
        // Framerate stuff
        int _timeSinceLastFrame = 0;
        int _millisecondsPerFrame;
        const int _defaultMillisecondsPerFrame = 100;

        // Movement data
        protected float _speed;

        public int Damage { get { return _damage; } }

        public EnemySprite(Texture2D textureImage, Texture2D deathImage, SoundEffect deathSound, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, float speed, int damage)
        {
            this.textureImage = textureImage;
            this._deathSound = deathSound;
            this._deathImage = deathImage;
            this.Position = position;
            this._frameSize = frameSize;
            this._collisionOffset = collisionOffset;
            this._currentFrame = currentFrame;
            this._sheetSize = sheetSize;
            this._speed = speed;
            this._damage = damage;
            this._millisecondsPerFrame = _defaultMillisecondsPerFrame;
        }

        /// <summary>
        /// Logic for when an enemy is shot
        /// </summary>
        /// <param name="player">the player that shot the enemy</param>
        /// <param name="manager">sprite manager</param>
        public void WasShot(PlayerSprite player, SpriteManager manager)
        {
            manager.Add(new EffectsSprite(_deathImage, Position, 6000));
            IsExpired = true;
            _deathSound.Play();
            player.AccumulatedPoints += POINT_AWARD;
            
        }

        /// <summary>
        /// Returns a new enemy sprite
        /// </summary>
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
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > _millisecondsPerFrame)
            {
                // Increment to next frame
                _timeSinceLastFrame = 0;
                ++_currentFrame.X;
                if (_currentFrame.X >= _sheetSize.X)
                    _currentFrame.X = 0;

                //move up
                if (movement.Y > .5)
                    _currentFrame.Y = 0;

                //move down
                else if (movement.Y < -.5)
                    _currentFrame.Y = 3;
                //move left
                else if (movement.X <= 0)
                    _currentFrame.Y = 1;

                else
                    _currentFrame.Y = 2;
               
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
                new Rectangle(_currentFrame.X * _frameSize.X,
                    _currentFrame.Y * _frameSize.Y,
                    _frameSize.X, _frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Enemy Sprite movement logic
        /// </summary>
        /// <param name="playerPos"></param>
        /// <returns></returns>
        protected Vector2 MoveSprite(Vector2 playerPos)
        {
            Vector2 movement = new Vector2();

            movement.X = playerPos.X - Position.X;
            movement.Y = playerPos.Y - Position.Y;

            movement.Normalize();
            Position += movement * _speed;

            return movement;
        } 
       
        // Gets the collision rect based on position, framesize and collision offset
        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)Position.X + _collisionOffset,
                    (int)Position.Y + _collisionOffset,
                    _frameSize.X - (_collisionOffset * 2),
                    _frameSize.Y - (_collisionOffset * 2));
            }
        }
    }
}
