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
                Level levelToSet;

                switch(BaseState.currLevel)
                {
                    case LevelName.Desert:
                        if (PlayerStats.desertChaotic)
                            break;
                        PlayerStats.desertChaotic = true;
                        levelToSet = new Level(BaseState.currLevel, myPlayer, true);
                        (levelToSet as Level).PositionPlayer();
                        Game1.OBJECT_HANDLER.ChangeState(levelToSet);
                        PlayerStats.timeInLevel = 0;
                        break;
                    case LevelName.Forest:
                        if (PlayerStats.forestChaotic)
                            break;
                        PlayerStats.forestChaotic = true;
                        levelToSet = new Level(BaseState.currLevel, myPlayer, true);
                        (levelToSet as Level).PositionPlayer();
                        Game1.OBJECT_HANDLER.ChangeState(levelToSet);
                        PlayerStats.timeInLevel = 0;
                        break;
                    case LevelName.Hills:
                        if (PlayerStats.hillChaotic)
                            break;
                        PlayerStats.hillChaotic = true;
                        levelToSet = new Level(BaseState.currLevel, myPlayer, true);
                        (levelToSet as Level).PositionPlayer();
                        Game1.OBJECT_HANDLER.ChangeState(levelToSet);
                        PlayerStats.timeInLevel = 0;
                        break;
                    case LevelName.Ice:
                        if (PlayerStats.snowChaotic)
                            break;
                        PlayerStats.snowChaotic = true;
                        levelToSet = new Level(BaseState.currLevel, myPlayer, true);
                        (levelToSet as Level).PositionPlayer();
                        Game1.OBJECT_HANDLER.ChangeState(levelToSet);
                        PlayerStats.timeInLevel = 0;
                        break;
                }

                
            }
        }
    }
}
