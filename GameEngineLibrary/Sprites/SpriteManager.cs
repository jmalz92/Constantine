using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngineLibrary.Sprites
{
    public class SpriteManager : DrawableGameComponent
    {
        Random rng = new Random();

        int elapsedMilliseconds;
        bool spawnSideFlag = false;


        List<EnemySprite> spriteList = new List<EnemySprite>();

        public SpriteManager(Game game)
            : base(game)
        {

            elapsedMilliseconds = 0;

        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime, PlayerSprite player)
        {
            elapsedMilliseconds += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedMilliseconds >= 1000)
            {

                if (spawnSideFlag)
                {
                    spriteList.Add(new EasyEnemy(
                    Game.Content.Load<Texture2D>(@"Images/zombie"),
                    new Vector2(0, rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0),
                    new Point(3, 4), 2.0f, "enemycollision"));

                    spriteList.Add(new EasyEnemy(
                    Game.Content.Load<Texture2D>(@"Images/skeleton"),
                    new Vector2(1280, rng.Next(1280)), new Point(32, 64), 10, new Point(0, 0),
                    new Point(3, 4), 2.0f, "enemycollision"));
                }
                else
                {
                   
                    spriteList.Add(new EasyEnemy(
                    Game.Content.Load<Texture2D>(@"Images/zombie"),
                    new Vector2(rng.Next(1280), 0), new Point(32, 64), 10, new Point(0, 0),
                    new Point(3, 4), 1.0f, "enemycollision"));

                    spriteList.Add(new EasyEnemy(
                    Game.Content.Load<Texture2D>(@"Images/skeleton"),
                    new Vector2(rng.Next(1280), 1280), new Point(32, 64), 10, new Point(0,0),
                    new Point(3, 4), 2.0f, "enemycollision"));
                }


                spawnSideFlag = !spawnSideFlag;
                elapsedMilliseconds = 0;
            }

            // Update all sprites
            foreach (EnemySprite s in spriteList)
            {
                s.Update(gameTime, Game.Window.ClientBounds, player);

                
                // Check for collisions and exit game if there is one
                if (s.collisionRect.Intersects(player.collisionRect))
                {
                    //Possibly create a collision object to provide collision info on collision?
                    player.IsColliding = true;
                    
                }
                
            }

           
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            // Draw all sprites
            foreach (EnemySprite s in spriteList)
                s.Draw(gameTime, spriteBatch, camera);

        }
    }
}
