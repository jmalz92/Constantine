using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameEngineLibrary;

namespace Constantine
{
    public class Player
    {

        Camera camera;
        Game1 gameRef;
        int health;
        int elapsedTimed = 0;
        int healthTimer = 1000;

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }
        public int Health
        {
            get { return health; }
            set
            {
                health = (int)MathHelper.Clamp(value, 0, 100);
            }
        }

        public Player(Game game)
        {
            gameRef = (Game1)game;
            camera = new Camera(gameRef.ScreenBounds);
            health = 100;
        }

        public void Update(GameTime gameTime)
        {
            elapsedTimed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedTimed >= healthTimer)
            {
                elapsedTimed = 0;
                Health += 1;
            }
            camera.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        
    }
}
