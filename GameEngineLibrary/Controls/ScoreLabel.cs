using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Controls
{
    /// <summary>
    /// Score label control
    /// </summary>
    public class ScoreLabel : Label
    {
        private int _score;
        public int Score
        {
            get { return _score; }
        }

        #region Constructors
        public ScoreLabel()
        {
            _score = 0;
            this.Text = "Score: 0";
        }

        public ScoreLabel(Vector2 position)
        {
            this.Position = position;
            this.Text = "Score: 0";
        }

        public ScoreLabel(Vector2 position, string text)
        {
            this.Position = position;
            this.Text = text;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add points to the score label
        /// </summary>
        /// <param name="points">the points to add</param>
        public void UpdateScore(int points)
        {
            this._score += points;
            this.Text = "Score: " + _score;
        }
        #endregion

    }
}
