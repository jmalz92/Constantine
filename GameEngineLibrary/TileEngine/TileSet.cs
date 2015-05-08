﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineLibrary.TileEngine
{
    /// <summary>
    /// Defines an entire tileset to be used to draw the tile map
    /// Tileset logic found at: http://xnagpa.net/xna4rpg.php
    /// Original Author: Jamie McMahon
    /// </summary>
    public class TileSet
    {
        #region Fields and Properties

        Texture2D image;
        int tileWidthInPixels;
        int tileHeightInPixels;
        int tilesWide;
        int tilesHigh;
        Rectangle[] sourceRectangles;

        #endregion

        #region Property Region

        public Texture2D Texture
        {
            get { return image; }
            private set { image = value; }
        }

        public int TileWidth
        {
            get { return tileWidthInPixels; }
            private set { tileWidthInPixels = value; }
        }

        public int TileHeight
        {
            get { return tileHeightInPixels; }
            private set { tileHeightInPixels = value; }
        }

        public int TilesWide
        {
            get { return tilesWide; }
            private set { tilesWide = value; }
        }

        public int TilesHigh
        {
            get { return tilesHigh; }
            private set { tilesHigh = value; }
        }

        public Rectangle[] SourceRectangles
        {
            get { return (Rectangle[])sourceRectangles.Clone(); }
        }

        #endregion

        #region Constructor Region

        public TileSet(Texture2D image, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            Texture = image;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;

            int tiles = tilesWide * tilesHigh;

            sourceRectangles = new Rectangle[tiles];

            int tile = 0;

            for (int y = 0; y < tilesHigh; y++)
                for (int x = 0; x < tilesWide; x++)
                {
                    sourceRectangles[tile] = new Rectangle(
                        x * tileWidth,
                        y * tileHeight,
                        tileWidth,
                        tileHeight);
                    tile++;
                }
        }

        #endregion
    }
}
