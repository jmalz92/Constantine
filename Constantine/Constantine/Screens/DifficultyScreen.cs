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
    public class DifficultyScreen : GameStateBase
    {
        LinkLabel _easyLabel;
        LinkLabel _normalLabel;
        LinkLabel _hardLabel;

        public DifficultyScreen(Game game, GameStateHandler handler)
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

            _easyLabel = new LinkLabel();
            _easyLabel.Position = new Vector2(450, 400);
            _easyLabel.Text = "Easy";
            _easyLabel.Color = Color.White;
            _easyLabel.TabStop = true;
            _easyLabel.HasFocus = true;
            _easyLabel.Selected += new EventHandler(difficulty_Selected);

            _normalLabel = new LinkLabel();
            _normalLabel.Position = new Vector2(450, 500);
            _normalLabel.Text = "Normal";
            _normalLabel.Color = Color.White;
            _normalLabel.TabStop = true;
            _normalLabel.HasFocus = false;
            _normalLabel.Selected += new EventHandler(difficulty_Selected);

            _hardLabel = new LinkLabel();
            _hardLabel.Position = new Vector2(450, 600);
            _hardLabel.Text = "Hard";
            _hardLabel.Color = Color.White;
            _hardLabel.TabStop = true;
            _hardLabel.HasFocus = false;
            _hardLabel.Selected += new EventHandler(difficulty_Selected);


            ControlManager.Add(_easyLabel);
            ControlManager.Add(_normalLabel);
            ControlManager.Add(_hardLabel);


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
            GameRef.SpriteBatch.Draw(Assets.Menu, GameRef.ScreenBounds, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }

        private void difficulty_Selected(object sender, EventArgs e)
        {
            //Todo: clean up this logic
            string difficulty = ((LinkLabel)sender).Text; 
            if(difficulty.Contains("Easy")){
                GameRef._gameScreen.Difficulty = 0;
            }
            if(difficulty.Contains("Normal")){
                GameRef._gameScreen.Difficulty = 1;
            }
            if(difficulty.Contains("Hard")){
                GameRef._gameScreen.Difficulty = 2;
            }
            _stateHandler.PushState(GameRef._gameScreen);
        }
    }
}
