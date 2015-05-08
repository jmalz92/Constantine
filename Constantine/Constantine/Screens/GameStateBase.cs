using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using GameEngineLibrary;
using GameEngineLibrary.Controls;


namespace Constantine.Screens
{
    public abstract partial class GameStateBase : GameState
    {
        protected Game1 GameRef;
        protected ControlManager ControlManager;

        public GameStateBase(Game game, GameStateHandler handler)
            :base(game, handler)
        {
            GameRef = (Game1)game;
        }

        protected virtual void OnStateDisplayed(EventArgs e)
        {
            EventHandler handler = StateDisplayed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler StateDisplayed;

        protected override void LoadContent()
        {
            ContentManager Content = Game.Content;

            SpriteFont menuFont = Content.Load<SpriteFont>(@"Fonts\ControlFont");
            ControlManager = new ControlManager(menuFont);

            base.LoadContent();
        }
    }
}
