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
        Vector2 position;
        Vector2 velocity;
        float speed = 3.0f;
        int collisionOffset = 10;
        const int cooldownFrames = 6;
        int cooldownRemaining = 0;
        int accumulatedPoints = 0;
        int health;
        int healthTimer = 1000;
        bool hasSpeedUp = false;
        bool isTransformed = false;
        int itemCount = 0;
        int elapsedUltimateTime = 0;
        int elapsedSpeedTime = 0;
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
                if (isTransformed)
                    return ultimateAnimations[currentAnimation].FrameWidth;
                else
                    return animations[currentAnimation].FrameWidth; 
            }
        }

        public int Height
        {
            get {
                if (isTransformed)
                    return ultimateAnimations[currentAnimation].FrameHeight;
                else
                    return animations[currentAnimation].FrameHeight; 
            }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 400.0f); }
        }

        public int Health
        {
            get { return health; }
            set
            {
                health = (int)MathHelper.Clamp(value, 0, 100);
            }
        }

        public int AccumulatedPoints
        {
            get { return accumulatedPoints; }
            set { accumulatedPoints = value; }
        }

        public int ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }

        public bool IsTransformed
        {
            get { return isTransformed; }
            set { isTransformed = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public override Rectangle CollisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    Width - (collisionOffset * 2),
                    Height - (collisionOffset * 2));
            }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set
            {
                velocity = value;
                if (velocity != Vector2.Zero)
                    velocity.Normalize();
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
            health = 100;

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
            

            if (isAnimating && isTransformed)
                ultimateAnimations[currentAnimation].Update(gameTime);
            else if(isAnimating)
                animations[currentAnimation].Update(gameTime);

            Vector2 aim = InputHandler.GetAimDirection(Position - camera.Position);

            if (aim.LengthSquared() > 0 && cooldownRemaining <= 0 && 
                (InputHandler.MouseDown(InputHandler.MouseState.RightButton) || InputHandler.ButtonDown(Buttons.RightTrigger)))
            {
                cooldownRemaining = cooldownFrames;
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

            if (cooldownRemaining > 0)
                cooldownRemaining--;

            UpdatePlayerStatus(gameTime);

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            if(isTransformed)
                spriteBatch.Draw(_ultimate, position - camera.Position, ultimateAnimations[currentAnimation].CurrentFrameRect,  Color.White);
            else 
                spriteBatch.Draw(texture, position - camera.Position, animations[currentAnimation].CurrentFrameRect, Color.White);
        }

        public void UpdatePlayerStatus(GameTime gameTime)
        {
            healthTimer--;
            if (healthTimer <= 0)
            {
                healthTimer = 1000;
                Health += 1;
            }

            if (isTransformed)
            {
                elapsedUltimateTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedUltimateTime >= 10000)
                {
                    MediaPlayer.Resume();
                    isTransformed = false;
                    elapsedUltimateTime = 0;
                }
            }

            if (hasSpeedUp)
            {
                elapsedSpeedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedSpeedTime >= 5000)
                {
                    hasSpeedUp = false;
                    elapsedSpeedTime = 0;
                    speed = 3.0f;
                }
            }

            if (itemCount >= 3)
            {
                isTransformed = true;
                itemCount = 0;
                MediaPlayer.Pause();
                _ultimateSound.Play();
                
            }
        }

        public void TakeDamage(EnemySprite enemy)
        {
            Health -= enemy.Damage;
        }

        public void PickupPowerUp(PowerUpSprite sprite)
        {
            _pickupSound.Play();
            if (sprite is UltimatePowerUp)
                itemCount++;
            if (sprite is SpeedPowerUp)
            {
                speed = 6.0f;
                hasSpeedUp = true;
            }
                
        }

        public void LockToMap()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - Height);
        }

        #endregion
    }
}
