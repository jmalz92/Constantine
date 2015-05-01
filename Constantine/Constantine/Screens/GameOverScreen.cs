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
        
        LinkLabel _playLabel;
        LinkLabel _mainMenuLabel;
        LinkLabel _exitLabel;

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


            _playLabel = new LinkLabel();
            _playLabel.Position = new Vector2(425, 400);
            _playLabel.Text = "Play Again";
            _playLabel.Color = Color.White;
            _playLabel.TabStop = true;
            _playLabel.HasFocus = true;
            _playLabel.Selected += new EventHandler(gameOverItem_Selected);

            _mainMenuLabel = new LinkLabel();
            _mainMenuLabel.Position = new Vector2(425, 500);
            _mainMenuLabel.Text = "Main Menu";
            _mainMenuLabel.Color = Color.White;
            _mainMenuLabel.TabStop = true;
            _mainMenuLabel.HasFocus = false;
            _mainMenuLabel.Selected += new EventHandler(gameOverItem_Selected);

            _exitLabel = new LinkLabel();
            _exitLabel.Position = new Vector2(425, 600);
            _exitLabel.Text = "Exit Game";
            _exitLabel.Color = Color.White;
            _exitLabel.TabStop = true;
            _exitLabel.HasFocus = false;
            _exitLabel.Selected += new EventHandler(gameOverItem_Selected);

            ControlManager.Add(_playLabel);
            ControlManager.Add(_mainMenuLabel);
            ControlManager.Add(_exitLabel);
        }


        
        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(Assets.GameOver, GameRef.ScreenBounds, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        private void gameOverItem_Selected(object sender, EventArgs e)
        {
            //Todo: clean up this logic
            string gameOverLabelText = ((LinkLabel)sender).Text; 
            if(gameOverLabelText.Contains("Play"))
            {
                Difficulty difficulty = GameRef._gameScreen.CurrentDifficulty;
                GameRef._gameScreen = new GameScreen(GameRef, _stateHandler);
                GameRef._gameScreen.CurrentDifficulty = difficulty;

                _stateHandler.PopState();
                _stateHandler.PushState(GameRef._gameScreen);
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
