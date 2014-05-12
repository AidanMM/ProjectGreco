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

namespace ProjectGreco.GameObjects.Buttons
{
    class StartButton : Button
    {
        public StartButton()
            : base (new Vector2(50,50), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "Start", true)
        {            
        }

        public override void DoThisOnClick()
        {
            // start game
            Game1.OBJECT_HANDLER.ChangeState(new HomeWorld());
        }
    }
}
