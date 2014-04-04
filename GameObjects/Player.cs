﻿using System;
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
        float speed = .5f;
        float speedLimit = 7.5f;
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
            A_BeginAnimation();
            
        }

        public override void Update()
        {

            

            OldPosition = new Vector2(position.X, position.Y);

            if (animating == true)
            {
                if (Game1.TIMER % (60 / framesPerSecond) == 0)
                    frameIndex++;
                if (frameIndex >= animationList[animationListIndex].Count && looping == true)
                    frameIndex = 0;
                else if (frameIndex >= animationList[animationListIndex].Count && looping == false)
                    A_StopAnimating();


            }

            position += velocity;
            velocity += acceleration;

            UpdateCollisionBox();

            if (Game1.KBState.IsKeyDown(Keys.Left))
            {
                acceleration.X = -speed;
                
                A_GoToAnimationIndex(1);
                A_BeginAnimation();
                
            }
            else if (Game1.KBState.IsKeyDown(Keys.Right))
            {
                acceleration.X = speed;
                A_GoToAnimationIndex(0);
                A_BeginAnimation();
            }
            else if (Math.Abs(velocity.X) < .2f && (Math.Floor(velocity.X) == 0 || Math.Ceiling(velocity.X) == 0))
            {
                velocity.X = 0;
                acceleration.X = 0;
                A_StopAnimating();
            }
            if (Game1.KBState.IsKeyUp(Keys.Right) && Game1.oldKBstate.IsKeyDown(Keys.Right))
            {
                if (velocity.X > 0)
                {
                    acceleration.X = -.2f;
                }
                else
                {
                    acceleration.X = .2f;
                    
                }
                if (Math.Abs(velocity.X) < .2f && (Math.Floor(velocity.X) == 0 || Math.Ceiling(velocity.X) == 0))
                {
                    velocity.X = 0;
                    acceleration.X = 0;
                    A_StopAnimating();
                }

            }
            if (Game1.KBState.IsKeyUp(Keys.Left) && Game1.oldKBstate.IsKeyDown(Keys.Left))
            {
                if (velocity.X > 0)
                {
                    acceleration.X = -.2f;
                }
                else
                {
                    acceleration.X = .2f;
                }
                if (Math.Abs(velocity.X) < .2f && (Math.Floor(velocity.X) == 0 || Math.Ceiling(velocity.X) == 0))
                {
                    velocity.X = 0;
                    acceleration.X = 0;
                    A_StopAnimating();
                }

            }
            
            if (Game1.KBState.IsKeyDown(Keys.Up) && !Game1.oldKBstate.IsKeyDown(Keys.Up) && applyGravity ==false)
            {
                velocity.Y -= 10.5f;
                applyGravity = true;
            }
            if (Game1.KBState.IsKeyDown(Keys.LeftAlt) && Game1.KBState.IsKeyDown(Keys.D2) && Game1.oldKBstate.IsKeyUp(Keys.D2))
            {
                Game1.OBJECT_HANDLER.ChangeState(new FlappyBird());
                return;
            }
            if (Game1.KBState.IsKeyDown(Keys.Space) && Game1.oldKBstate.IsKeyUp(Keys.Space))
            {
                Vector2 toMouse = new Vector2(
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.X - this.position.X,
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.Y - this.position.Y);
                toMouse.Normalize();
                float angle = -(float)Math.Atan(toMouse.X / -toMouse.Y);
                toMouse *= 10;
                Projectile temp = new Projectile(toMouse, this.position, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Arrow"]), "Arrow", angle);
            }
            
            if (Game1.KBState.IsKeyDown(Keys.B))
            {
                velocity.X = 0;
                velocity.Y = 0;
                
            }
            if (applyGravity == true)
            {
               acceleration.Y = 0.3f;
            }
            else
            {
                acceleration.Y = 0.0f;
            }
            if (objectBelow == false)
            {
                applyGravity = true;
            }
            if (velocity.X == 0)
            {
                A_StopAnimating();
                A_GoToFrameIndex(0);
            }
            if (velocity.X > speedLimit)
            {
                velocity.X = speedLimit;
            }
            else if (velocity.X < -speedLimit)
            {
                velocity.X = -speedLimit;
            }
            if (position.Y > LevelVariables.HEIGHT * 64)
            {
                position = new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64);
            }

            Game1.CAMERA_DISPLACEMENT = this.position - startingPositon;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //spriteBatch.Draw(animationList[animationListIndex][frameIndex], Game1.CAMERA_DISPLACEMENT, Color.White);
        }

        /// <summary>
        /// Override of the base collisions to incorporate an action to happen when you collide with an object. This one checks what side it collided on and fixes the issues
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
         
            if (determineEvent.ObjectType == "EdgeTile")
            {

                //Did I collide from above?
                if (OldPosition.Y < determineEvent.Position.Y
                   && (OldPosition.X > determineEvent.Position.X + determineEvent.Width
                    || OldPosition.X < determineEvent.Position.X + determineEvent.Width)
                    || OldPosition.Y + Height < determineEvent.Position.Y)
                {
                    applyGravity = false;
                    velocity.Y = 0;
                    position.Y = determineEvent.Position.Y - Height;
                }
                //Did I collide from below?
                else if (OldPosition.Y > determineEvent.Position.Y + determineEvent.Height)
                {
                    velocity.Y = 0;
                    position.Y = determineEvent.Position.Y + determineEvent.Height;
                }
                //Did I collide moving From the left?
                else if (OldPosition.X < determineEvent.Position.X)
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X -  Width;
                    acceleration.X = 0;

                }
                //Did I collide from the right?
                else
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X + determineEvent.Width;
                    acceleration.X = 0;
                }
            }
            
        }

        public override void C_NoCollisions()
        {
            
        }
    }
}
