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

    /// <summary>
    /// Main Game screen to display
    /// </summary>
    public class GameScreen : GameStateBase
    {
        TileEngine _tileEngine = new TileEngine(32, 32);
        TileSet _tileSet;
        TileMap _map;
        PlayerCamera _playerCamera;
        PlayerSprite _sprite;
        
        SpriteManager _spriteManager;

        ScoreLabel _scoreLabel;
        HealthBar _healthBar;
        PowerUpBar _powersBar;

        int _scoreTimer;

        public Difficulty CurrentDifficulty { get; set; }
        
        public GameScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
            _playerCamera = new PlayerCamera(game);
            _scoreTimer = 0;
        }

        public override void Initialize()
        {
            MediaPlayer.Stop();

            switch (CurrentDifficulty)
            {
                case Difficulty.Easy:
                    MediaPlayer.Play(Audio.EasyTrack); 
                    break;
                case Difficulty.Normal:
                    MediaPlayer.Play(Audio.NormalTrack); 
                    break;
                case Difficulty.Hard:
                    MediaPlayer.Play(Audio.HardTrack); 
                    break;
            }
            
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

            Dictionary<AnimationKey, Animation> ultimateAnimations = new Dictionary<AnimationKey, Animation>();
            Animation ultimateAnimation = new Animation(4, 80, 64, 0, 0);
            ultimateAnimations.Add(AnimationKey.Down, ultimateAnimation);
            ultimateAnimation = new Animation(4, 80, 64, 0, 64);
            ultimateAnimations.Add(AnimationKey.Left, ultimateAnimation);
            ultimateAnimation = new Animation(4, 80, 64, 0, 128);
            ultimateAnimations.Add(AnimationKey.Right, ultimateAnimation);
            ultimateAnimation = new Animation(4, 80, 64, 0, 192);
            ultimateAnimations.Add(AnimationKey.Up, ultimateAnimation);

            _sprite = new PlayerSprite(Assets.Player, Assets.Ultimate, Assets.Bullet, Audio.Ultimate, Audio.Bullet, Audio.Pickup, animations, ultimateAnimations);
            _sprite.Position = new Vector2(250, 250);

            _spriteManager = new SpriteManager();

            base.LoadContent();

            _scoreLabel = new ScoreLabel(new Vector2(780, 10));
            _healthBar = CreateHealthBar(this.GraphicsDevice);
            _powersBar = CreatePowerUpBar(this.GraphicsDevice);
            ControlManager.Add(_scoreLabel);

            ControlManager.Add(_healthBar);
            ControlManager.Add(_powersBar);
            SetMap();
             
        }

        /// <summary>
        /// Loads a map based on the selected difficulty
        /// </summary>
        private void SetMap() 
        { 
            if (CurrentDifficulty == Difficulty.Easy)
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
                for (int i = 0; i < 60; i++)
                {
                    int x = random.Next(0, 40);
                    int y = random.Next(0, 40);
                    int index = random.Next(2, 14);
                    Tile tile = new Tile(index, 0);
                    splatter.SetTile(x, y, tile);
                }
                _map.AddLayer(splatter);
            }
            else if (CurrentDifficulty == Difficulty.Normal)
            {
                Texture2D tilesetTexture = Game.Content.Load<Texture2D>(@"Tiles\cavetiles");
                _tileSet = new TileSet(tilesetTexture, 4, 1, 32, 32);

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

                MapLayer splatter = new MapLayer(40, 40);
                Random random = new Random();
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(0, 40);
                    int y = random.Next(0, 40);
                    int index = random.Next(1, 4);
                    Tile tile = new Tile(index, 0);
                    splatter.SetTile(x, y, tile);
                }
                _map.AddLayer(splatter);
            }
            else
            {
                Texture2D tilesetTexture = Game.Content.Load<Texture2D>(@"Tiles\scorchedtiles");
                _tileSet = new TileSet(tilesetTexture, 3, 1, 32, 32);

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
                for (int i = 0; i < 60; i++)
                {
                    int x = random.Next(0, 40);
                    int y = random.Next(0, 40);
                    int index = random.Next(1, 3);
                    Tile tile = new Tile(index, 0);
                    splatter.SetTile(x, y, tile);
                }
                _map.AddLayer(splatter);
            }
        }


        //possibly remove player sprite from here
        public override void Update(GameTime gameTime)
        {
            _playerCamera.Update(gameTime);
            _sprite.Update(gameTime, _spriteManager, _playerCamera.Camera);
            EnemyFactory.Update(gameTime, CurrentDifficulty, _spriteManager);
            PowerUpFactory.Update(gameTime, _spriteManager);
            _spriteManager.Update(gameTime, _sprite);
            AnimateSprite();
            ControlManager.Update(gameTime);
            _healthBar.UpdatePlayerHealth(_sprite.Health);
            _powersBar.IncreasePowerUpBar(_sprite.ItemCount);

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
            
           
            

            CheckGameOver();
           

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

            _map.Draw(GameRef.SpriteBatch, _playerCamera.Camera);
            _sprite.Draw(gameTime, GameRef.SpriteBatch, _playerCamera.Camera);
            _spriteManager.Draw(gameTime, GameRef.SpriteBatch, _playerCamera.Camera);
            

            base.Draw(gameTime);
            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        /// <summary>
        /// Animates the main character sprite
        /// </summary>
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
                _playerCamera.Camera.LockToSprite(_sprite);
            }
            else
            {
                _sprite.IsAnimating = false;
            }

        }

        /// <summary>
        /// Logic to check if a game over has occurred
        /// </summary>
        private void CheckGameOver()
        {
            if (_healthBar.IsEmpty) 
            {

                switch (CurrentDifficulty)
                {
                    case Difficulty.Easy:
                        if (_scoreLabel.Score > GameRef.SaveData.EasyScore)
                            GameRef.SaveData.EasyScore = _scoreLabel.Score;
                        break;
                    case Difficulty.Normal:
                        if (_scoreLabel.Score > GameRef.SaveData.NormalScore)
                            GameRef.SaveData.NormalScore = _scoreLabel.Score;
                        break;
                    case Difficulty.Hard:
                        if (_scoreLabel.Score > GameRef.SaveData.HardScore)
                            GameRef.SaveData.HardScore = _scoreLabel.Score;
                        break;
                }
                MediaPlayer.Stop();
                GameRef._gameOverScreen.Score = _scoreLabel.Score;
                GameRef._stateHandler.PushState(GameRef._gameOverScreen);
            }
        }

        /// <summary>
        /// Creates and displays a health bar
        /// </summary>
        /// <param name="gd"></param>
        /// <returns></returns>
        public HealthBar CreateHealthBar(GraphicsDevice gd)
        {
            int width = 210;
            int height = 42;
            int innerWidth = 200;
            int innerHeight = 30;

            HealthBar healthBar = new HealthBar(_sprite.Health, 0, 0);

            //create the textures
            Texture2D healthTexture = new Texture2D(gd, innerWidth, innerHeight, false, SurfaceFormat.Color);
            Texture2D borderTexture = new Texture2D(gd, width, height, false, SurfaceFormat.Color);

            //create the outer portion of the textbar texture
            Color[] backColor = new Color[width * height];
            for (int i = 0; i < backColor.Length; i++)
            {
                backColor[i] = new Color(0, 0, 0);
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

        /// <summary>
        /// Creates and displays a power up bar
        /// </summary>
        /// <param name="gd"></param>
        /// <returns></returns>
        private PowerUpBar CreatePowerUpBar(GraphicsDevice gd)
        {
            int width = 210;
            int height = 42;
            int innerWidth = 200;
            int innerHeight = 30;

            PowerUpBar powerUpBar = new PowerUpBar(240, 0);

            //create the textures
            Texture2D powerUpTexture = new Texture2D(gd, innerWidth, innerHeight, false, SurfaceFormat.Color);
            Texture2D borderTexture = new Texture2D(gd, width, height, false, SurfaceFormat.Color);

            //create the outer portion of the textbar texture
            Color[] backColor = new Color[width * height];
            for (int i = 0; i < backColor.Length; i++)
            {
                backColor[i] = new Color(0, 0, 0);
            }
            borderTexture.SetData(backColor);

            //create the inner portion of the textbar texture
            Color[] textureColor = new Color[innerWidth * innerHeight];
            for (int i = 0; i < textureColor.Length; i++)
            {
                textureColor[i] = new Color(0, 150, 255);
            }
            powerUpTexture.SetData(textureColor);

            //set the textures in the health bar
            powerUpBar.BorderTexture = borderTexture;
            powerUpBar.PowerUpTexture = powerUpTexture;
            //return newly created health bar
            return powerUpBar;
        }
    }
}
