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
    /// <summary>
    /// Factor class to spawn enemies on to the map
    /// </summary>
    static class EnemyFactory
    {
        static Random _rng = new Random();
        static int _elapsedMilliseconds = 0;
        static bool _spawnSideFlag = false;

        public static void Update(GameTime gameTime, Difficulty difficulty, SpriteManager manager)
        {
            _elapsedMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

            if (_elapsedMilliseconds >= 1000)
            {
                SpawnEnemies(difficulty, manager);
                
                _elapsedMilliseconds = 0;
            }
           
        }

        //TODO: too many switch cases, refactor
        /// <summary>
        /// Spawns enemies based on difficulty
        /// </summary>
        /// <param name="difficulty"></param>
        /// <param name="manager"></param>
        private static void SpawnEnemies(Difficulty difficulty, SpriteManager manager)
        {
            if (_spawnSideFlag) //spawn enemies from sides
            {
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        manager.Add(EnemySprite.CreateEnemy(Assets.TreeMutant, Assets.Log, Audio.Splat, new Vector2(0, _rng.Next(1280)), new Point(50, 50), 10, new Point(0, 0), new Point(4, 4), 2.0f, 5));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Rabbit, Assets.Blood, Audio.Splat, new Vector2(1280, _rng.Next(1280)), new Point(32, 32), 10, new Point(0, 0), new Point(3, 4), 2.0f, 5));
                        break;
                    case Difficulty.Normal:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Zombie, Assets.Blood, Audio.Splat, new Vector2(0, _rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0),new Point(3, 4), 2.5f, 10));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Skeleton, Assets.Bone, Audio.Splat, new Vector2(1280, _rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.5f, 10));
                        break;
                    case Difficulty.Hard:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Devil, Assets.DemonBlood, Audio.Splat, new Vector2(0, _rng.Next(1280)), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 3.0f, 25));
                        manager.Add(EnemySprite.CreateEnemy(Assets.LavaMonster, Assets.DemonBlood, Audio.Splat, new Vector2(1280, _rng.Next(1280)), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 3.0f, 25));
                        break;
                }
            }
            else //spawn enemies from top/bottom
            {

                switch (difficulty)
                {
                    case Difficulty.Easy:
                        manager.Add(EnemySprite.CreateEnemy(Assets.TreeMutant, Assets.Log, Audio.Splat, new Vector2(_rng.Next(1280), 0), new Point(50, 50), 10, new Point(0, 0), new Point(4, 4), 2.0f, 5));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Rabbit, Assets.Blood, Audio.Splat, new Vector2(_rng.Next(1280), 1280), new Point(32, 32), 10, new Point(0, 0), new Point(3, 4), 2.0f, 5));
                        break;
                    case Difficulty.Normal:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Zombie, Assets.Blood, Audio.Splat, new Vector2(_rng.Next(1280), 0), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.0f, 10));
                        manager.Add(EnemySprite.CreateEnemy(Assets.Skeleton, Assets.Bone, Audio.Splat, new Vector2(_rng.Next(1280), 1280), new Point(32, 64), 10, new Point(0, 0), new Point(3, 4), 2.0f, 10));
                        break;
                    case Difficulty.Hard:
                        manager.Add(EnemySprite.CreateEnemy(Assets.Devil, Assets.DemonBlood, Audio.Splat, new Vector2(_rng.Next(1280), 0), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 2.0f, 25));
                        manager.Add(EnemySprite.CreateEnemy(Assets.LavaMonster, Assets.DemonBlood, Audio.Splat, new Vector2(_rng.Next(1280), 1280), new Point(96, 48), 10, new Point(0, 0), new Point(3, 4), 2.0f, 25));
                        break;
                }
            }

            _spawnSideFlag = !_spawnSideFlag;
            
        }
    }
}
