﻿using System;
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
            
            AddObjectToHandler("Player", new Player(new Vector2(600, 300), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["PlayerTest"])));
          //  LevelObjectDictionary["Player"].A_BeginAnimation();
            AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Test"]), new Vector2(400, 400)));

            for (int i = 0; i < 50; i++)
            {
                AddObjectToHandler("Block", new GameObject(new Vector2(500 + i * 51, 600 - i * 20), "Block"), i);
            }
        }
    }
}
