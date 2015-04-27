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
    public class SpriteManager
    {
        List<Sprite> sprites = new List<Sprite>();
        List<EnemySprite> enemies = new List<EnemySprite>();
        List<BulletSprite> bullets = new List<BulletSprite>();
        List<PowerUpSprite> powerups = new List<PowerUpSprite>();
       
        bool isUpdating = false;
        List<Sprite> addedSprites = new List<Sprite>();

        public SpriteManager()
        {
        }

        public void Add(Sprite sprite)
        {
            if (!isUpdating)
                AddSprite(sprite);
            else
                addedSprites.Add(sprite);
        }

        private  void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
            if (sprite is BulletSprite)
                bullets.Add(sprite as BulletSprite);
            else if (sprite is EnemySprite)
                enemies.Add(sprite as EnemySprite);
            else if (sprite is PowerUpSprite)
                powerups.Add(sprite as PowerUpSprite);
            
        }

        public void Update(GameTime gameTime, PlayerSprite player)
        {
            isUpdating = true;
            HandleCollisions(player);

            foreach (var sprite in sprites)
                sprite.Update(gameTime, player.Position);

            isUpdating = false;

            foreach (var entity in addedSprites)
                AddSprite(entity);

            addedSprites.Clear();

            sprites = sprites.Where(x => !x.IsExpired).ToList();
            bullets = bullets.Where(x => !x.IsExpired).ToList();
            enemies = enemies.Where(x => !x.IsExpired).ToList();
            powerups = powerups.Where(x => !x.IsExpired).ToList();
        }

        private void HandleCollisions(PlayerSprite player)
        {
            
            // handle collisions between bullets and enemies
            for (int i = 0; i < enemies.Count; i++)
                for (int j = 0; j < bullets.Count; j++)
                {
                    if (IsColliding(enemies[i], bullets[j]))
                    {
                        enemies[i].WasShot(player);
                        bullets[j].IsExpired = true;
                    }
                }

            // handle collisions between the player and enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                if (IsColliding(player, enemies[i]))
                {
                    if (player.IsTransformed)
                        enemies[i].WasShot(player);
                    
                    else
                        player.IsColliding = true;
                }
            }

            // handle collisions between the player and powerups
            for (int i = 0; i < powerups.Count; i++)
            {
                if (IsColliding(player, powerups[i]))
                {
                    player.PickupPowerUp(powerups[i]);
                    powerups[i].IsExpired = true;
                }
            }

        }

        private static bool IsColliding(Sprite a, Sprite b)
        {
            return !a.IsExpired && !b.IsExpired && a.CollisionRect.Intersects(b.CollisionRect);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (var sprite in sprites)
                sprite.Draw(gameTime, spriteBatch, camera);
        }
    }
}
