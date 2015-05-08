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
    /// <summary>
    /// Holds and updates the player locked camera
    /// </summary>
    public class PlayerCamera
    {

        Camera _camera;
        Game1 gameRef;
        int elapsedTimed = 0;
       

        public Camera Camera
        {
            get { return _camera; }
            set { _camera = value; }
        }
       

        public PlayerCamera(Game game)
        {
            gameRef = (Game1)game;
            _camera = new Camera(gameRef.ScreenBounds);
        }

        public void Update(GameTime gameTime)
        {
            elapsedTimed += gameTime.ElapsedGameTime.Milliseconds;
            
            _camera.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }

        
    }
}
