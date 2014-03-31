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
    class HomeWorld : BaseState
    {
        public HomeWorld()
        {
            AddObjectToHandler("Player", new Player(new Vector2(200, 450), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["WalkRight"], Game1.ANIMATION_DICTIONARY["WalkLeft"])));
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, 0), Game1.IMAGE_DICTIONARY["cursor"]));
            AddObjectToHandler("Ground", new GameObject(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["tempGround"]), new Vector2(0, 500), "EdgeTile"));
        }
    }
}
