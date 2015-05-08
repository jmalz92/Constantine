using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using GameEngineLibrary;

namespace Constantine.Screens
{

    /// <summary>
    /// Game state to display a splash screen video before the game begins
    /// </summary>
    public class SplashScreen : GameStateBase
    {

        VideoPlayer _player;
        Texture2D _videoTexture;
        bool _videoLoaded = false;

        public SplashScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
        }

        public override void Initialize()
        {
            _player = new VideoPlayer();
            _player.IsLooped = false;
            _player.Volume = .5f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Assets.Splash != null && !_videoLoaded)
            {
                _videoLoaded = true;
                _player.Play(Assets.Splash);
            }

            if (_videoLoaded)
            {
                if (_player.State == MediaState.Stopped)
                {
                    _player.Dispose();
                    _stateHandler.PushState(GameRef._menuScreen);

                }
            }

            
                
            
        }
        public override void Draw(GameTime gameTime)
        {
            // Only call GetTexture if a video is playing or paused
            if (_player.State != MediaState.Stopped)
                _videoTexture = _player.GetTexture();

            // Drawing to the rectangle will stretch the 
            // video to fill the screen
            Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
                GraphicsDevice.Viewport.Y,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);

            // Draw the video, if we have a texture to draw.
            if (_videoTexture != null)
            {
                GameRef.SpriteBatch.Begin();
                GameRef.SpriteBatch.Draw(_videoTexture, screen, Color.White);
                GameRef.SpriteBatch.End();
            }
            base.Draw(gameTime);
            
        }
    }
}
