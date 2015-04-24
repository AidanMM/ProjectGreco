using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;
using ProjectGreco.Skills;
using ProjectGreco.GameObjects;


namespace ProjectGreco.GameObjects
{
    class TemporaryWall : GameObject
    {
        protected int timer = 0;

        protected int timeToLive = 10;

        public TemporaryWall(int ToLive, Player myPlayer)
            : base(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["LightWallBlock"]), new Vector2(myPlayer.Position.X , myPlayer.Position.Y), "EdgeTile")
        {
            this.timeToLive = ToLive;
            if (myPlayer.HFlip == true)
            {
                position = new Vector2(myPlayer.Position.X - 50, myPlayer.Position.Y);
                hFlip = true;
            }
            else
            {
                position = new Vector2(myPlayer.Position.X + myPlayer.Width + 50, myPlayer.Position.Y);
            }

            Game1.OBJECT_HANDLER.AddObjectToHandler("LightWall", this);
        }

        public override void Update()
        {
            base.Update();

            timer++;
            if (timer >= timeToLive)
            {
                Destroy();
            }
        }
    }
}
