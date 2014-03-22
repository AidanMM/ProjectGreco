using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects
{
    class FrappyBird : GameObject
    {
        public FrappyBird(List<List<Texture2D>> aList) : base(aList, new Vector2(200,500), "Frappy")
        {
            acceleration.Y = .5f;
            onScreen = true;
            checkForCollisions = true;
        }

        public override void Update()
        {
            
            bool gameStart = (Game1.OBJECT_HANDLER.currentState as FlappyBird).gameStart;
            if (Game1.KBState.IsKeyDown(Keys.Space))
            {
                gameStart = true;
                (Game1.OBJECT_HANDLER.currentState as FlappyBird).gameStart = true;
            }
            if(gameStart == true)
            {
                base.Update();
                if (Game1.KBState.IsKeyDown(Keys.Space) && Game1.oldKBstate.IsKeyUp(Keys.Space))
                {
                    velocity.Y -= 15;
                    if (velocity.Y > 0)
                    {
                        velocity.Y = 0;
                    }
                
                }
                if (position.Y > 720 || position.Y < -100)
                {
                    position.Y = 360;
                    velocity.Y = 0;
                }
                if (velocity.Y < -15)
                {
                    velocity.Y = -15;
                }
                if (velocity.Y > 20)
                {
                    velocity.Y = 20;
                }

                
            }
            if (Game1.KBState.IsKeyDown(Keys.LeftAlt) && Game1.KBState.IsKeyDown(Keys.D2)  && Game1.oldKBstate.IsKeyUp(Keys.D2))
            {
                Game1.OBJECT_HANDLER.ChangeState(new Level1());
            }
            

        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            base.C_OnCollision(determineEvent);

            if (determineEvent.ObjectType == "Pipe")
            {
                position.Y = 360;
                velocity.Y = 0;
            }

        }

        
    }
}
