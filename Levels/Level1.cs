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
    class Level1 : BaseState
    {
        public Level1()
            : base()
        {
            
            AddObjectToHandler("Player", new Player(new Vector2(200, 200), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Test"])));
            LevelObjectDictionary["Player"].A_BeginAnimation();
           

            for (int i = 0; i < 15; i++)
            {
                AddObjectToHandler("Block", new GameObject(new Vector2(0 + i * 51, 400), "Block"));
            }
        }
    }
}
