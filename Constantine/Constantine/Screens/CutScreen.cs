using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;

using GameEngineLibrary;
using GameEngineLibrary.Controls;
namespace Constantine.Screens
{
    public class CutScreen : GameStateBase
    {
        Texture2D _backgroundImage;
        LinkLabel _startLabel;
        LinkLabel _constLabel;
        //Song menuMusic;
        //bool isMusicPlaying;

        public CutScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
            //isMusicPlaying = false;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;
            _backgroundImage = Content.Load<Texture2D>(@"Images/constantinexi");

            MediaPlayer.IsRepeating = true;

            base.LoadContent();

            _startLabel = new LinkLabel();
            _startLabel.Position = new Vector2(0, 0);
            _startLabel.Text = "God has sent you to reclaim your kingdom!";
            _startLabel.Color = Color.Cornsilk;
            _startLabel.SelectedColor = Color.Cornsilk;
            _startLabel.TabStop = false;
            _startLabel.HasFocus = true;
            _startLabel.SpriteFont = Content.Load<SpriteFont>(@"Fonts\CutSceneFont");
            _startLabel.Selected += new EventHandler(startLabel_Selected);
            ControlManager.Add(_startLabel);

            _constLabel = new LinkLabel();
            _constLabel.Position = new Vector2(0, 100);
            _constLabel.Text = "Your name is Constantine XI. Good luck. :)";
            _constLabel.Color = Color.Cornsilk;
            _constLabel.SelectedColor = Color.Cornsilk;
            _constLabel.TabStop = false;
            _constLabel.HasFocus = true;
            _constLabel.SpriteFont = Content.Load<SpriteFont>(@"Fonts\CutSceneFont");
            _constLabel.Selected += new EventHandler(constLabel_Selected);
            ControlManager.Add(_constLabel);

        }

        private void constLabel_Selected(object sender, EventArgs e)
        {
            
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
            GameRef.SpriteBatch.Draw(_backgroundImage, GameRef.ScreenBounds, Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        private void startLabel_Selected(object sender, EventArgs e)
        {
            _stateHandler.PushState(GameRef._gameScreen);
        }

    }
}
