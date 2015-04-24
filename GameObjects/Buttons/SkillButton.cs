﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects.Buttons
{
    class SkillButton : Button
    {
        /// <summary>
        /// The action skill that will be activated when this button is clicked
        /// </summary>
        ActionSkills skillToSet;
        /// <summary>
        /// The player who will gain the skill once it is activated
        /// </summary>
        Player myPlayer;
        public SkillButton parent;

        /// <summary>
        /// This bool controls the activated state of the button
        /// </summary>
        public bool active;

        /// <summary>
        /// This bool will trigger whether or not information about this skill will be shown
        /// </summary>
        public bool displayText = false;

        public string abiltyText = "Ooops! Something went kind of wrong\n if you are seeing this!";

        /// <summary>
        /// The constructor for the button class, this takes the starting position, the player, and the Playskill that this object will activate
        /// </summary>
        /// <param name="startPos">Starting Position for the object</param>
        /// <param name="toSet">The Player to pass in</param>
        /// <param name="playerSkill">The skill that this button will activate</param>
        public SkillButton(Vector2 startPos, Player toSet, ActionSkills playerSkill)
            : base(startPos, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["SkillBox"]), "SkillButton", true)
        {
            position = startPos;
            skillToSet = playerSkill;
            myPlayer = toSet;
            parent = null;
            active = false;

            switch (skillToSet)
            {
                case ActionSkills.ChaoticReset:
                    A_GoToFrameIndex(0);
                    break;
                case ActionSkills.ConfuseRay:
                    A_GoToFrameIndex(1);
                    break;
                case ActionSkills.Exile:
                    A_GoToFrameIndex(2);
                    break;
                case ActionSkills.Ghost:
                    A_GoToFrameIndex(3);
                    break;
                case ActionSkills.LightJump:
                    A_GoToFrameIndex(4);
                    break;
                case ActionSkills.LightWall:
                    A_GoToFrameIndex(5);
                    break;
                case ActionSkills.ShadowDagger:
                    A_GoToFrameIndex(6);
                    break;
                case ActionSkills.ShadowHold:
                    A_GoToFrameIndex(7);
                    break;
                case ActionSkills.ShadowPush:
                    A_GoToFrameIndex(8);
                    break;
                default:
                    A_GoToFrameIndex(0);
                    break;
            }
        }

        /// <summary>
        /// The same as the defaul constructor except that it now takes a level to automaticaly add this object to the game.
        /// </summary>
        /// <param name="startPos">Starting position</param>
        /// <param name="toSet">the player to pass</param>
        /// <param name="playerSkill">The player skill to set</param>
        /// <param name="level">the level to add this object too</param>
        public SkillButton(Vector2 startPos, Player toSet, ActionSkills playerSkill, BaseState level)
            : base(startPos, Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["SkillBox"]), "SkillButton", true)
        {
            position = startPos;
            skillToSet = playerSkill;
            myPlayer = toSet;
            parent = null;
            active = false;

            switch (skillToSet)
            {
                case ActionSkills.ChaoticReset:
                    A_GoToFrameIndex(0);
                    break;
                case ActionSkills.ConfuseRay:
                    A_GoToFrameIndex(1);
                    break;
                case ActionSkills.Exile:
                    A_GoToFrameIndex(2);
                    break;
                case ActionSkills.Ghost:
                    A_GoToFrameIndex(3);
                    break;
                case ActionSkills.LightJump:
                    A_GoToFrameIndex(4);
                    break;
                case ActionSkills.LightWall:
                    A_GoToFrameIndex(5);
                    break;
                case ActionSkills.ShadowDagger:
                    A_GoToFrameIndex(6);
                    break;
                case ActionSkills.ShadowHold:
                    A_GoToFrameIndex(7);
                    break;
                case ActionSkills.ShadowPush:
                    A_GoToFrameIndex(8);
                    break;
                case ActionSkills.Dash:
                    A_GoToFrameIndex(9);
                    break;
                case ActionSkills.AirMelee:
                    A_GoToFrameIndex(10);
                    break;
                case ActionSkills.AirRanged:
                    A_GoToFrameIndex(11);
                    break;
                case ActionSkills.FastFall:
                    A_GoToFrameIndex(12);
                    break;
                case ActionSkills.JumpHeight:
                    A_GoToFrameIndex(13);
                    break;
                case ActionSkills.JumpPlus:
                    A_GoToFrameIndex(14);
                    break;
                case ActionSkills.JumpTriple:
                    A_GoToFrameIndex(14);
                    break;
                case ActionSkills.Speed:
                    A_GoToFrameIndex(15);
                    break;
                case ActionSkills.Wings:
                    A_GoToFrameIndex(16);
                    break;
                default:
                    A_GoToFrameIndex(0);
                    break;
            }
            zOrder = -5;
            level.AddObjectToHandler("SkillButton", this);
            ShowActive();
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            ShowActive();
            if (active == false)
                base.Draw(spriteBatch, Color.Gray);
            else
                base.Draw(spriteBatch, Color.White);

            if (displayText == true)
            {
                // new Vector2(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X + Width / 2, collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y + Height / 2),
                spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, abiltyText, 
                    new Vector2(this.position.X - Game1.CAMERA_DISPLACEMENT.X - Width * 3 , this.position.Y - Game1.CAMERA_DISPLACEMENT.Y ), 
                        Color.White);
            }
        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            if (determineEvent.ObjectType == "Cursor")
            {
                displayText = true;

                if ((determineEvent as Cursor).MouseClicked == true && active == false
                    && (parent == null || parent.active == true)
                    && myPlayer.skillPoints < myPlayer.maxPoints)
                {
                    active = true;
                    if (skillToSet == ActionSkills.Wings)
                    {
                        myPlayer.SkillWings = true;
                    }
                    else if (skillToSet == ActionSkills.Speed)
                    {
                        myPlayer.speed = .8f;
                        myPlayer.speedLimit = 9.0f;
                    }
                    else if (skillToSet == ActionSkills.JumpPlus || skillToSet == ActionSkills.JumpTriple)
                    {
                        myPlayer.MaximumJumps += 1;
                    }
                    else if (skillToSet == ActionSkills.JumpHeight )
                    {
                        myPlayer.jumpHeight = 13.0f;
                    }
                    else if (skillToSet == ActionSkills.FastFall)
                    {
                        myPlayer.strengthOfGravity = .5f;
                    }
                    else if (skillToSet == ActionSkills.AirMelee)
                    {
                        myPlayer.airMelee = true;
                    }
                    else if (skillToSet == ActionSkills.AirRanged)
                    {
                        myPlayer.airRanged = true;
                    }
                    else if (skillToSet == ActionSkills.Dash)
                    {
                        myPlayer.SkillDash = true;
                    }
                    else
                        myPlayer.availableSkills.Add(skillToSet);

                    myPlayer.skillPoints++;

                }


            }
          
        }

        public override void C_NoCollisions()
        {
            displayText = false;
        }

        public void ResetPlayer()
        {
            myPlayer.availableSkills.Clear();
            myPlayer.SkillDash = false;
            myPlayer.airRanged = false;
            myPlayer.airMelee = false;
            myPlayer.strengthOfGravity = .3f;
            myPlayer.jumpHeight = 11.5f;
            myPlayer.MaximumJumps = 1;
            myPlayer.speed = .5f;
            myPlayer.speedLimit = 7.5f;
            myPlayer.SkillWings = false;
            myPlayer.skillPoints = 0;

            
        }

        public void ShowActive()
        {
            if (myPlayer.airMelee == true && skillToSet == ActionSkills.AirMelee)
            {
                active = true;
            }
            if (myPlayer.airRanged == true && skillToSet == ActionSkills.AirRanged)
            {
                active = true;
            }
            if (myPlayer.MaximumJumps >= 2 && skillToSet == ActionSkills.JumpPlus)
            {
                active = true;
            }
            if (myPlayer.MaximumJumps >= 3 && skillToSet == ActionSkills.JumpTriple)
            {
                active = true;
            }
            if (myPlayer.jumpHeight > 11.5f && skillToSet == ActionSkills.JumpHeight)
            {
                active = true;
            }
            if (myPlayer.strengthOfGravity > .31f && skillToSet == ActionSkills.FastFall)
            {
                active = true;
            }
            if (myPlayer.SkillConfuseRay == true && skillToSet == ActionSkills.ConfuseRay)
            {
                active = true;
            }
            if (myPlayer.SkillDash == true && skillToSet == ActionSkills.Dash)
            {
                active = true;
            }
            if (myPlayer.speedLimit > 7.55f && skillToSet == ActionSkills.Speed)
            {
                active = true;
            }
            if (myPlayer.SkillExile == true && skillToSet == ActionSkills.Exile)
            {
                active = true;
            }
            if (myPlayer.SkillShadowPush == true && skillToSet == ActionSkills.ShadowPush)
            {
                active = true;
            }
            if (myPlayer.SkillShadowHold == true && skillToSet == ActionSkills.ShadowHold)
            {
                active = true;
            }
            if (myPlayer.SkillGhost == true && skillToSet == ActionSkills.Ghost)
            {
                active = true;
            }
            if (myPlayer.SkillShadowDagger == true && skillToSet == ActionSkills.ShadowDagger)
            {
                active = true;
            }
            if (myPlayer.SkillChaoticReset == true && skillToSet == ActionSkills.ChaoticReset)
            {
                active = true;
            }
        }

        
        

    }
}
