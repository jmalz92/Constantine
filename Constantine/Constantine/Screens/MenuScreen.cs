using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;using GameEngineLibrary;

namespace Constantine.Screens
{
    public class MenuScreen : GameStateBase
    {
        Texture2D _backgroundImage;
        Song menuMusic;
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
            ContentManager Content = GameRef.Content;
            _backgroundImage = Content.Load<Texture2D>(@"Images/menu");

            menuMusic = Content.Load<Song>(@"Sounds/Dystopia");
            MediaPlayer.IsRepeating = true;

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!isMusicPlaying)
            {
                isMusicPlaying = true;
                MediaPlayer.Play(menuMusic);
            }
            if (InputHandler.KeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                GameRef._stateHandler.PushState(GameRef._gameScreen);
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
