using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using GameEngineLibrary.TileEngine;

namespace GameEngineLibrary.Sprites
{
    /// <summary>
    /// Defines the main player
    /// </summary>
    public class PlayerSprite : Sprite
    {
        //too many fields, clean this up
        #region Field Region

        Dictionary<AnimationKey, Animation> animations;
        Dictionary<AnimationKey, Animation> ultimateAnimations;
        AnimationKey currentAnimation;
        bool isAnimating;

        Texture2D texture;
        Texture2D _bullet;
        Texture2D _ultimate;
        SoundEffect _bulletSound;
        SoundEffect _pickupSound;
        SoundEffect _ultimateSound;
        Vector2 _position;
        Vector2 _velocity;
        float _speed = 3.0f;
        int _collisionOffset = 10;
        const int _cooldownFrames = 6;
        int _cooldownRemaining = 0;
        int _accumulatedPoints = 0;
        int _health;
        int _healthTimer = 1000;
        bool _hasSpeedUp = false;
        bool _isTransformed = false;
        int _itemCount = 0;
        int _elapsedUltimateTime = 0;
        int _elapsedSpeedTime = 0;
        #endregion

        #region Property Region

        public AnimationKey CurrentAnimation
        {
            get { return currentAnimation; }
            set { currentAnimation = value; }
        }

        //TODO: rethink collision
        public bool IsColliding { get; set; }

        public bool IsAnimating
        {
            get { return isAnimating; }
            set { isAnimating = value; }
        }

        public int Width
        {
            get {
                if (_isTransformed)
                    return ultimateAnimations[currentAnimation].FrameWidth;
                else
                    return animations[currentAnimation].FrameWidth; 
            }
        }

        public int Height
        {
            get {
                if (_isTransformed)
                    return ultimateAnimations[currentAnimation].FrameHeight;
                else
                    return animations[currentAnimation].FrameHeight; 
            }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(_speed, 1.0f, 400.0f); }
        }

        public int Health
        {
            get { return _health; }
            set
            {
                _health = (int)MathHelper.Clamp(value, 0, 100);
            }
        }

        public int AccumulatedPoints
        {
            get { return _accumulatedPoints; }
            set { _accumulatedPoints = value; }
        }

        public int ItemCount
        {
            get { return _itemCount; }
            set { _itemCount = value; }
        }

        public bool IsTransformed
        {
            get { return _isTransformed; }
            set { _isTransformed = value; }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }

        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)_position.X + _collisionOffset,
                    (int)_position.Y + _collisionOffset,
                    Width - (_collisionOffset * 2),
                    Height - (_collisionOffset * 2));
            }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
                if (_velocity != Vector2.Zero)
                    _velocity.Normalize();
            }
        }

        #endregion

        #region Constructor Region

        public PlayerSprite(Texture2D sprite, Texture2D ultimate, Texture2D bullet, SoundEffect ultimateSound, SoundEffect bulletSound, SoundEffect pickupSound, 
            Dictionary<AnimationKey, Animation> animation, Dictionary<AnimationKey, Animation> ultimateAnimation)
        {
            texture = sprite;
            _bullet = bullet;
            _ultimate = ultimate;
            _ultimateSound = ultimateSound;
            _bulletSound = bulletSound;
            _pickupSound = pickupSound;
            animations = new Dictionary<AnimationKey, Animation>();
            ultimateAnimations = new Dictionary<AnimationKey, Animation>();
            _health = 100;

            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());

            foreach (AnimationKey key in ultimateAnimation.Keys)
                ultimateAnimations.Add(key, (Animation)ultimateAnimation[key].Clone());
        }

        #endregion

        #region Method Region

        //this isnt the best place to do bullet logic
        public void Update(GameTime gameTime, SpriteManager manager, Camera camera)
        {
            

            if (isAnimating && _isTransformed)
                ultimateAnimations[currentAnimation].Update(gameTime);
            else if(isAnimating)
                animations[currentAnimation].Update(gameTime);

            Vector2 aim = InputHandler.GetAimDirection(Position - camera.Position);

            if (aim.LengthSquared() > 0 && _cooldownRemaining <= 0 && 
                (InputHandler.MouseDown(InputHandler.MouseState.RightButton) || InputHandler.ButtonDown(Buttons.RightTrigger)))
            {
                _cooldownRemaining = _cooldownFrames;
                float aimAngle = (float)Math.Atan2(aim.Y, aim.X);
                Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

                //float randomSpread = rand.NextFloat(-0.04f, 0.04f) + rand.NextFloat(-0.04f, 0.04f);
                Vector2 vel = (11f * new Vector2((float)Math.Cos(aimAngle), (float)Math.Sin(aimAngle)));   

                Vector2 offset = Vector2.Transform(new Vector2(35, -8), aimQuat);
                manager.Add(new BulletSprite(_bullet, Position + offset, vel));

                offset = Vector2.Transform(new Vector2(35, 8), aimQuat);
                manager.Add(new BulletSprite(_bullet, Position + offset, vel));

                _bulletSound.Play(.2f, 0, 0);
            }

            if (_cooldownRemaining > 0)
                _cooldownRemaining--;

            UpdatePlayerStatus(gameTime);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            if(_isTransformed)
                spriteBatch.Draw(_ultimate, _position - camera.Position, ultimateAnimations[currentAnimation].CurrentFrameRect,  Color.White);
            else 
                spriteBatch.Draw(texture, _position - camera.Position, animations[currentAnimation].CurrentFrameRect, Color.White);
        }

        /// <summary>
        /// Updates all player attributes such as ultimate, speed, health, etc.
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdatePlayerStatus(GameTime gameTime)
        {
            _healthTimer--;
            if (_healthTimer <= 0)
            {
                _healthTimer = 1000;
                Health += 1;
            }

            if (_isTransformed)
            {
                _elapsedUltimateTime += gameTime.ElapsedGameTime.Milliseconds;
                if (_elapsedUltimateTime >= 10000)
                {
                    MediaPlayer.Resume();
                    _isTransformed = false;
                    _elapsedUltimateTime = 0;
                }
            }

            if (_hasSpeedUp)
            {
                _elapsedSpeedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (_elapsedSpeedTime >= 5000)
                {
                    _hasSpeedUp = false;
                    _elapsedSpeedTime = 0;
                    _speed = 3.0f;
                }
            }

            if (_itemCount >= 3)
            {
                _isTransformed = true;
                _itemCount = 0;
                MediaPlayer.Pause();
                _ultimateSound.Play();
                
            }
        }

        /// <summary>
        /// Takes damage based on the enemy
        /// </summary>
        /// <param name="enemy">the attacking enemy</param>
        public void TakeDamage(EnemySprite enemy)
        {
            Health -= enemy.Damage;
        }

        /// <summary>
        /// Logic to pick up power up
        /// </summary>
        /// <param name="sprite">the power up sprite to pick up</param>
        public void PickupPowerUp(PowerUpSprite sprite)
        {
            _pickupSound.Play();
            if (sprite is UltimatePowerUp)
                _itemCount++;
            if (sprite is SpeedPowerUp)
            {
                _speed = 6.0f;
                _hasSpeedUp = true;
            }
                
        }

        /// <summary>
        /// Locks the sprite within the screen bounds
        /// </summary>
        public void LockToMap()
        {
            _position.X = MathHelper.Clamp(_position.X, 0, TileMap.WidthInPixels - Width);
            _position.Y = MathHelper.Clamp(_position.Y, 0, TileMap.HeightInPixels - Height);
        }

        #endregion
    }
}
