﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;



namespace Constantine
{
    static class Audio
    {

        public static SoundEffect Bullet { get; private set; }

        public static void Load(ContentManager content)
        {
            Bullet = content.Load<SoundEffect>("Sounds/bullet");

        }
    }
}
