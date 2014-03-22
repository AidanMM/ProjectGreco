using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.GameObjects;

namespace ProjectGreco.Levels
{
    class FlappyBird : BaseState
    {
        public bool gameStart = false;

        public FlappyBird()
            : base()
        {
            AddObjectToHandler("Frappy", new FrappyBird(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Frappy"])));

            AddObjectToHandler("Pipe", new Pipe(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Pipe"]), new Vector2(850, -500)));
            AddObjectToHandler("Pipe", new Pipe(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Pipe"]), new Vector2(1200, -300)));
            AddObjectToHandler("Pipe", new Pipe(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Pipe"]), new Vector2(1550, -600)));
            AddObjectToHandler("Pipe", new Pipe(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Pipe"]), new Vector2(1900, -600)));
            
        }

        



    }
}
