using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using ProjectGreco.Levels;
using ProjectGreco.GameObjects;


namespace ProjectGreco.GameObjects
{
    class ThrowingDagger : Projectile
    {

        public ThrowingDagger(Vector2 velocity, Vector2 startPos)
            : base(velocity, startPos, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Swords"]), "ThrowingDagger", 0)
        {

        }
    }
}
