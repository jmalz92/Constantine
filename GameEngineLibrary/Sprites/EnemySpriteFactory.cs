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
    public class EnemySpriteFactory : DrawableGameComponent
    {
        Random rng = new Random();

        
        List<EnemySprite> spriteList = new List<EnemySprite>();

        public EnemySpriteFactory(Game game)
            : base(game)
        {
            spriteList.Add(new EasyEnemy(
                Game.Content.Load<Texture2D>(@"Images/zombie"),
                new Vector2(150, 150), new Point(32, 64), 10, new Point(0, 0),
                new Point(3, 4), Vector2.Zero, "enemycollision"));

            spriteList.Add(new EasyEnemy(
               Game.Content.Load<Texture2D>(@"Images/skeleton"),
               new Vector2(250, 150), new Point(32, 64), 10, new Point(0, 0),
               new Point(3, 4), Vector2.Zero, "enemycollision"));

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
            // Update all sprites
            foreach (EnemySprite s in spriteList)
            {
                s.Update(gameTime, Game.Window.ClientBounds);

                
                // Check for collisions and exit game if there is one
                if (s.collisionRect.Intersects(player.collisionRect))
                {
                    //if (s.collisionCueName != null) ((Game1)Game).PlayCue(s.collisionCueName);
                    Game.Exit();
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
