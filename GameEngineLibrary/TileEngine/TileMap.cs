using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngineLibrary.TileEngine
{
    /// <summary>
    /// Defines the entire tilemap to be drawn
    /// Map logic found at: http://xnagpa.net/xna4rpg.php
    /// Original Author: Jamie McMahon
    /// </summary>
    public class TileMap
    {
        #region Field/Property Region
        List<TileSet> _tileSets;
        List<MapLayer> _mapLayers;

        private static int mapWidth;
        private static int mapHeight;


        public static int WidthInPixels
        {
            get { return mapWidth * TileEngine.TileWidth; }
        }

        public static int HeightInPixels
        {
            get { return mapHeight * TileEngine.TileHeight; }
        }

        #endregion

        #region Constructor Region
        public TileMap(List<TileSet> tilesets, List<MapLayer> layers)
        {
            _tileSets = tilesets;
            _mapLayers = layers;

            mapWidth = _mapLayers[0].Width;
            mapHeight = _mapLayers[0].Height;
            for (int i = 1; i < layers.Count; i++)
            {
                if (mapWidth != _mapLayers[i].Width || mapHeight != _mapLayers[i].Height)
                    throw new Exception("Map layer size exception");
            }
        }

        public TileMap(TileSet tileset, MapLayer layer)
        {
            _tileSets = new List<TileSet>();
            _tileSets.Add(tileset);
            _mapLayers = new List<MapLayer>();
            _mapLayers.Add(layer);

            mapWidth = _mapLayers[0].Width;
            mapHeight = _mapLayers[0].Height;
        }
        #endregion

        #region Method Region
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Rectangle destination = new Rectangle(0, 0, TileEngine.TileWidth, TileEngine.TileHeight);
            Tile tile;
            foreach (MapLayer layer in _mapLayers)
            {
                for (int y = 0; y < layer.Height; y++)
                {
                    destination.Y = y * TileEngine.TileHeight - (int)camera.Position.Y;
                    for (int x = 0; x < layer.Width; x++)
                    {
                        tile = layer.GetTile(x, y);
                        if (tile.TileIndex == -1 || tile.Tileset == -1)
                            continue;
                        destination.X = x * TileEngine.TileWidth - (int)camera.Position.X;
                        spriteBatch.Draw(
                        _tileSets[tile.Tileset].Texture,
                        destination,
                        _tileSets[tile.Tileset].SourceRectangles[tile.TileIndex],
                        Color.White);
                    }
                }
            }
        }

        public void AddLayer(MapLayer layer)
        {
            if (layer.Width != mapWidth && layer.Height != mapHeight)
                throw new Exception("Map layer size exception");
            _mapLayers.Add(layer);
        }
        #endregion
    }
}
