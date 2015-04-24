﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using System.Linq;
using ProjectGreco.Levels;
using ProjectGreco.GameObjects;

namespace ProjectGreco.GameObjects
{


    class Arrow : Projectile
    {
        public static int count = 0;
        public int id;
        public bool piercing;

        public Arrow(Vector2 vel, Vector2 pos, string name, bool piercing = false) :
            base(vel, pos, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Arrow"]), name, 0)
        {
            acceleration.Y = 0.2f;
            id = count;
            count++;
            this.piercing = piercing;
        }

        public override void Update()
        {
            base.Update();

            if (velocity.X > 0)
            {
                angle = (float)Math.Atan(velocity.Y / velocity.X);
            }
            else
            {
                angle = (float)Math.Atan(velocity.Y / velocity.X) + (float)Math.PI;
            }


        }
    }
}