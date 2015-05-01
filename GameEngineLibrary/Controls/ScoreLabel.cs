using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameEngineLibrary.Controls
{
    public class ScoreLabel : Label
    {
        private int score;
        public int Score
        {
            get { return score; }
        }

        #region Constructors
        public ScoreLabel()
        {
            score = 0;
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
        public void UpdateScore(int points)
        {
            this.score += points;
            this.Text = "Score: " + score;
        }
        #endregion

    }
}
