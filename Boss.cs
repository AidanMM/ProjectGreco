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

        /// <summary>
        /// Stores projectiles that have already hit the boss to prevent multiple hits from one arrow.
        /// </summary>
        private List<int> projectiles = new List<int>();

        //
        // Constructor
        //

        /// <summary>
        /// Creates a boss and it's two minion arms.
        /// </summary>
        public Boss(List<List<Texture2D>> animationList, Vector2 pos, BossWeapon leftHand, BossWeapon rightHand)
            : base(animationList, pos, "Boss")
        {
            this.leftHand = leftHand;
            this.rightHand = rightHand;

            zOrder = -10;
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
