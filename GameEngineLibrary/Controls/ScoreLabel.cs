using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngineLibrary.Controls
{
    public class ScoreLabel : Label
    {
        private int score;

        #region Constructors
        public ScoreLabel()
        {
            score = 0;
            this.Text = "Score: 0";
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
