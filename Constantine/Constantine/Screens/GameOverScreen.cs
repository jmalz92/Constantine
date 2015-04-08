using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameEngineLibrary;
using GameEngineLibrary.Controls;

namespace Constantine.Screens
{
    public class GameOverScreen : GameStateBase
    {
        Texture2D _backgroundImage;
        LinkLabel _playLabel;
        LinkLabel _mainMenuLabel;
        LinkLabel _exitLabel;
        Label _gameOverLabel; //remove this for a game over background image

        public GameOverScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
           
            base.LoadContent();

            _gameOverLabel = new Label();
            _gameOverLabel.Position = new Vector2(100, 200);
            _gameOverLabel.Text = "GAME OVER";
            _gameOverLabel.Color = Color.White;

            _playLabel = new LinkLabel();
            _playLabel.Position = new Vector2(400, 200);
            _playLabel.Text = "Play Again";
            _playLabel.Color = Color.White;
            _playLabel.TabStop = true;
            _playLabel.HasFocus = true;
            _playLabel.Selected += new EventHandler(gameOverItem_Selected);

            _mainMenuLabel = new LinkLabel();
            _mainMenuLabel.Position = new Vector2(400, 300);
            _mainMenuLabel.Text = "Main Menu";
            _mainMenuLabel.Color = Color.White;
            _mainMenuLabel.TabStop = true;
            _mainMenuLabel.HasFocus = true;
            _mainMenuLabel.Selected += new EventHandler(gameOverItem_Selected);

            _exitLabel = new LinkLabel();
            _exitLabel.Position = new Vector2(400, 400);
            _exitLabel.Text = "Exit Game";
            _exitLabel.Color = Color.White;
            _exitLabel.TabStop = true;
            _exitLabel.HasFocus = true;
            _exitLabel.Selected += new EventHandler(gameOverItem_Selected);

            ControlManager.Add(_gameOverLabel);
            ControlManager.Add(_playLabel);
            ControlManager.Add(_mainMenuLabel);
            ControlManager.Add(_exitLabel);
        }


        
        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        private void gameOverItem_Selected(object sender, EventArgs e)
        {
            //Todo: clean up this logic
            string gameOverLabelText = ((LinkLabel)sender).Text; 
            if(gameOverLabelText.Contains("Play"))
            {
                int difficulty = GameRef._gameScreen.Difficulty;
                GameRef._gameScreen = new GameScreen(GameRef, _stateHandler);
                GameRef._gameScreen.Difficulty = difficulty;

                _stateHandler.PopState();
            }
            if(gameOverLabelText.Contains("Menu")){
                 GameRef._gameScreen = new GameScreen(GameRef, _stateHandler);
                _stateHandler.PopState();
                _stateHandler.PopState();
                _stateHandler.PopState();
            }
            if(gameOverLabelText.Contains("Exit")){
                Game.Exit();
            }
        }

    }
}
