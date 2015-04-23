using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;using GameEngineLibrary;
using GameEngineLibrary.Controls;

namespace Constantine.Screens
{
    public class MenuScreen : GameStateBase
    {
        LinkLabel _startLabel;
        bool isMusicPlaying;

        public MenuScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
            isMusicPlaying = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
           
            MediaPlayer.IsRepeating = true;

            base.LoadContent();

            _startLabel = new LinkLabel();
            _startLabel.Position = new Vector2(350, 600);
            _startLabel.Text = "Press ENTER to begin";
            _startLabel.Color = Color.White;
            _startLabel.TabStop = true;
            _startLabel.HasFocus = true;
            _startLabel.Selected += new EventHandler(startLabel_Selected);
            ControlManager.Add(_startLabel);
        }
        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime);

            base.Update(gameTime);
            if (!isMusicPlaying)
            {
                isMusicPlaying = true;
                MediaPlayer.Play(Audio.MenuTrack);
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            GameRef.SpriteBatch.Draw(Assets.Menu, GameRef.ScreenBounds, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        private void startLabel_Selected(object sender, EventArgs e)
        {
            _stateHandler.PushState(GameRef._difficultyScreen);
        }
    }
}
