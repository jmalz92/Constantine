﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Constantine
{
    /// <summary>
    /// Loads all images and artwork for retrieval
    /// </summary>
    static class Assets
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D Skeleton { get; private set; }
        public static Texture2D Zombie { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D Pause { get; private set; }
        public static Texture2D Menu { get; private set; }
        public static Texture2D Splash { get; private set; }
        public static Texture2D GameOver { get; private set; }
        public static Texture2D Tablet { get; private set; }
        public static Texture2D Bolt { get; private set; }


        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Images/player_placeholder");
            Skeleton = content.Load<Texture2D>("Images/skeleton");
            Zombie = content.Load<Texture2D>("Images/zombie");
            Bullet = content.Load<Texture2D>("Images/bullet");
            Pause = content.Load<Texture2D>("Images/Pause");
            Menu = content.Load<Texture2D>("Images/menu");
            Splash = content.Load<Texture2D>("Images/splash");
            GameOver = content.Load<Texture2D>("Images/GameOver");
            Tablet = content.Load<Texture2D>("Images/tablet");
            Bolt = content.Load<Texture2D>("Images/bolt");
        }
    }
}