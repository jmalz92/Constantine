﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameEngineLibrary.TileEngine;

namespace GameEngineLibrary.Sprites
{
    public class PlayerSprite : Sprite
    {
        #region Field Region

        Dictionary<AnimationKey, Animation> animations;
        AnimationKey currentAnimation;
        bool isAnimating;

        Texture2D texture;
        Texture2D _bullet;
        Vector2 position;
        Vector2 velocity;
        float speed = 3.0f;
        int collisionOffset = 10;
        const int cooldownFrames = 6;
        int cooldowmRemaining = 0;
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
            get { return animations[currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return animations[currentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = MathHelper.Clamp(speed, 1.0f, 400.0f); }
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

        public PlayerSprite(Texture2D sprite, Texture2D bullet, Dictionary<AnimationKey, Animation> animation)
        {
            texture = sprite;
            _bullet = bullet;
            animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                animations.Add(key, (Animation)animation[key].Clone());
        }

        #endregion

        #region Method Region

        //this isnt the best place to do bullet logic
        public void Update(GameTime gameTime, SpriteManager manager)
        {
            if (isAnimating)
                animations[currentAnimation].Update(gameTime);

            Vector2 aim = InputHandler.GetAimDirection(Position);

            if (aim.LengthSquared() > 0 && cooldowmRemaining <= 0)
            {
                cooldowmRemaining = cooldownFrames;
                float aimAngle = (float)Math.Atan2(aim.Y, aim.X);
                Quaternion aimQuat = Quaternion.CreateFromYawPitchRoll(0, 0, aimAngle);

                //float randomSpread = rand.NextFloat(-0.04f, 0.04f) + rand.NextFloat(-0.04f, 0.04f);
                Vector2 vel = (11f * new Vector2((float)Math.Cos(aimAngle), (float)Math.Sin(aimAngle)));   

                Vector2 offset = Vector2.Transform(new Vector2(35, -8), aimQuat);
                manager.Add(new BulletSprite(_bullet, Position + offset, vel));

                offset = Vector2.Transform(new Vector2(35, 8), aimQuat);
                manager.Add(new BulletSprite(_bullet, Position + offset, vel));

                //Sound.Shot.Play(0.2f, rand.NextFloat(-0.2f, 0.2f), 0);
            }

            if (cooldowmRemaining > 0)
                cooldowmRemaining--;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(texture, position - camera.Position, animations[currentAnimation].CurrentFrameRect,  Color.White);

        }

        public void LockToMap()
        {
            position.X = MathHelper.Clamp(position.X, 0, TileMap.WidthInPixels - Width);
            position.Y = MathHelper.Clamp(position.Y, 0, TileMap.HeightInPixels - Height);
        }

        #endregion
    }
}
