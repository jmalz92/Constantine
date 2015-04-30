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
    public class CutScreen : GameStateBase
    {
       
        VideoPlayer player;
        Texture2D videoTexture;
        bool videoLoaded = false;
        bool previouslyViewed = false; //do something else with this once serializer is plugged in

        public CutScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {
        }

        public override void Initialize()
        {
            player = new VideoPlayer();
            player.IsLooped = false;
            player.Volume = .5f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(previouslyViewed)
                _stateHandler.PushState(GameRef._gameScreen);

            if (Assets.Intro != null && !videoLoaded)
            {
                videoLoaded = true;
                player.Play(Assets.Intro);
            }

            if (videoLoaded)
            {
                if (player.State == MediaState.Stopped)
                {
                    player.Dispose();
                    _stateHandler.PushState(GameRef._gameScreen);

                }
            }

        }
        public override void Draw(GameTime gameTime)
        {
            // Only call GetTexture if a video is playing or paused
            if (player.State != MediaState.Stopped)
                videoTexture = player.GetTexture();

            // Drawing to the rectangle will stretch the 
            // video to fill the screen
            Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
                GraphicsDevice.Viewport.Y,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);

            // Draw the video, if we have a texture to draw.
            if (videoTexture != null)
            {
                GameRef.SpriteBatch.Begin();
                GameRef.SpriteBatch.Draw(videoTexture, screen, Color.White);
                GameRef.SpriteBatch.End();
            }
            base.Draw(gameTime);
            
        }

    }
}
