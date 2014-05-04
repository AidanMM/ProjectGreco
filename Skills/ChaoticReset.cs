using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.Levels;
using ProjectGreco.Skills;
using ProjectGreco.GameObjects;

namespace ProjectGreco.Skills
{
    class ChaoticReset
    {
        public ChaoticReset(Player myPlayer)
        {
            if (BaseState.currLevel != LevelName.Home)
            {
                Level levelToSet = new Level(BaseState.currLevel, myPlayer, true);
                (levelToSet as Level).PositionPlayer();
                Game1.OBJECT_HANDLER.ChangeState(levelToSet);
            }
        }
    }
}
