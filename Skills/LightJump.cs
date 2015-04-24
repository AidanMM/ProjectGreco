using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;
using ProjectGreco.Skills;
using ProjectGreco.GameObjects;

namespace ProjectGreco.Skills
{
    class LightJump
    {
        public static OneWayBlock oneWayBlock;

        public LightJump(Player myPlayer)
        {
            oneWayBlock = new OneWayBlock(new Vector2(myPlayer.Position.X, myPlayer.Position.Y + myPlayer.Height + 5), 100);        
        }
    }
}
