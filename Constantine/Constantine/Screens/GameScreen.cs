using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using GameEngineLibrary;
using GameEngineLibrary.TileEngine;
using GameEngineLibrary.Sprites;

namespace Constantine.Screens
{
    public class GameScreen : GameStateBase
    {
        TileEngine _tileEngine = new TileEngine(32, 32);
        TileSet _tileSet;
        TileMap _map;
        Player _player;
        AnimatedSprite _sprite;

        public GameScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
            _player = new Player(game);
        }

        public override void Initialize()
        {
            MediaPlayer.Stop(); //this needs to be refactored into a media handler class
            base.Initialize();
        }

        protected override void LoadContent()
        {

            Texture2D spriteSheet = Game.Content.Load<Texture2D>(@"Images\player_placeholder");
            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();
            Animation animation = new Animation(4, 32, 48, 0, 0);
            animations.Add(AnimationKey.Down, animation);
            animation = new Animation(4, 32, 48, 0, 48);
            animations.Add(AnimationKey.Left, animation);
            animation = new Animation(4, 32, 48, 0, 96);
            animations.Add(AnimationKey.Right, animation);
            animation = new Animation(4, 32, 48, 0, 144);
            animations.Add(AnimationKey.Up, animation);
            _sprite = new AnimatedSprite(spriteSheet, animations);

            base.LoadContent();
            
            Texture2D tilesetTexture = Game.Content.Load<Texture2D>(@"Tiles\tileset1");
            _tileSet = new TileSet(tilesetTexture, 8, 8, 32, 32);

            MapLayer layer = new MapLayer(40, 40);

            for (int y = 0; y < layer.Height; y++)
            {
                for (int x = 0; x < layer.Width; x++)
                {
                    Tile tile = new Tile(0, 0);
                    layer.SetTile(x, y, tile);
                }
            }

            _map = new TileMap(_tileSet, layer);

            MapLayer splatter = new MapLayer(40, 40);
            Random random = new Random();
            for (int i = 0; i < 80; i++)
            {
                int x = random.Next(0, 40);
                int y = random.Next(0, 40);
                int index = random.Next(2, 14);
                Tile tile = new Tile(index, 0);
                splatter.SetTile(x, y, tile);
            }
            _map.AddLayer(splatter);

            
        }
        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _sprite.Update(gameTime);

            AnimateSprite();

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                Matrix.Identity);

            _map.Draw(GameRef.SpriteBatch, _player.Camera);
            _sprite.Draw(gameTime, GameRef.SpriteBatch, _player.Camera);

            base.Draw(gameTime);

            GameRef.SpriteBatch.End();
        }

        private void AnimateSprite()
        {
            Vector2 motion = new Vector2();
            if (InputHandler.KeyDown(Keys.W) || InputHandler.ButtonDown(Buttons.LeftThumbstickUp, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.Up;
                motion.Y = -1;
            }
            else if (InputHandler.KeyDown(Keys.S) || InputHandler.ButtonDown(Buttons.LeftThumbstickDown, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.Down;
                motion.Y = 1;
            }
            if (InputHandler.KeyDown(Keys.A) || InputHandler.ButtonDown(Buttons.LeftThumbstickLeft, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.Left;
                motion.X = -1;
            }
            else if (InputHandler.KeyDown(Keys.D) || InputHandler.ButtonDown(Buttons.LeftThumbstickRight, PlayerIndex.One))
            {
                _sprite.CurrentAnimation = AnimationKey.Right;
                motion.X = 1;
            }
            if (motion != Vector2.Zero)
            {
                _sprite.IsAnimating = true;
                motion.Normalize();
                _sprite.Position += motion * _sprite.Speed;
                _sprite.LockToMap();
                _player.Camera.LockToSprite(_sprite);
            }
            else
            {
                _sprite.IsAnimating = false;
            }
        }
    }
}
