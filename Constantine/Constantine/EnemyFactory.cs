using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using Constantine.Screens;

using GameEngineLibrary.Sprites;

namespace Constantine
{
    static class EnemyFactory
    {
        static Random rng = new Random();
        static float inverseSpawnChance = 90;
        static int elapsedMilliseconds = 0;
        static bool spawnSideFlag = false;

        public static void Update(GameTime gameTime, Difficulty difficulty, SpriteManager manager)
        {
            elapsedMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedMilliseconds >= 1000)
            {
                SpawnEnemies(difficulty, manager);
                
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

        //TODO: too many switch cases
        private static void SpawnEnemies(Difficulty difficulty, SpriteManager manager)
        {
            if (spawnSideFlag)
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        manager.Add(EnemySprite.CreateEnemy(Assets.TreeMutant, Assets.Blood, Audio.Splat, new Vector2(0, rng.Next(1280)), new Point(50, 50), 10, new Point(0, 0), new Point(4, 4), 2.0f, 5));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Rabbit, Assets.Blood, Audio.Splat, new Vector2(1280, rng.Next(1280)), new Point(32, 32), 10, new Point(0, 0), new Point(3, 4), 2.0f, 5));
                        break;
                    case Difficulty.Normal:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Zombie, Assets.Blood, Audio.Splat, new Vector2(0, rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0),new Point(3, 4), 2.5f, 10));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Skeleton, Assets.Blood, Audio.Splat, new Vector2(1280, rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.5f, 10));
                        break;
                    case Difficulty.Hard:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Devil, Assets.Blood, Audio.Splat, new Vector2(0, rng.Next(1280)), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 3.0f, 25));
                        manager.Add(EnemySprite.CreateEnemy(Assets.LavaMonster, Assets.Blood, Audio.Splat, new Vector2(1280, rng.Next(1280)), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 3.0f, 25));
                        break;
                }
            }
            else
            {

                switch (difficulty)
                {
                    case Difficulty.Easy:
                        manager.Add(EnemySprite.CreateEnemy(Assets.TreeMutant, Assets.Blood, Audio.Splat, new Vector2(rng.Next(1280), 0), new Point(50, 50), 10, new Point(0, 0), new Point(4, 4), 2.0f, 5));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Rabbit, Assets.Blood, Audio.Splat, new Vector2(rng.Next(1280), 1280), new Point(32, 32), 10, new Point(0, 0), new Point(3, 4), 2.0f, 5));
                        break;
                    case Difficulty.Normal:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Zombie, Assets.Blood, Audio.Splat, new Vector2(rng.Next(1280), 0), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.0f, 10));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Skeleton, Assets.Blood, Audio.Splat, new Vector2(rng.Next(1280), 1280), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.0f, 10));
                        break;
                    case Difficulty.Hard:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Devil, Assets.Blood, Audio.Splat, new Vector2(rng.Next(1280), 0), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 2.0f, 25));
                        manager.Add(EnemySprite.CreateEnemy(Assets.LavaMonster, Assets.Blood, Audio.Splat, new Vector2(rng.Next(1280), 1280), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 2.0f, 25));
                        break;
                }
            }

            spawnSideFlag = !spawnSideFlag;
            
        }
    }
}
