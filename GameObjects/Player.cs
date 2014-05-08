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
    public enum ActionSkills
    {
        ChaoticReset,
        ConfuseRay,
        Exile,
        Ghost,
        LightJump,
        LightWall,
        ShadowDagger,
        ShadowHold,
        ShadowPush,
        Dash,
        AirMelee,
        AirRanged,
        FastFall,
        JumpHeight,
        JumpPlus,
        Speed,
        Wings
    }

    public class Player : GameObject
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
        public float strengthOfGravity = .3f;
        public float jumpHeight = 10.5f;


        public int skillPoints = 0;
        public int maxPoints = 8;

        

        /// <summary>
        /// This bool is used to manage the ghosting state of the player
        /// </summary>
        protected bool ghosting = false;

        protected int ghostTimer = 0;

        protected int ghostLimit = 10;

        public bool airMelee = false;

        public bool airRanged = false;
        
        /// <summary>
        /// The maximum jumps that the player has
        /// </summary>
        private int maximumJumps;
		
        public int MaximumJumps
        {
            get { return maximumJumps; }
            set { maximumJumps = value; }
        }
        private int activeSkillIndex;
        Vector2 startingPositon;
        public List<ActionSkills> availableSkills = new List<ActionSkills>();

        protected List<List<Texture2D>> skillBox = Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["SkillBox"]);

        protected int skillFrame = 0;

        // A // means the skill has yet to actually be implemented yet
        #region SKILLS
        public bool SkillChaoticReset = false;//
        public bool SkillConfuseRay = false;//
        public bool SkillDash = false;
        public bool SkillDoubleJump = false;
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
        public bool SkillWings = false;
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
            //SkillLightJump = true;
            //SkillShadowDagger = true;
            //SkillExile = true;
            //SkillChaoticReset = true;
            //SkillShadowPush = true;
            //SkillShadowHold = true;
            //SkillGhost = true;
            //SkillLightWall = true;
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

            

            #region General Movement
            if (Game1.KBState.IsKeyDown(Keys.A))
            {
                acceleration.X = -speed;
                
                A_GoToAnimationIndex(0);
                A_BeginAnimation();
                hFlip = true;
                
            }
            else if (Game1.KBState.IsKeyDown(Keys.D))
            {
                acceleration.X = speed;
                A_GoToAnimationIndex(0);
                A_BeginAnimation();
                hFlip = false;
            }
            
            else if (Math.Abs(velocity.X) < .2f && (Math.Floor(velocity.X) == 0 || Math.Ceiling(velocity.X) == 0))
            {
                velocity.X = 0;
                acceleration.X = 0;
                A_GoToAnimationIndex(0);
                A_GoToFrameIndex(0);
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
                velocity.Y = -jumpHeight;
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
            if (Game1.KBState.IsKeyDown(Keys.Space) && Game1.oldKBstate.IsKeyUp(Keys.Space))
            {
                UseSkill(activeSkillIndex);
                
            }
            // Secondary Skill
            //if (Game1.KBState.IsKeyDown(Keys.LeftShift) && Game1.oldKBstate.IsKeyUp(Keys.LeftShift))
            //{
            //    int secondaryIndex;
            //    if (activeSkillIndex + 1 == availableSkills.Count)
            //    {
            //        secondaryIndex = 0;
            //    }
            //    else
            //        secondaryIndex = activeSkillIndex + 1;

            //    UseSkill(secondaryIndex);
                
            //}
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
                if (availableSkills.Count == 0)
                {

                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.ChaoticReset)
                {
                    skillFrame = 0;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.ConfuseRay)
                {
                    skillFrame = 1;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.Exile)
                {
                    skillFrame = 2;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.Ghost)
                {
                    skillFrame = 3;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.LightJump)
                {
                    skillFrame = 4;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.LightWall)
                {
                    skillFrame = 5;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowDagger)
                {
                    skillFrame = 6;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowHold)
                {
                    skillFrame = 7;
                }
                else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowPush)
                {
                    skillFrame = 8;
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
                 if (availableSkills.Count == 0)
                 {

                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.ChaoticReset)
                 {
                     skillFrame = 0;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.ConfuseRay)
                 {
                     skillFrame = 1;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.Exile)
                 {
                     skillFrame = 2;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.Ghost)
                 {
                     skillFrame = 3;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.LightJump)
                 {
                     skillFrame = 4;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.LightWall)
                 {
                     skillFrame = 5;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowDagger)
                 {
                     skillFrame = 6;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowHold)
                 {
                     skillFrame = 7;
                 }
                 else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowPush)
                 {
                     skillFrame = 8;
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
            if (Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.prevMouseState.LeftButton == ButtonState.Released && (applyGravity == false || airRanged == true))
            {
                Vector2 toMouse = new Vector2(
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.X - this.position.X + Width / 2,
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.Y - this.position.Y + Height / 2);
                Vector2 dir = toMouse;
                
                dir.Normalize();
                float angle = (float)Math.Atan(toMouse.Y / toMouse.X);
                toMouse *= 10;
                //toMouse += velocity;
                Vector2 arrowVel = new Vector2(0, 0);
                if (toMouse.Length() / 200 > 20)
                {
                    arrowVel = new Vector2(dir.X * 20, dir.Y * 20);
                }
                else if (toMouse.Length() / 200 < 10)
                {
                    arrowVel = new Vector2(dir.X * 10, dir.Y * 10);
                }
                else
                    arrowVel = new Vector2(dir.X * toMouse.Length() / 200, dir.Y * toMouse.Length() / 200);
                
                
                if (toMouse.X > 0) 
                {
                    Projectile temp = new Arrow(arrowVel, this.position + new Vector2(Width /2, Height/2), "Arrow");
                }
                else
                {
                    Projectile temp = new Arrow(arrowVel, this.position + new Vector2(Width / 2, Height / 2), "Arrow");
                }
                EndGhost();
            }

            #endregion

            #region Sword
            if (Game1.mouseState.RightButton == ButtonState.Pressed && Game1.prevMouseState.RightButton == ButtonState.Released && (applyGravity != true || airMelee == true))
            {
                new Sword(this);
                EndGhost();
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
                acceleration.Y = strengthOfGravity;
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
            if (ghosting == true)
            {
                ghostTimer++;
                if (ghostTimer >= ghostLimit)
                {
                    ghostTimer = 0;
                    ghosting = false;
                }
            }

            #region General Updates

            position += velocity;
            velocity += acceleration;
            UpdateCollisionBox();
            #endregion
            Game1.CAMERA_DISPLACEMENT = this.position - startingPositon;
            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ghosting == false)
            {
                base.Draw(spriteBatch);
            }
            else if(ghosting == true)
            {
                base.Draw(spriteBatch, Color.Black);
            }
            if(availableSkills.Count > 0)
            spriteBatch.Draw(skillBox[0][skillFrame], new Vector2(0, 0), Color.White);
            if (activeSkillIndex >= availableSkills.Count)
            {
                activeSkillIndex = availableSkills.Count - 1;
                if (availableSkills.Count == 0)
                {
                    activeSkillIndex = 0;
                }
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.ChaoticReset)
            {
                skillFrame = 0;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.ConfuseRay)
            {
                skillFrame = 1;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.Exile)
            {
                skillFrame = 2;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.Ghost)
            {
                skillFrame = 3;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.LightJump)
            {
                skillFrame = 4;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.LightWall)
            {
                skillFrame = 5;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowDagger)
            {
                skillFrame = 6;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowHold)
            {
                skillFrame = 7;
            }
            else if (availableSkills[activeSkillIndex] == ActionSkills.ShadowPush)
            {
                skillFrame = 8;
            }
        }

        /// <summary>
        /// Override of the base collisions to incorporate an action to happen when you collide with an object. This one checks what side it collided on and fixes the issues
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
         
            if (determineEvent.ObjectType == "EdgeTile")
            {
                OldPosition = new Vector2(OldPosition.X, OldPosition.Y - velocity.Y);
                
                if (Math.Floor(OldPosition.X) <= determineEvent.Position.X 
                    && ( (OldPosition.Y + Height >= determineEvent.Position.Y
                    && OldPosition.Y + Height <= determineEvent.Position.Y + determineEvent.Height)
                    || (OldPosition.Y <= determineEvent.Position.Y + determineEvent.Height 
                    && OldPosition.Y >= determineEvent.Position.Y)))
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X - Width;
                    acceleration.X = 0;
                }
                else if (OldPosition.X >= determineEvent.Position.X + determineEvent.Width
                    && ( (OldPosition.Y + Height >= determineEvent.Position.Y
                    && OldPosition.Y + Height <= determineEvent.Position.Y + determineEvent.Height)
                    || (OldPosition.Y <= determineEvent.Position.Y + determineEvent.Height 
                    && OldPosition.Y >= determineEvent.Position.Y)))
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X + determineEvent.Width;
                    acceleration.X = 0;
                }
                else if(Math.Floor(OldPosition.Y + Height) <= determineEvent.Position.Y
                    && ( (OldPosition.X + Width - 1 > determineEvent.Position.X  
                    && OldPosition.X + Width  <= determineEvent.Position.X + determineEvent.Width)
                    || (OldPosition.X <= determineEvent.Position.X + determineEvent.Width 
                    && OldPosition.X >= determineEvent.Position.X )
                    || (OldPosition.X < determineEvent.Position.X
                    && OldPosition.X + Width  > determineEvent.Position.X + determineEvent.Width)))
                {
                    applyGravity = false;
                    jumpCounter = 0;
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    position.Y = determineEvent.Position.Y - Height;
                }
                else if (Math.Floor(OldPosition.Y) > determineEvent.Position.Y + determineEvent.Height
                    && position.Y < determineEvent.Position.Y + determineEvent.Height
                    && ((OldPosition.X + Width - 1 > determineEvent.Position.X
                    && OldPosition.X + Width <= determineEvent.Position.X + determineEvent.Width)
                    || (OldPosition.X <= determineEvent.Position.X + determineEvent.Width
                    && OldPosition.X >= determineEvent.Position.X)
                    || (OldPosition.X < determineEvent.Position.X
                    && OldPosition.X + Width > determineEvent.Position.X + determineEvent.Width)))
                {
                    position.Y = determineEvent.Position.Y + determineEvent.Height;
                    velocity.Y = 0;

                }

            }
            if (determineEvent.ObjectType == "OneWayBlock")
            {
                if (Math.Floor(OldPosition.Y + Height) <= determineEvent.Position.Y
                    && ((OldPosition.X + Width - 1 > determineEvent.Position.X
                    && OldPosition.X + Width <= determineEvent.Position.X + determineEvent.Width)
                    || (OldPosition.X <= determineEvent.Position.X + determineEvent.Width
                    && OldPosition.X >= determineEvent.Position.X)
                    || (OldPosition.X < determineEvent.Position.X
                    && OldPosition.X + Width > determineEvent.Position.X + determineEvent.Width)))
                {
                    applyGravity = false;
                    jumpCounter = 0;
                    velocity.Y = 0;
                    acceleration.Y = 0;
                    position.Y = determineEvent.Position.Y - Height;
                }
            }

            //Do the collision code for checking against enemies here. We don't have any yet, but do it inside of the ghosting check
            if (ghosting == false)
            {

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
            if (skillIndex >= availableSkills.Count)
            {
                skillIndex = availableSkills.Count - 1;
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

        public void Ghost(int timeToGhost)
        {
            ghosting = true;
            ghostLimit = timeToGhost;
            
        }

        public void EndGhost()
        {
            ghosting = false;
            ghostTimer = 0;
        }

        
    }
}
