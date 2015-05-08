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

namespace Constantine.Screens
{
    //Game state to display a cut scene video
    public class CutScreen : GameStateBase
    {
        VideoPlayer _player;
        Texture2D _videoTexture;
        bool _videoLoaded = false;

        public CutScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
        }

        public override void Initialize()
        {

            if (GameRef.SaveData.CinematicViewed)
                _stateHandler.PushState(GameRef._gameScreen);

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

            if (Assets.Intro != null && !_videoLoaded)
            {
                _videoLoaded = true;
                _player.Play(Assets.Intro);
                GameRef.SaveData.CinematicViewed = true;
            }

            if (_videoLoaded)
            {
                if (_player.State == MediaState.Stopped)
                {
                    _player.Dispose();
                    _stateHandler.PushState(GameRef._gameScreen);
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
