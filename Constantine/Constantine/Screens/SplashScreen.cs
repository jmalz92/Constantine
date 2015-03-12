using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameEngineLibrary;

namespace Constantine.Screens
{
    public class SplashScreen : GameStateBase
    {
        Texture2D _backgroundImage;

        public SplashScreen(Game game, GameStateHandler handler)
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
            _backgroundImage = Content.Load<Texture2D>(@"Images/splash");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (gameTime.TotalGameTime.TotalSeconds > 2)
            {
                _stateHandler.PushState(GameRef._menuScreen);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            base.Draw(gameTime);
            GameRef.SpriteBatch.Draw(_backgroundImage, GameRef.ScreenBounds, Color.White);
            GameRef.SpriteBatch.End();
        }
    }
}
