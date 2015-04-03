using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using GameEngineLibrary;

namespace Constantine.Screens
{
    public class CutScreen : GameStateBase
    {
        SpriteFont spriteFont;
        public CutScreen(Game game, GameStateHandler handler)
            : base(game, handler)
        {

        }

        public override void Initialize()
        {
            MediaPlayer.Stop();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteFont = Game.Content.Load<SpriteFont>("MySpriteFont");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (InputHandler.KeyDown(Keys.G) && InputHandler.KeyDown(Keys.S))
                GameRef._stateHandler.PopState();

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            GraphicsDevice.Clear(Color.White);
            GameRef.SpriteBatch.DrawString(spriteFont, "This is a cut screen", new Vector2(100f, 100f), Color.Red);
            base.Draw(gameTime);
            GameRef.SpriteBatch.End();
        }
    }
}
