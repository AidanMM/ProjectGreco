using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;
using ProjectGreco.Levels;

namespace ProjectGreco
{
    class BossWeapon : GameObject
    {
        public BossWeapon(List<List<Texture2D>> animationList, Vector2 pos)
            : base(animationList, pos, "BossWeapon")
        {
            
        }
    }
}
