﻿using System;
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
using ProjectGreco.Levels;

namespace ProjectGreco
{
    /// <summary>
    /// The boss of the game.
    /// </summary>
    class Boss : GameObject
    {
        /// <summary>
        /// Amount of health the boss has left.
        /// </summary>
        private int health = 200;

        /// <summary>
        /// Amount of health the boss has left.
        /// </summary>
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        /// <summary>
        /// Number of enemies the boss has spawned and are currently alive
        /// </summary>
        private int activeEnemies = 0;

        /// <summary>
        /// Number of enemies the boss has spawned and are currently alive
        /// </summary>
        public int ActiveEnemies
        {
            get { return activeEnemies; }
            set { activeEnemies = value; }
        }

        /// <summary>
        /// The frequency at which the boss spawns enemies to fight
        /// </summary>
        private double spawnFrequency = .125;

        /// <summary>
        /// The frequency at which the boss spawns enemies to fight
        /// </summary>
        public double SpawnFrequency
        {
            get { return spawnFrequency; }
            set { spawnFrequency = value; }
        }

        /// <summary>
        /// The maximum numbers of enemies the boss can spawn at once.
        /// </summary>
        private int maxSpawns = 5;

        /// <summary>
        /// The maximum numbers of enemies the boss can spawn at once.
        /// </summary>
        public int MaxSpawns
        {
            get { return maxSpawns; }
            set { maxSpawns = value; }
        }

        /// <summary>
        /// The Boss's Left Hand
        /// </summary>
        private BossWeapon leftHand;

        /// <summary>
        /// The Boss's Left Hand
        /// </summary>
        public BossWeapon LeftHand
        {
            get { return leftHand; }
            set { leftHand = value; }
        }

        /// <summary>
        /// The Boss's Right Hand
        /// </summary>
        private BossWeapon rightHand;

        /// <summary>
        /// The Boss's Right Hand
        /// </summary>
        public BossWeapon RightHand
        {
            get { return rightHand; }
            set { rightHand = value; }
        }

        public int rotation = 0;

        /// <summary>
        /// Stores projectiles that have already hit the boss to prevent multiple hits from one arrow.
        /// </summary>
        private List<int> projectiles = new List<int>();

        /// <summary>
        /// The level the boss exists in.  Used for spawning.
        /// </summary>
        private BaseState level;

        /// <summary>
        /// The level the boss exists in.  Used for spawning.
        /// </summary>
        public BaseState Level
        {
            get { return level; }
        }

        //
        // Constructor
        //

        /// <summary>
        /// Creates a boss and it's two minion arms.
        /// </summary>
        public Boss(List<List<Texture2D>> animationList, Vector2 pos, BaseState level)
            : base(animationList, pos, "Boss")
        {
            this.leftHand = new BossWeapon(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["BossLeftHand"]), new Vector2(2500, 0));
            this.rightHand = new BossWeapon(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["BossRightHand"]), new Vector2(3350, 0));
            this.level = level;

            level.AddObjectToHandler("LeftHand", leftHand);
            level.AddObjectToHandler("RightHand", rightHand);

            checkForCollisions = true;

            zOrder = -100;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Texture2D mySprite = Game1.IMAGE_DICTIONARY["BossMain"];

            Rectangle drawRec = new Rectangle(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X,
                collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, collisionBox.Width, collisionBox.Height);

            Vector2 texturePosition = new Vector2(collisionBox.X + (collisionBox.Width / 2) - (mySprite.Width / 2), collisionBox.Y + (collisionBox.Height / 2) - (mySprite.Height / 2));

            spriteBatch.Draw(mySprite, new Rectangle((int)texturePosition.X - (int)Game1.CAMERA_DISPLACEMENT.X, (int)texturePosition.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, mySprite.Width, mySprite.Height),
                    null, Color.White, (float)(rotation * Math.PI / 180),
                    new Vector2(mySprite.Width / 2, mySprite.Height / 2), SpriteEffects.None, 0.0f);
        }

        public override void Update()
        {
            // Base update
            UpdateCollisionBox();

            if (animating == true)
            {
                if (Game1.TIMER % (60 / framesPerSecond) == 0)
                    frameIndex++;
                if (frameIndex >= animationList[animationListIndex].Count && looping == true)
                    frameIndex = 0;
                else if (frameIndex >= animationList[animationListIndex].Count && looping == false)
                    A_StopAnimating();

            }

            OnScreenCheck();

            rotation += 1;
            rotation = rotation % 360;

            // Enemy spawning

            if (destroyThis)
            {
                //End the game
            }
        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            // If the enemy gets hit by an arrow
            if (determineEvent.ObjectType == "Arrow")
            {
                // Check out the arrow's id
                Arrow myArrow = determineEvent as Arrow;
                if (!projectiles.Contains(myArrow.id))
                {
                    // Add the projectile to a blacklist so one arrow does not hit an enemy infinity times.
                    projectiles.Add(myArrow.id);
                    health--;
                    if (!myArrow.piercing)
                        myArrow.DestroyThis = true;
                    if (health <= 0)
                    {
                        destroyThis = true;
                    }
                }
            }

            if (determineEvent.ObjectType == "Sword")
            {
                health--;
                if (health <= 0)
                {
                    destroyThis = true;
                }
            }
        }




    }
}