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
            AddSkillButton(ActionSkills.JumpHeight, false, new Vector2(startPosition.X, startPosition.Y - 55));
            leftTree[1].parent = leftTree[0];
            AddSkillButton(ActionSkills.AirRanged, false, new Vector2(startPosition.X, startPosition.Y - 110));
            leftTree[2].parent = leftTree[1];
            AddSkillButton(ActionSkills.LightWall, false, new Vector2(startPosition.X - 75, startPosition.Y - 165));
            leftTree[3].parent = leftTree[2];
            AddSkillButton(ActionSkills.LightJump, false, new Vector2(startPosition.X - 75, startPosition.Y - 220));
            leftTree[4].parent = leftTree[3];
            AddSkillButton(ActionSkills.Wings, false, new Vector2(startPosition.X - 75, startPosition.Y - 275));
            leftTree[5].parent = leftTree[4];
            AddSkillButton(ActionSkills.FastFall, false, new Vector2(startPosition.X + 75, startPosition.Y - 165));
            leftTree[6].parent = leftTree[2];
            AddSkillButton(ActionSkills.JumpTriple, false, new Vector2(startPosition.X + 75, startPosition.Y - 220));
            leftTree[7].parent = leftTree[6];
            AddSkillButton(ActionSkills.ConfuseRay, false, new Vector2(startPosition.X + 75, startPosition.Y - 275));
            leftTree[8].parent = leftTree[7];

            AddSkillButton(ActionSkills.Dash, true, new Vector2(startPosition.X + 250, startPosition.Y));
            AddSkillButton(ActionSkills.Speed, true, new Vector2(startPosition.X + 250, startPosition.Y - 55));
            rightTree[1].parent = rightTree[0];
            AddSkillButton(ActionSkills.AirMelee, true, new Vector2(startPosition.X + 250, startPosition.Y - 110));
            rightTree[2].parent = rightTree[1];
            AddSkillButton(ActionSkills.Exile, true, new Vector2(startPosition.X - 75 + 250, startPosition.Y - 165));
            rightTree[3].parent = rightTree[2];
            AddSkillButton(ActionSkills.ShadowPush, true, new Vector2(startPosition.X - 75 + 250, startPosition.Y - 220));
            rightTree[4].parent = rightTree[3];
            AddSkillButton(ActionSkills.ShadowHold, true, new Vector2(startPosition.X - 75 + 250, startPosition.Y - 275));
            rightTree[5].parent = rightTree[4];
            AddSkillButton(ActionSkills.Ghost, true, new Vector2(startPosition.X + 75 + 250, startPosition.Y - 165));
            rightTree[6].parent = rightTree[2];
            AddSkillButton(ActionSkills.ShadowDagger, true, new Vector2(startPosition.X + 75 + 250, startPosition.Y - 220));
            rightTree[7].parent = rightTree[6];
            AddSkillButton(ActionSkills.ChaoticReset, true, new Vector2(startPosition.X + 75 + 250, startPosition.Y - 275));
            rightTree[8].parent = rightTree[7];

            
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
            if (toHold.jumpHeight > 10.5f)
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
            toHold.jumpHeight = 10.5f;
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
