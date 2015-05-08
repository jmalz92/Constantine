using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameEngineLibrary.TileEngine
{
    /// <summary>
    /// Tile Engine
    /// Logic found at: http://xnagpa.net/xna4rpg.php
    /// Original Author: Jamie McMahon
    /// </summary>
    public class TileEngine
    {
        private static int  _tileWidth;
        private static int _tileHeight;

        public static int TileWidth { get { return _tileWidth; } }
        public static int TileHeight { get { return _tileHeight; } }

        public TileEngine(int tileWidth, int tileHeight)
        {
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public static Point GetPositionFromVector(Vector2 position)
        {
            return new Point((int)position.X / _tileWidth, (int)position.Y / _tileHeight);
        }
    }
}
