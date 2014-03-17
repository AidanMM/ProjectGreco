using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace ProjectGreco.GameObjects
{
    class Player : GameObject
    {
        float speed = .1f;
        bool applyGravity = true;
        Vector2 startingPositon;
        
        
        
        public Player(Vector2 startPos) : base(startPos, "Player")
        {
            CheckForCollisions = true;
            startingPositon = new Vector2(600, 320);
            position = startPos;
            onScreen = true;
            
        }
        public Player(Vector2 startPos, List<List<Texture2D>> aList)
            : base(aList, startPos, "Player")
        {
            CheckForCollisions = true;
            startingPositon = new Vector2(600, 320);
            position = startPos;
            onScreen = true;            
            
            
        }

        public override void Update()
        {

            OldPosition = new Vector2(position.X, position.Y);


            position += velocity;
            velocity += acceleration;

            UpdateCollisionBox();

            if (animating == true)
            {
                frameIndex++;
                if (frameIndex >= animationList[animationListIndex].Count && looping == true)
                    frameIndex = 0;
                else if (frameIndex >= animationList[animationListIndex].Count && looping == false)
                    A_StopAnimating();


            }
            
            
            if (Game1.KBState.IsKeyDown(Keys.Left))
            {
                velocity.X -= speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Right))
            {
                velocity.X += speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Down))
            {
                velocity.Y += 10;
            }
            if (Game1.KBState.IsKeyDown(Keys.Up) && !Game1.oldKBstate.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 5.0f;
                applyGravity = true;
            }
            if (Game1.KBState.IsKeyDown(Keys.B))
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            if (applyGravity == true)
            {
                acceleration.Y = .2f;
            }
            else
            {
                acceleration.Y = 0.0f;
            }
            Game1.CAMERA_DISPLACEMENT = this.position - startingPositon;
            

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //spriteBatch.Draw(animationList[animationListIndex][frameIndex], Game1.CAMERA_DISPLACEMENT, Color.White);
        }

        /// <summary>
        /// Override of the base collisions to incororate an action to happen when you collide with an object. This one checks what side it collided on and fixes the issues
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
            base.C_OnCollision(determineEvent);
            if (determineEvent.ObjectType == "EdgeTile")
            {
               

                 if (OldPosition.Y < determineEvent.Position.Y)
                {
                 
                    position.Y = determineEvent.Position.Y - this.collisionBox.Height;
                    velocity.Y = 0;
                }
                 if (OldPosition.Y > determineEvent.Position.Y)
                {
                   
                    position.Y = determineEvent.Position.Y + this.collisionBox.Height;
                    velocity.Y = 0;
                }
                 applyGravity = false;


                
            }

            
                
        }
    }
}
