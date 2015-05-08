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
    /// Input Handler logic found at: http://xnagpa.net/xna4rpg.php
    /// Original Author: Jamie McMahon
    /// Edited to included computer mouse states and game controller aim directions
    /// </summary>
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        #region Fields/Properties
        private static bool isAimingWithMouse = false;
        private static MouseState _mouseState, _lastMouseState;


        static KeyboardState _keyboardState;
        static KeyboardState _lastKeyboardState;

        static GamePadState _gamePadState;
        static GamePadState _lastGamePadState;

        public static Vector2 MousePosition { get { return new Vector2(_mouseState.X, _mouseState.Y); } }

        public static MouseState MouseState
        {
            get { return _mouseState; }
        }

        public static MouseState LastMouseState
        {
            get { return _lastMouseState; }
        }

        public static KeyboardState KeyboardState
        {
            get { return _keyboardState; }
        }

        public static KeyboardState LastKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public static GamePadState GamePadState
        {
            get { return _gamePadState; }
        }

        public static GamePadState LastGamePadState
        {
            get { return _lastGamePadState; }
        }
        #endregion

        #region Constructor(s)
        public InputHandler(Game game)
            : base(game)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            _gamePadState = GamePad.GetState(PlayerIndex.One);

            
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
            _lastMouseState = _mouseState;
            _mouseState = Mouse.GetState();

            _lastKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();

            _lastGamePadState = _gamePadState;
            _gamePadState = GamePad.GetState(PlayerIndex.One);

            // If the player pressed one of the arrow keys or is using a gamepad to aim, we want to disable mouse aiming. Otherwise,
            // if the player moves the mouse, enable mouse aiming.
            if (_gamePadState.ThumbSticks.Right != Vector2.Zero)
                isAimingWithMouse = false;
            else if (MousePosition != new Vector2(_lastMouseState.X, _lastMouseState.Y))
                isAimingWithMouse = true;
            base.Update(gameTime);
        }
        #endregion

        #region Keyboard/Game Pad Methods

        public static void Flush()
        {
            _lastKeyboardState = _keyboardState;
        }

        public static Vector2 GetAimDirection(Vector2 playerPosition) //change this to hide info about player?
        {
            Vector2 direction = Vector2.Zero;
            if (isAimingWithMouse)
            {
                direction = MousePosition - playerPosition;
            }
            else
            {
                direction = _gamePadState.ThumbSticks.Right;
                direction.Y *= -1;
            }

            
            if (direction == Vector2.Zero)
                return Vector2.Zero;
            else
                return Vector2.Normalize(direction);
            
        }

        public static bool MouseDown(ButtonState state)
        {
            return state == ButtonState.Pressed;
        }

        public static bool KeyReleased(Keys key)
        {
            return _keyboardState.IsKeyUp(key) &&
                _lastKeyboardState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key) &&
                _lastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }


        public static bool ButtonReleased(Buttons button)
        {
            return _gamePadState.IsButtonUp(button) &&
                _lastGamePadState.IsButtonDown(button);
        }

        public static bool ButtonPressed(Buttons button)
        {
            return _gamePadState.IsButtonDown(button) &&
                _lastGamePadState.IsButtonUp(button);
        }

        public static bool ButtonDown(Buttons button)
        {
            return _gamePadState.IsButtonDown(button);
        }

        #endregion

    }
}
