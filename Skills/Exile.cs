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
    class Exile
    {
        public Exile(Player myPlayer)
        {
            try
            {
                (Game1.OBJECT_HANDLER.objectDictionary[Game1.TITLE_STRING] as BaseEnemy).Exile(60);
            }
            catch
            {

            }
        }

       
    }
}
