﻿using System;
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
    enum ActionSkills
    {
        ChaoticReset,
        ConfuseRay,
        Exile,
        Ghost,
        LightJump,
        LightWall,
        ShadowDagger,
        ShadowHold,
        ShadowPush
    }

    class Player : GameObject
    {
        public float speed = .5f;
        public float dashVelocity = 30.0f;
        public float speedLimit = 7.5f;
        public bool applyGravity = true;
        public bool canDash = false;
        public bool dashing = false;
        public int dashTimer = 0;
        public int dashTimeLength = 10;
        public int jumpCounter = 0;
        private int maximumJumps;
        private int activeSkillIndex;
        Vector2 startingPositon;
        List<ActionSkills> availableSkills = new List<ActionSkills>();

        // A // means the skill has yet to actually be implemented yet
        #region SKILLS
        public bool SkillChaoticReset = false;//
        public bool SkillConfuseRay = false;//
        public bool SkillDash = true;
        public bool SkillDoubleJump = true;
        public bool SkillExile = false;//
        public bool SkillFastFall = false;//
        public bool SkillGhost = false;//
        public bool SkillJumpHeight = false;//
        public bool SkillLightJump = false;//
        public bool SkillLightWall = false;//
        public bool SkillMeleeAir = false;//
        public bool SkillRangedAir = false;//
        public bool SkillShadowDagger = false;//
        public bool SkillShadowHold = false;//
        public bool SkillShadowPush = false;//
        public bool SkillSpeed = false;//
        public bool SkillTripleJump = false;
        public bool SkillWings = true;
        #endregion


        public Player(Vector2 startPos) : base(startPos, "Player")
        {
            CheckForCollisions = true;
            startingPositon = new Vector2(600, 320);
            position = startPos;
            onScreen = true;
            zOrder = 1;

            #region Skill Setup
            // Setup the player's ability to jump multiple times
            maximumJumps = 1;

            if (SkillDoubleJump)
                maximumJumps++;
            if (SkillTripleJump)
                maximumJumps++;

            // Create the player's list of active skills
            if (SkillChaoticReset)
                availableSkills.Add(ActionSkills.ChaoticReset);
            if (SkillConfuseRay)
                availableSkills.Add(ActionSkills.ConfuseRay);
            if (SkillExile)
                availableSkills.Add(ActionSkills.Exile);
            if (SkillGhost)
                availableSkills.Add(ActionSkills.Ghost);
            if (SkillLightJump)
                availableSkills.Add(ActionSkills.LightJump);
            if (SkillLightWall)
                availableSkills.Add(ActionSkills.LightWall);
            if (SkillShadowDagger)
                availableSkills.Add(ActionSkills.ShadowDagger);
            if (SkillShadowHold)
                availableSkills.Add(ActionSkills.ShadowHold);
            if (SkillShadowPush)
                availableSkills.Add(ActionSkills.ShadowPush);

            #endregion

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

            #region Skill Setup
            // Setup the player's ability to jump multiple times
            maximumJumps = 1;

            if (SkillDoubleJump)
                maximumJumps++;
            if (SkillTripleJump)
                maximumJumps++;

            // Create the player's list of active skills
            if (SkillChaoticReset)
                availableSkills.Add(ActionSkills.ChaoticReset);
            if (SkillConfuseRay)
                availableSkills.Add(ActionSkills.ConfuseRay);
            if (SkillExile)
                availableSkills.Add(ActionSkills.Exile);
            if (SkillGhost)
                availableSkills.Add(ActionSkills.Ghost);
            if (SkillLightJump)
                availableSkills.Add(ActionSkills.LightJump);
            if (SkillLightWall)
                availableSkills.Add(ActionSkills.LightWall);
            if (SkillShadowDagger)
                availableSkills.Add(ActionSkills.ShadowDagger);
            if (SkillShadowHold)
                availableSkills.Add(ActionSkills.ShadowHold);
            if (SkillShadowPush)
                availableSkills.Add(ActionSkills.ShadowPush);

            #endregion
        }

        public override void Update()
        {

            

            OldPosition = new Vector2(position.X, position.Y);

            #region Animation
            if (animating == true)
            {
                if (Game1.TIMER % (60 / framesPerSecond) == 0)
                    frameIndex++;
                if (frameIndex >= animationList[animationListIndex].Count && looping == true)
                    frameIndex = 0;
                else if (frameIndex >= animationList[animationListIndex].Count && looping == false)
                    A_StopAnimating();


            }
            #endregion

            #region General Updates

            position += velocity;
            velocity += acceleration;
            UpdateCollisionBox();
            #endregion

            #region General Movement
            if (Game1.KBState.IsKeyDown(Keys.A))
            {
                acceleration.X = -speed;
                
                A_GoToAnimationIndex(1);
                A_BeginAnimation();
                
            }
            else if (Game1.KBState.IsKeyDown(Keys.D))
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
            if (Game1.KBState.IsKeyUp(Keys.D) && Game1.oldKBstate.IsKeyDown(Keys.D))
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
            if (Game1.KBState.IsKeyUp(Keys.A) && Game1.oldKBstate.IsKeyDown(Keys.A))
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
            
            if (Game1.KBState.IsKeyDown(Keys.W) && !Game1.oldKBstate.IsKeyDown(Keys.W) && jumpCounter < maximumJumps)
            {
                velocity.Y = -10.5f;
                applyGravity = true;
                jumpCounter++;
            }
            if (Game1.KBState.IsKeyDown(Keys.W) && Game1.oldKBstate.IsKeyDown(Keys.W) && SkillWings && velocity.Y > 2.5f)
            {
                velocity.Y = 2.5f;
            }
            #endregion

            #region Dashing
            
            if (Game1.KBState.IsKeyDown(Keys.LeftShift) && Game1.KBState.IsKeyDown(Keys.A) && canDash == true)
            {
                if (SkillDash)
                {
                    Dash myDash = new Dash(this, Direction.Left);
                }
                
            }
            else if (Game1.KBState.IsKeyDown(Keys.LeftShift) && Game1.KBState.IsKeyDown(Keys.D) && canDash == true)
            {

                if (SkillDash)
                {
                    Dash myDash = new Dash(this, Direction.Right);
                }
                
            }
            else if (dashing == true)
            {
                dashTimer = 0;
                velocity.X = 0;
                dashing = false;
                canDash = false;
                if (velocity.X > speedLimit)
                    velocity.X = speedLimit;

                if (velocity.X < -speedLimit)
                    velocity.X = -speedLimit;
            }
            if (Game1.KBState.IsKeyDown(Keys.LeftShift) && Game1.oldKBstate.IsKeyUp(Keys.LeftShift))
            {
                canDash = true;
            }
            if (Game1.KBState.IsKeyDown(Keys.LeftShift) && Game1.oldKBstate.IsKeyUp(Keys.LeftShift))
            {
                canDash = true;
            }
            
            

           

            #endregion

            #region Skills

            // Main skill
            if (Game1.KBState.IsKeyDown(Keys.Space))
            {
                UseSkill(activeSkillIndex);
            }
            // Secondary Skill
            if (Game1.KBState.IsKeyDown(Keys.LeftShift))
            {
                int secondaryIndex;
                if (activeSkillIndex + 1 == availableSkills.Count)
                {
                    secondaryIndex = 0;
                }
                else
                    secondaryIndex = activeSkillIndex + 1;

                UseSkill(secondaryIndex);
                
            }
            // Change Skill
            if (Game1.KBState.IsKeyDown(Keys.E) && !Game1.oldKBstate.IsKeyDown(Keys.E))
            {
                if (activeSkillIndex + 1 == availableSkills.Count)
                {
                    activeSkillIndex = 0;
                }
                else
                {
                    activeSkillIndex++;
                }
            }
            else if (Game1.KBState.IsKeyDown(Keys.Q) && !Game1.oldKBstate.IsKeyDown(Keys.Q))
            {
                if (activeSkillIndex - 1 < 0)
                {
                    activeSkillIndex = availableSkills.Count - 1;
                }
                else
                {
                    activeSkillIndex--;
                }
            }

            #endregion

            #region Frappy
            if (Game1.KBState.IsKeyDown(Keys.LeftAlt) && Game1.KBState.IsKeyDown(Keys.D2) && Game1.oldKBstate.IsKeyUp(Keys.D2))
            {
                Game1.OBJECT_HANDLER.ChangeState(new FlappyBird());
                return;
            }
            #endregion

            #region Shooting
            if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 toMouse = new Vector2(
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.X - this.position.X,
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.Y - this.position.Y);
                Vector2 dir = toMouse;
                
                dir.Normalize();
                float angle = (float)Math.Atan(toMouse.Y / toMouse.X);
                toMouse *= 10;
                //toMouse += velocity;
                Vector2 arrowVel = new Vector2(dir.X * toMouse.Length()/200 , dir.Y * toMouse.Length()/200);
                if (toMouse.X > 0)
                {
                    Projectile temp = new Arrow(arrowVel, this.position, "Arrow");
                }
                else
                {
                    Projectile temp = new Arrow(arrowVel, this.position, "Arrow");
                }
            }

            #endregion

            #region Other Events
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
            if (velocity.X > speedLimit && dashing == false)
            {
                velocity.X = speedLimit;
            }
            else if (velocity.X < -speedLimit && dashing == false)
            {
                velocity.X = -speedLimit;
            }
            if (position.Y > LevelVariables.HEIGHT * 64)
            {
                position = new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64);
            }
            

            Game1.CAMERA_DISPLACEMENT = this.position - startingPositon;
            #endregion
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
                    jumpCounter = 0;
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

        public void UseSkill(int skillIndex)
        {
            if (availableSkills.Count == 0)
            {
                return;
            }

            switch (availableSkills[skillIndex])
            {
                case ActionSkills.ChaoticReset:
                    ChaoticReset myChaoticReset = new ChaoticReset(this);
                    break;
                case ActionSkills.ConfuseRay:
                    ConfuseRay myConfuseRay = new ConfuseRay(this);
                    break;
                case ActionSkills.Exile:
                    Exile myExile = new Exile(this);
                    break;
                case ActionSkills.Ghost:
                    Ghost myGhost = new Ghost(this);
                    break;
                case ActionSkills.LightJump:
                    LightJump myLightJump = new LightJump(this);
                    break;
                case ActionSkills.LightWall:
                    LightWall myLightWall = new LightWall(this);
                    break;
                case ActionSkills.ShadowDagger:
                    ShadowDagger myShadowDagger = new ShadowDagger(this);
                    break;
                case ActionSkills.ShadowHold:
                    ShadowHold myShadowHold = new ShadowHold(this);
                    break;
                case ActionSkills.ShadowPush:
                    ShadowPush myShadowPush = new ShadowPush(this);
                    break;

                default:
                    break;
            }
        }
    }
}
