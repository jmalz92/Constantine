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
using GameEngineLibrary.Controls;

namespace Constantine.Screens
{
    public class GameScreen : GameStateBase
    {
        TileEngine _tileEngine = new TileEngine(32, 32);
        TileSet _tileSet;
        TileMap _map;
        Player _player;
        PlayerSprite _sprite;
        
        SpriteManager _spriteManager;

        ScoreLabel _scoreLabel;
        HealthBar _healthBar;
        int _scoreTimer;

        public int Difficulty { get; set; }
        
        public GameScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
            _player = new Player(game);
            _scoreTimer = 0;
        }

        public override void Initialize()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(Audio.HardTrack); 
            base.Initialize();
        }

        protected override void LoadContent()
        {

            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();
            Animation animation = new Animation(4, 32, 48, 0, 0);
            animations.Add(AnimationKey.Down, animation);
            animation = new Animation(4, 32, 48, 0, 48);
            animations.Add(AnimationKey.Left, animation);
            animation = new Animation(4, 32, 48, 0, 96);
            animations.Add(AnimationKey.Right, animation);
            animation = new Animation(4, 32, 48, 0, 144);
            animations.Add(AnimationKey.Up, animation);
            _sprite = new PlayerSprite(Assets.Player, Assets.Bullet, Audio.Bullet, animations);
            _sprite.Position = new Vector2(250, 250);

            _spriteManager = new SpriteManager();

            base.LoadContent();

            _scoreLabel = new ScoreLabel(new Vector2(780, 10));
            _healthBar = CreateHealthBar(this.GraphicsDevice);
            ControlManager.Add(_scoreLabel);

            ControlManager.Add(_healthBar);

            SetMap();
             
        }

        private void SetMap() 
        { 
            if (Difficulty == 0)
            {
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
            else if (Difficulty == 1)
            {
                Texture2D tilesetTexture = Game.Content.Load<Texture2D>(@"Tiles\cavetiles");
                _tileSet = new TileSet(tilesetTexture, 5, 5, 32, 32);

                MapLayer layer = new MapLayer(40, 40);

                for (int y = 0; y < layer.Height; y++)
                {
                    for (int x = 0; x < layer.Width; x++)
                    {
                        Tile tile = new Tile(3, 0);
                        layer.SetTile(x, y, tile);
                    }
                }

                _map = new TileMap(_tileSet, layer);
            }
            else
            {
                Texture2D tilesetTexture = Game.Content.Load<Texture2D>(@"Tiles\scorchedtiles");
                _tileSet = new TileSet(tilesetTexture, 1, 1, 32, 32);

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
            }
        }


        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _sprite.Update(gameTime, _spriteManager);
            EnemyFactory.Update(gameTime, _spriteManager);
            _spriteManager.Update(gameTime, _sprite);
            AnimateSprite();
            ControlManager.Update(gameTime);
            _healthBar.UpdatePlayerHealth(_player.Health);

            if (_sprite.IsColliding)
            {
                _player.Health -= 5;
                _sprite.IsColliding = false;
            }


            _scoreTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (_scoreTimer >= 1000)
            {
                _scoreLabel.UpdateScore(1);
                _scoreTimer = 0;
            }

            _scoreLabel.UpdateScore(_sprite.AccumulatedPoints);
            _sprite.AccumulatedPoints = 0;

            if (InputHandler.KeyPressed(Keys.Escape) || InputHandler.ButtonPressed(Buttons.Start))
            {
                GameRef._stateHandler.PushState(GameRef._pauseScreen);
            }

            if (_healthBar.IsEmpty)
            {
                GameRef._stateHandler.PushState(GameRef._gameOverScreen);
            }

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
            _spriteManager.Draw(gameTime, GameRef.SpriteBatch, _player.Camera);
            

            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        private void AnimateSprite()
        {
            Vector2 motion = new Vector2();
            if (InputHandler.KeyDown(Keys.W) || InputHandler.ButtonDown(Buttons.LeftThumbstickUp))
            {
                _sprite.CurrentAnimation = AnimationKey.Up;
                motion.Y = -1;
            }
            else if (InputHandler.KeyDown(Keys.S) || InputHandler.ButtonDown(Buttons.LeftThumbstickDown))
            {
                _sprite.CurrentAnimation = AnimationKey.Down;
                motion.Y = 1;
            }
            if (InputHandler.KeyDown(Keys.A) || InputHandler.ButtonDown(Buttons.LeftThumbstickLeft))
            {
                _sprite.CurrentAnimation = AnimationKey.Left;
                motion.X = -1;
            }
            else if (InputHandler.KeyDown(Keys.D) || InputHandler.ButtonDown(Buttons.LeftThumbstickRight))
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

        //Do not supply the graphics device as a parameter, any other alternatives for health bars?
        public HealthBar CreateHealthBar(GraphicsDevice gd)
        {
            int width = 210;
            int height = 42;
            int innerWidth = 200;
            int innerHeight = 30;

            HealthBar healthBar = new HealthBar(_player.Health, 0, 0);

            //create the textures
            Texture2D healthTexture = new Texture2D(gd, innerWidth, innerHeight, false, SurfaceFormat.Color);
            Texture2D borderTexture = new Texture2D(gd, width, height, false, SurfaceFormat.Color);

            //create the outer portion of the textbar texture
            Color[] backColor = new Color[width * height];
            for (int i = 0; i < backColor.Length; i++)
            {
                backColor[i] = new Color(200, 100, 50);
            }
            borderTexture.SetData(backColor);

            //create the inner portion of the textbar texture
            Color[] textureColor = new Color[innerWidth * innerHeight];
            for (int i = 0; i < textureColor.Length; i++)
            {
                textureColor[i] = new Color(255, 0, 0);
            }
            healthTexture.SetData(textureColor);

            //set the textures in the health bar
            healthBar.BorderTexture = borderTexture;
            healthBar.HealthTexture = healthTexture;
            //return newly created health bar
            return healthBar;
        }
    }
}
