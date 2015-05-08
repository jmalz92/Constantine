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
    /// <summary>
    /// Class to handle all interactions between sprites
    /// </summary>
    public class SpriteManager
    {
        List<Sprite> _sprites = new List<Sprite>();
        List<EnemySprite> _enemies = new List<EnemySprite>();
        List<BulletSprite> _bullets = new List<BulletSprite>();
        List<PowerUpSprite> _powerups = new List<PowerUpSprite>();
        List<EffectsSprite> _sfx = new List<EffectsSprite>();


        bool _isUpdating = false;
        List<Sprite> _addedSprites = new List<Sprite>();

        public SpriteManager()
        {
        }
        
        /// <summary>
        /// Adds a new sprite to the manager
        /// </summary>
        /// <param name="sprite"></param>
        public void Add(Sprite sprite)
        {
            if (!_isUpdating)
                AddSprite(sprite);
            else
                _addedSprites.Add(sprite);
        }

        /// <summary>
        /// Adds a new sprite to the appropriate sprite list
        /// </summary>
        /// <param name="sprite"></param>
        private void AddSprite(Sprite sprite)
        {
            _sprites.Add(sprite);
            if (sprite is BulletSprite)
                _bullets.Add(sprite as BulletSprite);
            else if (sprite is EnemySprite)
                _enemies.Add(sprite as EnemySprite);
            else if (sprite is PowerUpSprite)
                _powerups.Add(sprite as PowerUpSprite);
            else if (sprite is EffectsSprite)
                _sfx.Add(sprite as EffectsSprite);
            
        }

        public void Update(GameTime gameTime, PlayerSprite player)
        {
            _isUpdating = true;
            HandleCollisions(player);

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, player.Position);

            _isUpdating = false;

            foreach (var entity in _addedSprites)
                AddSprite(entity);

            _addedSprites.Clear();

            //Lambda functions to remove deleted sprites from the manager
            _sprites = _sprites.Where(x => !x.IsExpired).ToList();
            _bullets = _bullets.Where(x => !x.IsExpired).ToList();
            _enemies = _enemies.Where(x => !x.IsExpired).ToList();
            _powerups = _powerups.Where(x => !x.IsExpired).ToList();
            _sfx = _sfx.Where(x => !x.IsExpired).ToList();
        }

        /// <summary>
        /// Handles all collisions between sprites
        /// </summary>
        /// <param name="player"></param>
        private void HandleCollisions(PlayerSprite player)
        {
            
            // handle collisions between bullets and enemies
            for (int i = 0; i < _enemies.Count; i++)
                for (int j = 0; j < _bullets.Count; j++)
                {
                    if (IsColliding(_enemies[i], _bullets[j]))
                    {
                        _enemies[i].WasShot(player, this);
                        _bullets[j].IsExpired = true;
                    }
                }

            // handle collisions between the player and enemies
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (IsColliding(player, _enemies[i]))
                {
                    if (player.IsTransformed)
                        _enemies[i].WasShot(player, this);

                    else
                        player.TakeDamage(_enemies[i]);
                }
            }

            // handle collisions between the player and powerups
            for (int i = 0; i < _powerups.Count; i++)
            {
                if (IsColliding(player, _powerups[i]))
                {
                    player.PickupPowerUp(_powerups[i]);
                    _powerups[i].IsExpired = true;
                }
            }

        }

        /// <summary>
        /// Determins wether 2 sprites are colliding with eachother
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static bool IsColliding(Sprite a, Sprite b)
        {
            return !a.IsExpired && !b.IsExpired && a.CollisionRect.Intersects(b.CollisionRect);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch, camera);
        }
    }
}
