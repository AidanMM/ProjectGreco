using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.Levels;
using ProjectGreco.Skills;

namespace ProjectGreco.GameObjects
{
    class WanderingCam : GameObject
    {

        Vector2 startingPositon;
        int timer = 0;


        public WanderingCam(Vector2 pos) : base()
        {
            startingPositon = new Vector2(600, 320);
            position = pos;
            velocity.X = 3;
            velocity.Y = -.5f;
        }

        public override void Update()
        {
            base.Update();
            timer++;
            if (timer % 4200 == 0)
            {
                velocity.X *= -1;
                
            }
            if (timer % 400 == 0)
            {
                velocity.Y *= -1;
            }
            
            Game1.CAMERA_DISPLACEMENT = this.position - startingPositon;
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
        }
    }
}
