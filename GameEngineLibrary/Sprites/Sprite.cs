using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Sprites
{
    /// <summary>
    /// Sprite base class
    /// </summary>
    public abstract class Sprite
    {
        // The tint of the image. This will also allow us to change the transparency.
        protected Color color = Color.White;

        public Vector2 Position, Velocity;
        public float Orientation;
        public bool IsExpired;		// true if the entity was destroyed and should be deleted.
        public int CollisionOffset;

        public virtual Rectangle CollisionRect { get; set; }

        public virtual void Update(GameTime gameTime, Vector2 playerPos)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
           
        }
    }
}
