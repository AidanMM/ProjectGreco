﻿using System;
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
    class Ghost
    {
        public Ghost(Player myPlayer)
        {
            myPlayer.Ghost(45);
        }
    }
}
