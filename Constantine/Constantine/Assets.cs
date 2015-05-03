using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace Constantine
{
    /// <summary>
    /// Loads all images and artwork for retrieval
    /// </summary>
    static class Assets
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D Ultimate { get; private set; }
        public static Texture2D Skeleton { get; private set; }
        public static Texture2D Zombie { get; private set; }
        public static Texture2D Rabbit { get; private set; }
        public static Texture2D TreeMutant { get; private set; }
        public static Texture2D Devil { get; private set; }
        public static Texture2D LavaMonster { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D Pause { get; private set; }
        public static Texture2D Menu { get; private set; }
        public static Texture2D GameOver { get; private set; }
        public static Texture2D Tablet { get; private set; }
        public static Texture2D Bolt { get; private set; }
        public static Texture2D Blood { get; private set; }
        public static Texture2D DemonBlood { get; private set; }
        public static Texture2D Bone { get; private set; }
        public static Texture2D Log { get; private set; }

        public static Video Splash { get; private set; }
        public static Video Intro { get; private set; }

        public static void Load(ContentManager content)
        {
            Player = content.Load<Texture2D>("Images/player_placeholder");
            Ultimate = content.Load<Texture2D>("Images/angel_placeholder");
            Skeleton = content.Load<Texture2D>("Images/skeleton");
            Zombie = content.Load<Texture2D>("Images/zombie");
            Rabbit = content.Load<Texture2D>("Images/rabbit");
            TreeMutant = content.Load<Texture2D>("Images/treemutant");
            Devil = content.Load<Texture2D>("Images/devil");
            LavaMonster = content.Load<Texture2D>("Images/lavamonster");
            Bullet = content.Load<Texture2D>("Images/bullet");
            Pause = content.Load<Texture2D>("Images/Pause");
            Menu = content.Load<Texture2D>("Images/menu");
            GameOver = content.Load<Texture2D>("Images/GameOver");
            Tablet = content.Load<Texture2D>("Images/tablet");
            Bolt = content.Load<Texture2D>("Images/bolt");
            Blood = content.Load<Texture2D>("Images/blood");
            DemonBlood = content.Load<Texture2D>("IMages/demonblood");
            Bone = content.Load<Texture2D>("Images/bone");
            Log = content.Load<Texture2D>("Images/log");


            Splash = content.Load<Video>("Video/splash");
            Intro = content.Load<Video>("Video/intro");
        }
    }
}
