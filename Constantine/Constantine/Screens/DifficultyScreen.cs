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
        Texture2D _backgroundImage;
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
            ContentManager Content = GameRef.Content;
            _backgroundImage = Content.Load<Texture2D>(@"Images/menu");
            base.LoadContent();

            _easyLabel = new LinkLabel();
            _easyLabel.Position = new Vector2(500, 400);
            _easyLabel.Text = "Easy";
            _easyLabel.Color = Color.White;
            _easyLabel.TabStop = true;
            _easyLabel.HasFocus = false;
            _easyLabel.Selected += new EventHandler(difficulty_Selected);

            _normalLabel = new LinkLabel();
            _normalLabel.Position = new Vector2(500, 500);
            _normalLabel.Text = "Normal";
            _normalLabel.Color = Color.White;
            _normalLabel.TabStop = true;
            _normalLabel.HasFocus = true;
            _normalLabel.Selected += new EventHandler(difficulty_Selected);

            _hardLabel = new LinkLabel();
            _hardLabel.Position = new Vector2(500, 600);
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
            ControlManager.Update(gameTime, PlayerIndex.One);

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

        private void difficulty_Selected(object sender, EventArgs e)
        {
            _stateHandler.PushState(GameRef._gameScreen);
        }
    }
}
