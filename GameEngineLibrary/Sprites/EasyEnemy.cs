using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineLibrary.Sprites
{
    class EasyEnemy : EnemySprite
    {
        
        public EasyEnemy(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, float speed, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, collisionCueName)
        {
        }

        public EasyEnemy(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, float speed,
            int millisecondsPerFrame, string collisionCueName)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, collisionCueName)
        {
        }

        protected override void MoveSprite(Vector2 playerPos)
        {
            Vector2 movement = new Vector2();

            movement.X = playerPos.X - position.X;
            movement.Y = playerPos.Y - position.Y;

            movement.Normalize();
            position += movement * speed;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds, PlayerSprite player)
        {
            // Move sprite based on direction
            MoveSprite(player.Position);

            base.Update(gameTime, clientBounds, player);
        }


    }
}
