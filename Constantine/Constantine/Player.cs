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
        int elapsedTimed = 0;
       

        public Camera Camera
        {
            get { return camera; }
            set { camera = value; }
        }
       

        public Player(Game game)
        {
            gameRef = (Game1)game;
            camera = new Camera(gameRef.ScreenBounds);
        }

        public void Update(GameTime gameTime)
        {
            elapsedTimed += gameTime.ElapsedGameTime.Milliseconds;
            
            camera.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        
    }
}
