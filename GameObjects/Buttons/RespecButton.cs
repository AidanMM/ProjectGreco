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
    class RespecButton : Button
    {
        SkillTree skillTree;
        
        public RespecButton(Vector2 pos, SkillTree skTree, BaseState toAdd)
            : base(pos, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "Respec", true)
        {
            skillTree = skTree;
            toAdd.AddObjectToHandler("Respec Button", this);
            zOrder = -5;
        }

        public override void DoThisOnClick()
        {
            skillTree.ResetPlayer();
        }
    }
}
