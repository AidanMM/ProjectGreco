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
using ProjectGreco.GameObjects.Buttons;

namespace ProjectGreco.GameObjects
{
    public class SkillTree
    {
        List<SkillButton> leftTree;
        List<SkillButton> rightTree;
        Player toHold;
        BaseState currentLevel;
        public SkillTree(Vector2 startPosition, Player toPass, BaseState toAddTo)
        {
            leftTree = new List<SkillButton>();
            rightTree = new List<SkillButton>();
            toHold = toPass;
            currentLevel = toAddTo;

            AddSkillButton(ActionSkills.JumpPlus, false, startPosition);
            leftTree[0].abiltyText = "Plus one jump!";
            AddSkillButton(ActionSkills.JumpHeight, false, new Vector2(startPosition.X, startPosition.Y - 55));
            leftTree[1].parent = leftTree[0];
            leftTree[1].abiltyText = "Extra jump height!";
            AddSkillButton(ActionSkills.AirRanged, false, new Vector2(startPosition.X, startPosition.Y - 110));
            leftTree[2].parent = leftTree[1];
            leftTree[2].abiltyText = "Ranged attack while\njumping!";
            AddSkillButton(ActionSkills.LightWall, false, new Vector2(startPosition.X - 75, startPosition.Y - 165));
            leftTree[3].parent = leftTree[2];
            leftTree[3].abiltyText = "\nGain the ability to\ncreate walls!";
            AddSkillButton(ActionSkills.LightJump, false, new Vector2(startPosition.X - 75, startPosition.Y - 220));
            leftTree[4].parent = leftTree[3];
            leftTree[4].abiltyText = "Gain the ability to\ncreate a platform\nmade of light!";
            AddSkillButton(ActionSkills.Wings, false, new Vector2(startPosition.X - 75, startPosition.Y - 275));
            leftTree[5].parent = leftTree[4];
            leftTree[5].abiltyText = "Gain the ability to\nglide through the air!";
            AddSkillButton(ActionSkills.FastFall, false, new Vector2(startPosition.X + 75, startPosition.Y - 165));
            leftTree[6].parent = leftTree[2];
            leftTree[6].abiltyText = "           fall faster \n           through the air!";
            AddSkillButton(ActionSkills.JumpTriple, false, new Vector2(startPosition.X + 75, startPosition.Y - 220));
            leftTree[7].parent = leftTree[6];
            leftTree[7].abiltyText = "\n           Extra jump!";
            AddSkillButton(ActionSkills.ConfuseRay, false, new Vector2(startPosition.X + 75, startPosition.Y - 275));
            leftTree[8].parent = leftTree[7];
            leftTree[8].abiltyText = "          Gain the ability \n          to confuse your\n          targets!";
            AddSkillButton(ActionSkills.Dash, true, new Vector2(startPosition.X + 250, startPosition.Y));
            rightTree[0].abiltyText = "                                           Dash with the shift button!";
            AddSkillButton(ActionSkills.Speed, true, new Vector2(startPosition.X + 250, startPosition.Y - 55));
            rightTree[1].parent = rightTree[0];
            rightTree[1].abiltyText = "                                           Run faster!";
            AddSkillButton(ActionSkills.AirMelee, true, new Vector2(startPosition.X + 250, startPosition.Y - 110));
            rightTree[2].parent = rightTree[1];
            rightTree[2].abiltyText = "                                           Gain the ability to melee in the air!";
            AddSkillButton(ActionSkills.Exile, true, new Vector2(startPosition.X - 75 + 250, startPosition.Y - 165));
            rightTree[3].parent = rightTree[2];
            rightTree[3].abiltyText = "Gain the ability to Exile enemies for a\nbrief period of time!";
            AddSkillButton(ActionSkills.ShadowPush, true, new Vector2(startPosition.X - 75 + 250, startPosition.Y - 220));
            rightTree[4].parent = rightTree[3];
            rightTree[4].abiltyText = "Gain the ability to push enemies back!";
            AddSkillButton(ActionSkills.ShadowHold, true, new Vector2(startPosition.X - 75 + 250, startPosition.Y - 275));
            rightTree[5].parent = rightTree[4];
            rightTree[5].abiltyText = "Gain the ability to use shadows to \nhold enemies still";
            AddSkillButton(ActionSkills.Ghost, true, new Vector2(startPosition.X + 75 + 250, startPosition.Y - 165));
            rightTree[6].parent = rightTree[2];
            rightTree[6].abiltyText = "                                           Gain the ability to hide in the shadows\n"+
                                      "                                           and become invulnerable!";
            AddSkillButton(ActionSkills.ShadowDagger, true, new Vector2(startPosition.X + 75 + 250, startPosition.Y - 220));
            rightTree[7].parent = rightTree[6];
            rightTree[7].abiltyText = "                                           Gain the ability to throw a dagger and\n"+
                                      "                                           telaport to an enemy!";
            AddSkillButton(ActionSkills.ChaoticReset, true, new Vector2(startPosition.X + 75 + 250, startPosition.Y - 275));
            rightTree[8].parent = rightTree[7];
            rightTree[8].abiltyText = "                                           Gain the ability to Re roll the stage,\n"+
                                      "                                           One time use!";

            
            new RespecButton(new Vector2(startPosition.X + 80, startPosition.Y), this, currentLevel);

            ShowActive();

        }

        /// <summary>
        /// Create a skill button, with the given skill
        /// </summary>
        /// <param name="toCreate">The skill to give</param>
        /// <param name="RightTree">Whether or not to place this skill in the Right tree</param>
        public void AddSkillButton(ActionSkills toCreate, bool RightTree, Vector2 pos)
        {
            if (RightTree == false)
            {
                leftTree.Add(new SkillButton(pos, toHold, toCreate, currentLevel)); 
            }
            else
            {
                rightTree.Add(new SkillButton(pos, toHold, toCreate, currentLevel)); 
            }
        }

        public void ShowActive()
        {
            if (toHold.airMelee == true)
            {
                rightTree[2].active = true;
            }
            if (toHold.airRanged == true)
            {
                leftTree[2].active = true;
            }
            if (toHold.MaximumJumps >= 2)
            {
                leftTree[0].active = true;
            }
            if (toHold.MaximumJumps >= 3)
            {
                leftTree[7].active = true;
            }
            if (toHold.jumpHeight > 11.5f)
            {
                leftTree[1].active = true;
            }
            if (toHold.strengthOfGravity > .31f)
            {
                leftTree[6].active = true;
            }
            if (toHold.SkillConfuseRay == true)
            {
                leftTree[8].active = true;
            }
            if (toHold.SkillDash == true)
            {
                rightTree[0].active = true;
            }
            if (toHold.speedLimit > 7.55f)
            {
                rightTree[1].active = true;
            }
            if (toHold.SkillExile == true)
            {
                rightTree[3].active = true;
            }
            if (toHold.SkillShadowPush == true)
            {
                rightTree[4].active = true;
            }
            if (toHold.SkillShadowHold == true)
            {
                rightTree[5].active = true;
            }
            if (toHold.SkillGhost == true)
            {
                rightTree[6].active = true;
            }
            if (toHold.SkillShadowDagger == true)
            {
                rightTree[7].active = true;
            }
            if (toHold.SkillChaoticReset == true)
            {
                rightTree[8].active = true;
            }

        }

        public void ResetPlayer()
        {
            toHold.availableSkills.Clear();
            toHold.SkillDash = false;
            toHold.airRanged = false;
            toHold.airMelee = false;
            toHold.strengthOfGravity = .3f;
            toHold.jumpHeight = 11.5f;
            toHold.MaximumJumps = 1;
            toHold.speed = .5f;
            toHold.speedLimit = 7.5f;
            toHold.SkillWings = false;
            toHold.skillPoints = 0;

            for (int i = 0; i < leftTree.Count; i++)
            {
                leftTree[i].active = false;
                rightTree[i].active = false;
            }


        }
    }
}
