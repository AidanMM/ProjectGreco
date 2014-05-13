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
        JumpTriple,
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
        public int dashTimeLength = 11;
        public int jumpCounter = 0;
        public float strengthOfGravity = .3f;
        public float jumpHeight = 11.5f;

        public int skillPoints = 0;
        public int maxPoints = 8;

        public int health = 3;
        public int maxHealth = 3;

        public int maxStamina = 24;
        public int stamina = 24;

        public int staminaRegenTimer = 0;
        public int lostStaminaTimer = 0;
        public int lostStamina = 24;

        protected int swordTimer = 0;

        

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
            // Stamina regen
            staminaRegenTimer++;

            // Counts down before doing the lost stamina graphic
            if (lostStaminaTimer > 0)
                lostStaminaTimer--;

            // Regens faster if you haven't been spamming
            if (staminaRegenTimer >= 80 && stamina < maxStamina)
            {
                stamina++;
                staminaRegenTimer = 80 - (maxStamina - stamina);
            }

            // Decrease lost stamina when appropriate
            if (lostStaminaTimer <= 0)
            {
                lostStamina--;
            }

            // Make sure it's never less.
            if (lostStamina < stamina)
            {
                lostStamina = stamina;
            }

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
            
            if (dashing)
            {
                if (dashTimer >= dashTimeLength)
                {
                    velocity.X = 0;
                    dashing = false;
                    applyGravity = true;
                    dashTimer = 0;
                }
                else
                {
                    dashTimer++;
                    acceleration.Y = 0;
                    applyGravity = false;
                }
            }
            else if (Game1.KBState.IsKeyDown(Keys.LeftShift) && !Game1.oldKBstate.IsKeyDown(Keys.LeftShift) && Game1.KBState.IsKeyDown(Keys.A) && stamina >= 6)
            {
                if (SkillDash)
                {
                    Dash myDash = new Dash(this, Direction.Left);
                    stamina-= 6;
                    staminaRegenTimer = 0;
                    lostStaminaTimer = 30;
                }
                
            }
            else if (Game1.KBState.IsKeyDown(Keys.LeftShift) && !Game1.oldKBstate.IsKeyDown(Keys.LeftShift) && Game1.KBState.IsKeyDown(Keys.D) && stamina >= 6)
            {

                if (SkillDash)
                {
                    Dash myDash = new Dash(this, Direction.Right);
                    stamina-= 6;
                    staminaRegenTimer = 0;
                    lostStaminaTimer = 30;
                }
                
            }
            #endregion

            #region Skills

            
            // Main skill
            if (Game1.KBState.IsKeyDown(Keys.Space) && Game1.oldKBstate.IsKeyUp(Keys.Space) && stamina >= 8 && availableSkills.Count > 0)
            {
                UseSkill(activeSkillIndex);
                stamina -= 8;
                staminaRegenTimer = 0;
                lostStaminaTimer = 30;
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
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.X - this.position.X ,
                    Game1.OBJECT_HANDLER.objectDictionary["Cursor"].Position.Y - this.position.Y );
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


                
                arrowVel = new Vector2(arrowVel.X + velocity.X, arrowVel.Y);
                
                
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
            if (Game1.mouseState.RightButton == ButtonState.Pressed && Game1.prevMouseState.RightButton == ButtonState.Released && (applyGravity != true || airMelee == true) && swordTimer >= 25)
            {
                new Sword(this);
                EndGhost();
                swordTimer = 0;
            }
            swordTimer++;
            #endregion

            #region Other Events
            if (Game1.KBState.IsKeyDown(Keys.B))
            {
                velocity.X = 0;
                velocity.Y = 0;
                
            }

            if (flashCounter > 0)
            {
                flashCounter--;
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
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (Game1.OBJECT_HANDLER.currentState.LevelType == LevelName.Home && position.X > 3950)
            {
                position.X = 3950;
            }
            else if (position.X > LevelVariables.WIDTH * 64 - 50)
            {
                position.X = LevelVariables.WIDTH * 64 - 50;
            }
            

            #region General Updates

            position += velocity;
            velocity += acceleration;
            UpdateCollisionBox();

            if (destroyThis)
                Destroy();
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
            // Draw the health and stamina empty bars first
            for (int i = 0; i < maxHealth; i++)
            {
                spriteBatch.Draw(Game1.IMAGE_DICTIONARY["EmptyBar"], new Vector2(i * 64, 0), Color.White);
            }
            for (int i = 0; i < maxStamina; i++)
            {
                spriteBatch.Draw(Game1.IMAGE_DICTIONARY["EmptyStaminaBar"], new Vector2(i * 8, 14), Color.White);
            }

            // Draw any of the lost stamina bars if they're there.

            for (int i = 0; i < lostStamina; i++)
            {
                spriteBatch.Draw(Game1.IMAGE_DICTIONARY["LostStaminaBar"], new Vector2(i * 8, 14), Color.White);
            }

            // Draw the health and stamina bars.
            for (int i = 0; i < health; i++)
            {
                spriteBatch.Draw(Game1.IMAGE_DICTIONARY["HealthBar"], new Vector2(i * 64, 0), Color.White);
            }
            for (int i = 0; i < stamina; i++)
            {
                spriteBatch.Draw(Game1.IMAGE_DICTIONARY["StaminaBar"], new Vector2(i * 8, 14), Color.White);
            }



            if(availableSkills.Count > 0)
            spriteBatch.Draw(skillBox[0][skillFrame], new Vector2(0, 28), Color.White);
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
                    || (OldPosition.Y < determineEvent.Position.Y + determineEvent.Height 
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
                    || (OldPosition.X < determineEvent.Position.X + determineEvent.Width 
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
                    || (OldPosition.X < determineEvent.Position.X + determineEvent.Width
                    && OldPosition.X >= determineEvent.Position.X)
                    || (OldPosition.X < determineEvent.Position.X
                    && OldPosition.X + Width > determineEvent.Position.X + determineEvent.Width)))
                {
                    position.Y = determineEvent.Position.Y + determineEvent.Height + 5;
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
                if (determineEvent.ObjectType == "enemy" && flashCounter == 0)
                {
                    flashCounter = 60;
                    BaseEnemy myEnemy = (determineEvent as BaseEnemy);
                    health--;

                    if (health == 0)
                    {
                        health = 3;
                        if (Game1.OBJECT_HANDLER.currentState.LevelType != LevelName.Home)
                        {
                            (Game1.OBJECT_HANDLER.currentState as Level).PositionPlayer();
                        }
                    }

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
