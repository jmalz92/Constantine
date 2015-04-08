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

        #region Field Region
        Camera camera;
        Game1 gameRef;
        int health = 100;
        #endregion

        #region Property Region
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
        #endregion

        #region Constructor Region
        public Player(Game game)
        {
            gameRef = (Game1)game;
            camera = new Camera(gameRef.ScreenBounds);
        }
        #endregion

        #region Method Region
        public void Update(GameTime gameTime)
        {
            camera.Update(gameTime);
            UpdateHealth();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
        #endregion

        //Move to animated sprite class
        public void UpdateHealth()
        {
            if (InputHandler.KeyDown(Keys.H) && !InputHandler.LastKeyboardState.IsKeyDown(Keys.H))
            {
                this.Health += 5;
            }
            else if (InputHandler.KeyDown(Keys.K) && !InputHandler.LastKeyboardState.IsKeyDown(Keys.K))
            {
                this.Health -= 5;
            }

        }
    }
}
