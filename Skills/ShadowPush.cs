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
    class ShadowPush
    {
        public ShadowPush(Player myPlayer)
        {
            try
            {
                BaseEnemy toPush = (Game1.OBJECT_HANDLER.objectDictionary[Game1.TITLE_STRING] as BaseEnemy);

                Vector2 pushDir = new Vector2(myPlayer.Position.X + myPlayer.Width / 2 - toPush.Position.X, myPlayer.Position.Y + myPlayer.Height / 2 - toPush.Position.Y);

                pushDir.Normalize();

                pushDir *= -20;

                toPush.Velocity = pushDir;
            }
            catch
            {

            }
            
        }
    }
}
