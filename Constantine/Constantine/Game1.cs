using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Constantine.Screens;
using GameEngineLibrary;

namespace Constantine
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        const int SCREEN_WIDTH = 1024;
        const int SCREEN_HEIGHT = 768;

        GraphicsDeviceManager graphics;
        public SpriteBatch SpriteBatch;

        public SplashScreen _splashScreen;
        public MenuScreen _menuScreen;
        public GameScreen _gameScreen;
        public DifficultyScreen _difficultyScreen;
        public PauseScreen _pauseScreen;
        public GameOverScreen _gameOverScreen;
        public GameStateHandler _stateHandler;
        
        

        public readonly Rectangle ScreenBounds;

        public Game1()
        {
            //This is a jonathan comment
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            ScreenBounds = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
            Content.RootDirectory = "Content";

            Components.Add(new InputHandler(this));
            _stateHandler = new GameStateHandler(this);
            Components.Add(_stateHandler);

            _splashScreen = new SplashScreen(this, _stateHandler);
            _menuScreen = new MenuScreen(this, _stateHandler);
            _gameScreen = new GameScreen(this, _stateHandler);
            _difficultyScreen = new DifficultyScreen(this, _stateHandler);
            
            _pauseScreen = new PauseScreen(this, _stateHandler);
            _gameOverScreen = new GameOverScreen(this, _stateHandler);

            _stateHandler.ChangeState(_splashScreen);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "Constantine Alpha Build";
            this.IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Assets.Load(Content);
            Audio.Load(Content);
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
