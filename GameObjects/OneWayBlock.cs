using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;
using ProjectGreco.Skills;

namespace ProjectGreco.GameObjects
{
    class OneWayBlock : GameObject
    {
        int timer = 0;
        int timeToDie;
        public OneWayBlock(Vector2 posToSet)
            : base(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["OneWayBlock"]), posToSet, "OneWayBlock")
        {
            Game1.OBJECT_HANDLER.currentState.AddObjectToHandler("OneWayBlock", this);
            timeToDie = 0;
        }
        public OneWayBlock(Vector2 posToSet, int toDie)
            : base(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["OneWayBlock"]), posToSet, "OneWayBlock")
        {
            Game1.OBJECT_HANDLER.currentState.AddObjectToHandler("OneWayBlock", this);
            timeToDie = toDie;
        }


        public override void Update()
        {
            base.Update();
            timer++;

            if (timeToDie > 0 && timer > timeToDie)
            {
                Destroy();
            }
        }
        

        

    }
}
