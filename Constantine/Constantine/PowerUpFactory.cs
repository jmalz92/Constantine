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

    static class PowerUpFactory
    {
        static int ultimatePowerTimer = 0;
        static int speedPowerTimer = 0;

        public static void Update(GameTime gameTime, SpriteManager manager)
        {
            ultimatePowerTimer += gameTime.ElapsedGameTime.Milliseconds;
            speedPowerTimer += gameTime.ElapsedGameTime.Milliseconds;

            if (ultimatePowerTimer >= 20000)
            {
                manager.Add(new PowerUpSprite(Assets.Tablet));
                ultimatePowerTimer = 0;
            }

            if (speedPowerTimer >= 30000)
            {
                manager.Add(new PowerUpSprite(Assets.Bolt));
                speedPowerTimer = 0;
            }
            // slowly increase the spawn rate as time progresses
            //if (inverseSpawnChance > 30)
            //    inverseSpawnChance -= 0.005f;
        }
    }
}
