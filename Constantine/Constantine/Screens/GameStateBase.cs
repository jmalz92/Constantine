using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameEngineLibrary;

namespace Constantine.Screens
{
    public abstract partial class GameStateBase : GameState
    {
        protected Game1 GameRef;

        public GameStateBase(Game game, GameStateHandler handler)
            :base(game, handler)
        {
            GameRef = (Game1)game;
        }
    }
}
