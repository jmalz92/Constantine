using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameEngineLibrary
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameState : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Fields/Properties
        protected GameStateHandler _stateHandler;
        private List<GameComponent> _childComponents;
        private GameState _tag;

        public List<GameComponent> Components
        {
            get { return _childComponents; }
        }

        public GameState Tag
        {
            get { return _tag; }
        }
        #endregion

        #region Constructors
        public GameState(Game game, GameStateHandler stateHandler)
            : base(game)
        {
            _stateHandler = stateHandler;
            _childComponents = new List<GameComponent>();
            _tag = this;
        }
        #endregion

        #region XNA Methods
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in _childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            DrawableGameComponent drawComponent;
            foreach (GameComponent component in _childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }
        #endregion

        internal protected virtual void StateChange(object sender, EventArgs e)
        {
            if (_stateHandler.CurrentState == Tag)
                Show();
            else
                Hide();
        }

        protected virtual void Show()
        {
            Visible = true;
            Enabled = true;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        protected virtual void Hide()
        {
            Visible = false;
            Enabled = false;

            foreach (GameComponent component in _childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

    }
}
