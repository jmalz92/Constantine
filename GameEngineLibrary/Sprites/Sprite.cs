using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Sprites
{
    public abstract class Sprite
    {
        // The tint of the image. This will also allow us to change the transparency.
        protected Color color = Color.White;

        public Vector2 Position, Velocity;
        public float Orientation;
        public bool IsExpired;		// true if the entity was destroyed and should be deleted.
        public int CollisionOffset;

        public virtual Rectangle CollisionRect { get; set; }

        // Gets the collision rect based on position, framesize and collision offset
        //public Rectangle collisionRect
        //{
        //    get
        //    {
        //        return new Rectangle(
        //            (int)Position.X + CollisionOffset,
        //            (int)Position.Y + CollisionOffset,
        //            frameSize.X - (CollisionOffset * 2),
        //            frameSize.Y - (CollisionOffset * 2));
        //    }
        //}
        
        public virtual void Update(GameTime gameTime, Vector2 playerPos)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
           
        }
    }
}
