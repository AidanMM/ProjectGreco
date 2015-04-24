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
    class ConfuseRay
    {
        public ConfuseRay(Player myPlayer)
        {
            try
            {
                (Game1.OBJECT_HANDLER.objectDictionary[Game1.TITLE_STRING] as BaseEnemy).Confuse(90);
            }
            catch
            {

            }

        }
    }
}
