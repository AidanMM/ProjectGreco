﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;
using ProjectGreco.GameObjects.Buttons;

namespace ProjectGreco.Levels
{
    public class HomeWorld : BaseState
    {
        public SkillTree skillTree;
        public HomeWorld(Player myPlayer = null)
        {
            levelType = LevelName.Home;

            if (PlayerStats.firstTime)
            {
                PlayerStats.firstTime = false;
                myPlayer = new Player(new Vector2(0, 0), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["PlayerAnimation"]));
                PlayerStats.hill = new Level(LevelName.Hills, myPlayer, true);
                PlayerStats.snow = new Level(LevelName.Ice, myPlayer, true);
                PlayerStats.desert = new Level(LevelName.Desert, myPlayer, true);
                PlayerStats.forest = new Level(LevelName.Forest, myPlayer, true);
                
            }
            myPlayer.Position = new Vector2(0, 0);

            AddObjectToHandler("Player", myPlayer);
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, 0), Game1.IMAGE_DICTIONARY["cursor"]));
            AddObjectToHandler("Ground", new GameObject(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["homeWorld"]), new Vector2(0, 500), "EdgeTile"));
            AddObjectToHandler("Grass", new GameObject(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Grass"]), new Vector2(0, 485), "Grass"));
            LevelObjectDictionary["Grass"].FramesPerSecond = 2;
            LevelObjectDictionary["Grass"].A_BeginAnimation();
            LevelObjectDictionary["Grass"].ZOrder = 3;
            // Don't spawn the portal if the level is complete.

            AddObjectToHandler("PortalHills", new LevelPortal(new Vector2(1500, 366), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Portal"]), PlayerStats.hill, (LevelObjectDictionary["Player"] as Player)));

            AddObjectToHandler("PortalIce", new LevelPortal(new Vector2(1700, 366), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Portal"]), PlayerStats.snow, (LevelObjectDictionary["Player"] as Player)));

            AddObjectToHandler("PortalDesert", new LevelPortal(new Vector2(1900, 366), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Portal"]), PlayerStats.desert, (LevelObjectDictionary["Player"] as Player)));

            AddObjectToHandler("PortalForest", new LevelPortal(new Vector2(2100, 366), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Portal"]), PlayerStats.forest, (LevelObjectDictionary["Player"] as Player)));

            //Close the portals if they player has completed them
            if (PlayerStats.hillComplete)
            {
                (LevelObjectDictionary["PortalHills"] as LevelPortal).Closed = true;
            }
            if (PlayerStats.snowComplete)
            {
                (LevelObjectDictionary["PortalIce"] as LevelPortal).Closed = true;
            }
            if (PlayerStats.desertComplete)
            {
                (LevelObjectDictionary["PortalDesert"] as LevelPortal).Closed = true;
            }
            if (PlayerStats.forestComplete)
            {
                (LevelObjectDictionary["PortalForest"] as LevelPortal).Closed = true;
            }

            if (PlayerStats.hillComplete && PlayerStats.snowComplete && PlayerStats.desertComplete && PlayerStats.forestComplete)
            {
                Boss myBoss = new Boss(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["BossCenter"]), new Vector2(3050, 200), this, myPlayer);

                AddObjectToHandler("Boss", myBoss);
            }

           
            skillTree = new SkillTree(new Vector2(800, 400), LevelObjectDictionary["Player"] as Player, this);
            


			SortByZorder();
        }
    }
}
