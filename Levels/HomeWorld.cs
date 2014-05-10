using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
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
            AddObjectToHandler("Button", new Button(new Vector2(200, 200), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "clickable test", true));
            AddObjectToHandler("NoButton", new Button(new Vector2(200, 300), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "unclickable test", false));
            // Don't spawn the portal if the level is complete.

            AddObjectToHandler("PortalHills", new LevelPortal(new Vector2(1500, 400), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), PlayerStats.hill, (LevelObjectDictionary["Player"] as Player)));

            AddObjectToHandler("PortalIce", new LevelPortal(new Vector2(1700, 400), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), PlayerStats.snow, (LevelObjectDictionary["Player"] as Player)));

            AddObjectToHandler("PortalDesert", new LevelPortal(new Vector2(1900, 400), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), PlayerStats.desert, (LevelObjectDictionary["Player"] as Player)));

            AddObjectToHandler("PortalForest", new LevelPortal(new Vector2(2100, 400), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), PlayerStats.forest, (LevelObjectDictionary["Player"] as Player)));

            BossWeapon leftHand = new BossWeapon(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["BossLeftHand"]), new Vector2(800, 0));
            BossWeapon rightHand = new BossWeapon(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["BossRightHand"]), new Vector2(1650, 0));

            Boss myBoss = new Boss(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["BossMain"]), new Vector2(1100, -100), leftHand, rightHand);

            AddObjectToHandler("Boss", myBoss);
            AddObjectToHandler("LeftHand", leftHand);
            AddObjectToHandler("RightHand", rightHand);

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

           
            skillTree = new SkillTree(new Vector2(800, 400), LevelObjectDictionary["Player"] as Player, this);


            MediaPlayer.Volume = .5f;
            MediaPlayer.Play(Game1.SONG_LIBRARY["HomeWorldMusic"]);  


			SortByZorder();
        }
    }
}
