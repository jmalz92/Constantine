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
    static class EnemyFactory
    {
        static Random rng = new Random();
        static float inverseSpawnChance = 90;
        static int elapsedMilliseconds = 0;
        static bool spawnSideFlag = false;

        public static void Update(GameTime gameTime, SpriteManager manager)
        {
            elapsedMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedMilliseconds >= 1000)
            {

                if (spawnSideFlag)
                {
                    manager.Add(EasyEnemy.CreateEnemy(Assets.Zombie, Audio.Splat, new Vector2(0, rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0),new Point(3, 4), 2.0f, ""));

                    manager.Add(EasyEnemy.CreateEnemy(Assets.Skeleton, Audio.Splat, new Vector2(1280, rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.0f, ""));
                }
                else
                {

                    manager.Add(EasyEnemy.CreateEnemy(Assets.Zombie, Audio.Splat, new Vector2(rng.Next(1280), 0), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 1.0f, ""));

                    manager.Add(EasyEnemy.CreateEnemy(Assets.Skeleton, Audio.Splat, new Vector2(rng.Next(1280), 1280), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.0f, ""));
                }


                spawnSideFlag = !spawnSideFlag;
                elapsedMilliseconds = 0;
            }
            // slowly increase the spawn rate as time progresses
            //if (inverseSpawnChance > 30)
            //    inverseSpawnChance -= 0.005f;
        }


        public static void Reset()
        {
            inverseSpawnChance = 90;
        }
    }
}
