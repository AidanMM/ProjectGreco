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
    class ShadowHold
    {
        public ShadowHold(Player myPlayer)
        {
            try
            {
                (Game1.OBJECT_HANDLER.objectDictionary[Game1.TITLE_STRING] as BaseEnemy).ShadowHold(90);
            }
            catch
            {

            }
        }
    }
}
