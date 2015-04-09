using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngineLibrary.Sprites
{
    public enum EnemyType
    {
        Easy,
        Normal,
        Hard
    }

    public class EnemySpriteFactory
    {


        public EnemySpriteFactory()
        {

        }

        //TODO: figure out type conflicts
        //public EnemySprite GetEnemySprite(EnemyType type)
        //{

        //    EnemySprite enemy = null;

        //    switch (type)
        //    {
        //        case EnemyType.Easy:
        //            break;
        //        case EnemyType.Normal:
        //            break;
        //        case EnemyType.Hard:
        //            break;
        //    }

        //    return enemy;

        //}
    }
}
