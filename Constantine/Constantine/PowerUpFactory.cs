using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using GameEngineLibrary.Sprites;

namespace Constantine
{
    /// <summary>
    /// Factory class to spawn power ups on to the map
    /// </summary>
    static class PowerUpFactory
    {
        static int _ultimatePowerTimer = 0;
        static int _speedPowerTimer = 0;

        public static void Update(GameTime gameTime, SpriteManager manager)
        {
            _ultimatePowerTimer += gameTime.ElapsedGameTime.Milliseconds;
            _speedPowerTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (_ultimatePowerTimer >= 20000)
            {
                manager.Add(new UltimatePowerUp(Assets.Tablet));
                _ultimatePowerTimer = 0;
            }

            if (_speedPowerTimer >= 30000)
            {
                manager.Add(new SpeedPowerUp(Assets.Bolt));
                _speedPowerTimer = 0;
            }
            
        }
    }
}
