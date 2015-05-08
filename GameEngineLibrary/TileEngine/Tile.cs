using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngineLibrary.TileEngine
{
    /// <summary>
    /// Defines a single tile on the tile map
    /// Tile logic found at: http://xnagpa.net/xna4rpg.php
    /// Original Author: Jamie McMahon
    /// </summary>
    public class Tile
    {
        #region Field Region

        int tileIndex;
        int tileset;

        #endregion

        #region Property Region

        public int TileIndex
        {
            get { return tileIndex; }
            private set { tileIndex = value; }
        }

        public int Tileset
        {
            get { return tileset; }
            private set { tileset = value; }
        }

        #endregion

        #region Constructor Region

        public Tile(int tileIndex, int tileset)
        {
            TileIndex = tileIndex;
            Tileset = tileset;
        }

        #endregion
    }
}
