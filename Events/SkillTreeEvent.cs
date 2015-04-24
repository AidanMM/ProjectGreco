using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;
using ProjectGreco.Levels;

namespace ProjectGreco.Events
{
    class SkillTreeEvent : Event
    {
        public SkillTreeEvent(int index)
            : base(index)
        {

        }

        public override bool Update()
        {
            if (Game1.OBJECT_HANDLER.currentState.LevelType == LevelName.Home && Game1.OBJECT_HANDLER.objectDictionary["Player"].Position.X > 600)
            {
                return true;
            }
            return false;
        }
    }
}
