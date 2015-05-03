using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameEngineLibrary;
using GameEngineLibrary.Controls;


namespace Constantine.Screens
{

    public enum Difficulty
    {
        Easy, 
        Normal, 
        Hard
    }

    public class DifficultyScreen : GameStateBase
    {
        LinkLabel _easyLabel;
        LinkLabel _normalLabel;
        LinkLabel _hardLabel;

        Label _easyScoreLabel;
        Label _normalScoreLabel;
        Label _hardScoreLabel;
        Label _highScoreLabel;


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

            //TODO: change LinkLabel / Label constructor to accept these value
            _easyLabel = new LinkLabel(); 
            _easyLabel.Position = new Vector2(350, 425);
            _easyLabel.Text = "Easy";
            _easyLabel.Color = Color.White;
            _easyLabel.TabStop = true;
            _easyLabel.HasFocus = true;
            _easyLabel.Selected += new EventHandler(difficulty_Selected);

            _normalLabel = new LinkLabel();
            _normalLabel.Position = new Vector2(350, 500);
            _normalLabel.Text = "Normal";
            _normalLabel.Color = Color.White;
            _normalLabel.TabStop = true;
            _normalLabel.HasFocus = false;
            _normalLabel.Selected += new EventHandler(difficulty_Selected);

            _hardLabel = new LinkLabel();
            _hardLabel.Position = new Vector2(350, 575);
            _hardLabel.Text = "Hard";
            _hardLabel.Color = Color.White;
            _hardLabel.TabStop = true;
            _hardLabel.HasFocus = false;
            _hardLabel.Selected += new EventHandler(difficulty_Selected);

            _highScoreLabel = new Label();
            _highScoreLabel.Position = new Vector2(550, 350);
            _highScoreLabel.Text = "High Score";
            _highScoreLabel.Color = Color.White;

            _easyScoreLabel = new Label();
            _easyScoreLabel.Position = new Vector2(550, 425);
            _easyScoreLabel.Text = "0";
            _easyScoreLabel.Color = Color.White;

            _normalScoreLabel = new Label();
            _normalScoreLabel.Position = new Vector2(550, 500);
            _normalScoreLabel.Text = "0";
            _normalScoreLabel.Color = Color.White;

            _hardScoreLabel = new Label();
            _hardScoreLabel.Position = new Vector2(550, 575);
            _hardScoreLabel.Text = "0";
            _hardScoreLabel.Color = Color.White;


           

            ControlManager.Add(_easyLabel);
            ControlManager.Add(_normalLabel);
            ControlManager.Add(_hardLabel);

            ControlManager.Add(_highScoreLabel);

            ControlManager.Add(_easyScoreLabel);
            ControlManager.Add(_normalScoreLabel);
            ControlManager.Add(_hardScoreLabel);



        }
        public override void Update(GameTime gameTime)
        {
            UpdateHighScores();
            ControlManager.Update(gameTime);

            base.Update(gameTime);
        }

        //TODO: If time allows use databinding instead of this method
        public void UpdateHighScores()
        {
            this._easyScoreLabel.Text = GameRef.SaveData.EasyScore.ToString();
            this._normalScoreLabel.Text = GameRef.SaveData.NormalScore.ToString();
            this._hardScoreLabel.Text = GameRef.SaveData.HardScore.ToString();
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
            MediaPlayer.Stop();
            
            //Todo: clean up this logic
            string difficulty = ((LinkLabel)sender).Text; 
            if(difficulty.Contains("Easy")){
                GameRef._gameScreen.CurrentDifficulty = Difficulty.Easy;
            }
            if(difficulty.Contains("Normal")){
                GameRef._gameScreen.CurrentDifficulty = Difficulty.Normal;
            }
            if(difficulty.Contains("Hard")){
                GameRef._gameScreen.CurrentDifficulty = Difficulty.Hard;
            }
            _stateHandler.PushState(GameRef._cutScreen);
        }
    }
}
