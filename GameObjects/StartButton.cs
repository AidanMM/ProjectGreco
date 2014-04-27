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

namespace ProjectGreco.GameObjects
{
    class StartButton : Button
    {
        public StartButton()
            : base(new Vector2(0, 0), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["WalkRight"], Game1.ANIMATION_DICTIONARY["WalkLeft"]))
        {

        }

        public override void DoThisOnClick()
        {
            base.DoThisOnClick();
        }
    }
}
