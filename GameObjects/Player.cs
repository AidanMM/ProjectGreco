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
    class Player : GameObject
    {
        float speed = 3.0f;
        bool applyGravity = true;
        Vector2 startingPositon;
        
        
        
        public Player(Vector2 startPos) : base(startPos, "Player")
        {
            CheckForCollisions = true;
            startingPositon = new Vector2(600, 320);
            position = startPos;
            onScreen = true;
            zOrder = 1;
            
        }
        public Player(Vector2 startPos, List<List<Texture2D>> aList)
            : base(aList, startPos, "Player")
        {
            CheckForCollisions = true;
            startingPositon = new Vector2(600, 320);
            position = startPos;
            onScreen = true;
            zOrder = 1;
            
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
                velocity.X = -speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Right))
            {
                velocity.X = speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Down))
            {
                applyGravity = true;
            }
            if (Game1.KBState.IsKeyDown(Keys.Up) && !Game1.oldKBstate.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 7.5f;
                applyGravity = true;
            }
            if (Game1.KBState.IsKeyDown(Keys.LeftAlt) && Game1.KBState.IsKeyDown(Keys.D2) && Game1.oldKBstate.IsKeyUp(Keys.D2))
            {
                Game1.OBJECT_HANDLER.ChangeState(new FlappyBird());
                return;
            }
            if (Game1.KBState.IsKeyDown(Keys.Space) && Game1.oldKBstate.IsKeyUp(Keys.Space))
            {
                Projectile temp = new Projectile(new Vector2(10, 0), this.position, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Arrow"]), "Arrow");
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
                
                if (OldPosition.Y + animationList[animationListIndex][frameIndex].Height > determineEvent.Position.Y)
                {
                    position = new Vector2(OldPosition.X - velocity.X, OldPosition.Y);
                    if (Math.Abs(velocity.X) > Math.Abs(velocity.Y))
                    {
                        velocity.X = 0;
                    }
                }
                else
                {
                    position = OldPosition;
                }
                velocity.Y = 0;
               


                 applyGravity = false;


                
            }
        }
    }
}
