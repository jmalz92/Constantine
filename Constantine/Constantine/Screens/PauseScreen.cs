using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using GameEngineLibrary;
using GameEngineLibrary.Controls;

namespace Constantine.Screens
{
    public class PauseScreen : GameStateBase
    {
        LinkLabel _returnLabel;
        LinkLabel _mainMenuLabel;
        LinkLabel _exitLabel;

        public PauseScreen(Game game, GameStateHandler handler) : base(game, handler) 
        {
        }

        public override void Initialize()
        { 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            _returnLabel = new LinkLabel();
            _returnLabel.Position = new Vector2(425, 400);
            _returnLabel.Text = "Return to Game";
            _returnLabel.Color = Color.White;
            _returnLabel.TabStop = true;
            _returnLabel.HasFocus = true;
            _returnLabel.Selected += new EventHandler(menuItem_Selected);

            _mainMenuLabel = new LinkLabel();
            _mainMenuLabel.Position = new Vector2(425, 500);
            _mainMenuLabel.Text = "Main Menu";
            _mainMenuLabel.Color = Color.White;
            _mainMenuLabel.TabStop = true;
            _mainMenuLabel.HasFocus = false;
            _mainMenuLabel.Selected += new EventHandler(menuItem_Selected);

            _exitLabel = new LinkLabel();
            _exitLabel.Position = new Vector2(425, 600);
            _exitLabel.Text = "Exit Game";
            _exitLabel.Color = Color.White;
            _exitLabel.TabStop = true;
            _exitLabel.HasFocus = false;
            _exitLabel.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(_returnLabel);
            ControlManager.Add(_mainMenuLabel);
            ControlManager.Add(_exitLabel);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime);

            if (InputHandler.KeyPressed(Keys.Escape) || InputHandler.ButtonPressed(Buttons.Start))
            {
                _stateHandler.PopState();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(Assets.Pause, GameRef.ScreenBounds, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        private void menuItem_Selected(object sender, EventArgs e)
        {
            LinkLabel selectedLabel = sender as LinkLabel;

            if (selectedLabel.Text.Contains("Return"))
            {
                _stateHandler.PopState();
            }
            else if (selectedLabel.Text.Contains("Menu"))
            {
                _stateHandler.PopState();
                _stateHandler.PopState();
                _stateHandler.PopState();
                GameRef._gameScreen = new GameScreen(GameRef, _stateHandler);

            }
            else if (selectedLabel.Text.Contains("Exit"))
            {
                Game.Exit();
            }
        }

    }
}
