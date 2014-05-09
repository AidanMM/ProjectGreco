using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects
{

    class BaseEnemy : GameObject
    {

        public float speed = .5f;
        public float speedLimit = 7.5f;
        public bool applyGravity = true;
        public int jumpCounter = 0;
        public Random rand;
        public int rotation = 0;

        public int counter = 0;
        public int ghostCounter = 0;
        public int ghostDashTimer = 0;

        public bool chasing = false;
        public bool overshoot = false;
        public int chaseDistance = 64 * 8; // The 64 is for the width of a block, enemies should start pursuing at a distance of about seven blocks for now.
        public int overshootBeginDistance = 32;
        public int overshootEndDistance = 64;
        private float baseSpeed = 1.0f;
        public float speedModifier = 1.0f;
        public EnemySize mySize;

        public Player myPlayer;
        public List<int> projectiles;
        public bool destroy = false;

        /// <summary>
        /// This bool will control whether or not this enemy is confused.  When it is confused its speed modifier becomes -.5
        /// </summary>
        protected bool confused = false;

        /// <summary>
        /// This bool will tell whether or not this object can be exiled.  If it can be, then the exile spell will work on it.
        /// </summary>
        protected bool exileable = true;

        /// <summary>
        /// The status of whether or not the object is currently in the exiled state
        /// </summary>
        protected bool exiled = false;

        /// <summary>
        /// The status of whether or not the object is currently in the held state
        /// </summary>
        protected bool held = false;

        /// <summary>
        /// The position for this enemy to return to after it has been exiled
        /// </summary>
        protected Vector2 exileReturnPosition;

        /// <summary>
        /// The ammount of time until the current effect should end
        /// </summary>
        protected int endTime;

        /// <summary>
        /// The timer for the enemy to use to time things.
        /// </summary>
        protected int timer = 0;




        private const float FLYING_VELOCITY_MAX = .8f;





        /// <summary>
        /// The array of vertices used to draw the health bar primitive for the enemy
        /// </summary>
        VertexPositionColor[] vertices;

        private int maxHealth = 10;

        /// <summary>
        /// The Max Health field for enemies
        /// </summary>
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        private int currentHealth;

        /// <summary>
        /// The current health field of the enemy.
        /// </summary>
        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        private EnemyType ai;

        /// <summary>
        /// The current AI the enemy is following.
        /// </summary>
        public EnemyType AI
        {
            get { return ai; }
        }

        /// <summary>
        /// Basic paramaterized constructor for the base enemy object. Does not account for health, use the health paramaterzied constructor for that
        /// </summary>
        /// <param name="animationList">The animations for the enemy</param>
        /// <param name="pos">The position to spawn the enemy at</param>
        public BaseEnemy(List<List<Texture2D>> animationList, Vector2 pos, EnemyType ai, Random rand, Player myPlayer, EnemySize size)
            : base(animationList, pos, "enemy")
        {
            this.rand = rand;

            zOrder = 8999;

            //Create the vertices and their color
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(pos.X, pos.Y, 0);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(pos.X, pos.Y, 0);
            vertices[1].Color = Color.Green;
            this.ai = ai;
            this.myPlayer = myPlayer;
            this.mySize = size;

            this.projectiles = new List<int>();

            CheckForCollisions = true;

            currentHealth = maxHealth;

            if (ai == EnemyType.Flying)
            {
                overshootEndDistance = 16;
            }

            if (mySize == EnemySize.Small)
            {
                maxHealth = 1;
                currentHealth = 1;
                baseSpeed = 2.0f;
            }

            if (mySize == EnemySize.Medium)
            {
                maxHealth = 3;
                currentHealth = 3;
                baseSpeed = 1.0f;
            }

            if (mySize == EnemySize.Large)
            {
                maxHealth = 10;
                currentHealth = 10;
                baseSpeed = 0.5f;
            }

        }

        /// <summary>
        /// Base Enemy update also updates the vertices of the primitives
        /// </summary>
        public override void Update()
        {

            if (exiled == false && held == false)
            {
                vertices[0].Position = new Vector3(position.X - 200, position.Y - 100, 0);

                vertices[1].Position = new Vector3(collisionBox.X + (float)(this.collisionBox.Width * (float)((float)currentHealth / (float)maxHealth)), vertices[0].Position.Y, 0);

                // Needed to remove the velocity and acceleration parts for the enemies to function correctly
                #region Base Update
                OldPosition = new Vector2(position.X, position.Y);

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
                #endregion
                // vertices[0].Position = new Vector3(position.X - (int)Game1.CAMERA_DISPLACEMENT.X - 400, collisionBox.Y - collisionBox.Height / 10 - (int)Game1.CAMERA_DISPLACEMENT.Y, 0);

                if (Game1.KBState.IsKeyDown(Keys.D1) && Game1.oldKBstate.IsKeyUp(Keys.D1) && Game1.KBState.IsKeyDown(Keys.LeftAlt))
                {
                    currentHealth--;
                }

                if (destroyThis == true)
                {
                    Destroy();
                }

                #region Artificial Intelligence Update
                if (onScreen)
                {
                    double distanceToPlayer = Math.Pow(
                        Math.Pow(position.X - myPlayer.Position.X, 2) +
                        Math.Pow(position.Y - myPlayer.Position.Y, 2),
                        .5);

                    if (distanceToPlayer > Math.Abs(velocity.X) * overshootEndDistance)
                    {
                        overshoot = false;
                    }

                    if (distanceToPlayer < overshootBeginDistance && ai == EnemyType.Ghost)
                    {
                        overshoot = true;
                    }
                    if ((Math.Abs(position.X - myPlayer.Position.X)) < overshootBeginDistance && ai == EnemyType.Flying)
                    {
                        overshoot = true;
                    }
                    if (distanceToPlayer < chaseDistance)
                    {
                        chasing = true;
                    }
                    else
                    {
                        chasing = false;
                        ghostDashTimer = 0;
                    }

                    switch (ai)
                    {
                        #region Ghost
                        case EnemyType.Ghost:

                            // Overshoot will make it so the enemy goes past the player instead of hovering ontop of them.
                            if (!overshoot)
                            {

                                if (!chasing)
                                {
                                    // Let's make the ghost hover around

                                    if (ghostCounter <= 60)
                                    {
                                        velocity.X = -0.4f;
                                    }
                                    else if (ghostCounter <= 120)
                                    {
                                        velocity.X = 0.4f;
                                    }
                                    if (ghostCounter <= 30 || ghostCounter > 90)
                                    {
                                        velocity.Y = -0.4f;
                                    }
                                    else if (ghostCounter > 30 || ghostCounter <= 90)
                                    {
                                        velocity.Y = 0.4f;
                                    }

                                }
                                if (chasing)
                                {
                                    float totalVelocity = 6.2f * baseSpeed * speedModifier;
                                    float xPercent = (myPlayer.Position.X - position.X) / (float)distanceToPlayer;
                                    float yPercent = (myPlayer.Position.Y - position.Y) / (float)distanceToPlayer;

                                    velocity.X = totalVelocity * xPercent;
                                    velocity.Y = totalVelocity * yPercent;

                                    if (ghostCounter == 0 || ghostCounter == 30 || ghostCounter == 60 || ghostCounter == 90)
                                    {
                                        int guess = rand.Next(0, 10);

                                        if (guess == 0)
                                        {
                                            ghostDashTimer = 60;
                                        }

                                    }

                                    if (ghostDashTimer > 0)
                                    {
                                        velocity.X = totalVelocity * xPercent * 1.5f;
                                        velocity.Y = totalVelocity * yPercent * 1.5f;
                                        ghostDashTimer--;
                                    }

                                }


                                if (velocity.X >= 12.0f * baseSpeed * speedModifier)
                                {
                                    velocity.X = 12.0f * baseSpeed * speedModifier;
                                }
                                if (velocity.X <= -12.0f * baseSpeed * speedModifier)
                                {
                                    velocity.X = 12.0f * baseSpeed * speedModifier;
                                }
                                if (velocity.Y >= 12.0f * baseSpeed * speedModifier)
                                {
                                    velocity.Y = 12.0f * baseSpeed * speedModifier;
                                }
                                if (velocity.Y <= -12.0f * baseSpeed * speedModifier)
                                {
                                    velocity.Y = 12.0f * baseSpeed * speedModifier;
                                }

                            }
                            break;

                        #endregion
                        #region Ground
                        case EnemyType.Ground:

                            // Ground enemies should occasionally hop to mix up movement a bit.
                            if (counter >= 10)
                            {
                                int roll = rand.Next(0, 20);
                                if (roll == 0 && jumpCounter == 0)
                                {
                                    velocity.Y = -20.5f;
                                    jumpCounter++;
                                }
                            }

                            if (applyGravity == true)
                            {
                                acceleration.Y = 0.6f;
                            }
                            else
                            {
                                acceleration.Y = 0.0f;
                            }
                            if (objectBelow == false)
                            {
                                applyGravity = true;

                            }
                            if (position.Y > LevelVariables.HEIGHT * 64)
                            {
                                position = new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64);
                            }

                            if (chasing)
                            {
                                // Go towards the player
                                if (myPlayer.Position.X > position.X)
                                {
                                    acceleration.X = 0.1f * baseSpeed * speedModifier;
                                }
                                // In either direction
                                if (myPlayer.Position.X < position.X)
                                {
                                    acceleration.X = -0.1f * baseSpeed * speedModifier;
                                }

                                // Hop if the player is higher
                                if (myPlayer.Position.Y + collisionBox.Height < position.Y && jumpCounter == 0)
                                {
                                    velocity.Y = -10.5f;
                                    jumpCounter++;
                                }

                                // If your velocity is zero do a cute hop, more importantly can navigate uneven terrain
                                if (velocity.X == 0 && jumpCounter == 0)
                                {
                                    velocity.Y = -10.5f;
                                    jumpCounter++;
                                }


                            }
                            break;
                        #endregion
                        #region Flying
                        case EnemyType.Flying:

                            if (applyGravity == true)
                            {
                                acceleration.Y = 0.6f;
                            }
                            else
                            {
                                acceleration.Y = 0.0f;
                            }
                            if (objectBelow == false)
                            {
                                applyGravity = true;

                            }
                            if (!chasing)
                            {
                                // Have the enemy flap semi-randomly.
                                if (counter >= 10)
                                {
                                    int roll = rand.Next(0, 9);
                                    if (roll > 2)
                                    {
                                        velocity.Y = -2.7f;
                                    }
                                }
                            }
                            // Catch the enemy if it falls out of the level.
                            if (position.Y > LevelVariables.HEIGHT * 64)
                            {
                                Destroy();
                            }
                            // Since this guy flys let him have a glide.
                            if (velocity.Y > FLYING_VELOCITY_MAX)
                            {
                                velocity.Y = FLYING_VELOCITY_MAX;
                            }

                            if (chasing)
                            {
                                if (!overshoot)
                                {
                                    if (myPlayer.Position.X > position.X)
                                    {
                                        acceleration.X = 0.3f * baseSpeed * speedModifier;
                                    }
                                    if (myPlayer.Position.X < position.X)
                                    {
                                        acceleration.X = -0.3f * baseSpeed * speedModifier;
                                    }
                                }

                                if (counter >= 10 & myPlayer.Position.Y < position.Y)
                                {
                                    int roll = rand.Next(0, 9);
                                    if (roll > 2)
                                    {
                                        velocity.Y = -7.7f;
                                    }
                                }
                            }
                            if (!chasing)
                            {
                                velocity.X = 0;
                            }
                            if (velocity.X > 9.0f * baseSpeed * speedModifier)
                            {
                                velocity.X = 9.0f * baseSpeed * speedModifier;
                            }
                            if (velocity.X < -9.0f * baseSpeed * speedModifier)
                            {
                                velocity.X = -9.0f * baseSpeed * speedModifier;
                            }
                            break;
                        #endregion
                    }
                    position += velocity;
                    velocity += acceleration;
                    UpdateCollisionBox();
                    if (counter >= 10)
                    {
                        counter = 0;
                    }
                    if (ghostCounter >= 120)
                    {
                        ghostCounter = 0;
                    }
                    counter++;
                    ghostCounter++;
                }


                #endregion


                if (destroy)
                {
                    if (mySize == EnemySize.Large)
                        PlayerStats.mula += 4;
                    if (mySize == EnemySize.Medium)
                        PlayerStats.mula += 2;
                    if (mySize == EnemySize.Small)
                        PlayerStats.mula += 1;

                    Destroy();
                }
            }
            else
            {
                timer++;
                if (timer >= endTime)
                {
                    if(exiled == true)
                        ReturnFromExile();
                    if (held == true)
                    {
                        held = false;
                    }
                }
            }
            if (confused == true)
            {
                speedModifier = -.5f;
                timer++;
                if (timer >= endTime)
                {
                    speedModifier = 1.0f;
                    timer = 0;
                    confused = false;
                }
            }

            //Check to see if an object is on screen.
            OnScreenCheck();
            

        }

        /// <summary>
        /// Includes the drawing of primitives
        /// </summary>
        /// <param name="spriteBatch">spritebatch for which to use to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //vertices[0].Position = new Vector3(vertices[0].Position.X - Game1.CAMERA_DISPLACEMENT.X, vertices[0].Position.Y - Game1.CAMERA_DISPLACEMENT.Y, vertices[0].Position.Z);

            //vertices[1].Position = new Vector3(vertices[1].Position.X - Game1.CAMERA_DISPLACEMENT.X, vertices[1].Position.Y - Game1.CAMERA_DISPLACEMENT.Y, vertices[1].Position.Z);

            Rectangle drawRec = new Rectangle(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X,
                collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, collisionBox.Width, collisionBox.Height);

            rotation += (int)(acceleration.X*10*Math.Abs(velocity.X));
            rotation = rotation % 360;

            // Deal with onion rotations
            if (ai == EnemyType.Ground)
            {
                if (held == false)
                {
                    spriteBatch.Draw(animationList[animationListIndex][frameIndex], new Rectangle(
                        drawRec.X + collisionBox.Width / 2,
                        drawRec.Y + collisionBox.Height / 2,
                        drawRec.Width,
                        drawRec.Height)
                        , null, Color.White, (float)(rotation * Math.PI / 180),
                        new Vector2(collisionBox.Width / 2, collisionBox.Height / 2), SpriteEffects.None, 0.0f);
                }
                else
                {
                    spriteBatch.Draw(animationList[animationListIndex][frameIndex], new Rectangle(
                        drawRec.X + collisionBox.Width / 2,
                        drawRec.Y + collisionBox.Height / 2,
                        drawRec.Width,
                        drawRec.Height)
                        , null, Color.Black, (float)(rotation * Math.PI / 180),
                        new Vector2(collisionBox.Width / 2, collisionBox.Height / 2), SpriteEffects.None, 0.0f);
                }
            }
                
            else
            {
                if(held == false)
                    spriteBatch.Draw(animationList[animationListIndex][frameIndex], drawRec, Color.White);
                else
                    spriteBatch.Draw(animationList[animationListIndex][frameIndex], drawRec, Color.Black);
            }
            

            vertices[0].Position.X = drawRec.X;
            vertices[0].Position.Y = drawRec.Y;


            vertices[1].Position.X = drawRec.X + 100;
            vertices[1].Position.Y = drawRec.Y;


            //Game1.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
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
                    currentHealth--;
                    if (!myArrow.piercing)
                        myArrow.DestroyThis = true;
                    if (currentHealth <= 0)
                    {
                        destroy = true;
                    }
                }
            }

            if (determineEvent.ObjectType == "Sword")
            {
                currentHealth--;
                if (currentHealth <= 0)
                {
                    destroy = true;
                }
            }


            if (determineEvent.ObjectType == "EdgeTile" && ai != EnemyType.Ghost)
            {
                OldPosition = new Vector2(OldPosition.X, OldPosition.Y - velocity.Y);

                if (Math.Floor(OldPosition.X) <= determineEvent.Position.X
                    && ((OldPosition.Y + Height >= determineEvent.Position.Y
                    && OldPosition.Y + Height <= determineEvent.Position.Y + determineEvent.Height)
                    || (OldPosition.Y < determineEvent.Position.Y + determineEvent.Height
                    && OldPosition.Y >= determineEvent.Position.Y)))
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X - Width;
                    acceleration.X = 0;
                }
                else if (OldPosition.X >= determineEvent.Position.X + determineEvent.Width
                    && ((OldPosition.Y + Height >= determineEvent.Position.Y
                    && OldPosition.Y + Height <= determineEvent.Position.Y + determineEvent.Height)
                    || (OldPosition.Y <= determineEvent.Position.Y + determineEvent.Height
                    && OldPosition.Y >= determineEvent.Position.Y)))
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X + determineEvent.Width;
                    acceleration.X = 0;
                }
                else if (Math.Floor(OldPosition.Y + Height) <= determineEvent.Position.Y
                    && ((OldPosition.X + Width - 1 > determineEvent.Position.X
                    && OldPosition.X + Width <= determineEvent.Position.X + determineEvent.Width)
                    || (OldPosition.X < determineEvent.Position.X + determineEvent.Width
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
        }

        public override void C_NoCollisions()
        {

        }

        /// <summary>
        /// This function will exile this enemy, removing it from its current position, and then returning it back after some ammount of time.
        /// </summary>
        public void Exile(int timeTillReturn)
        {
            if (exileable == true)
            {
                exileReturnPosition = position;
                position = new Vector2(-1000, -1000);
                exiled = true;
                UpdateCollisionBox();
                endTime = timeTillReturn;
            }
        }

        /// <summary>
        /// This function will return an object which is exiled from exile.
        /// </summary>
        public void ReturnFromExile()
        {
            position = new Vector2(exileReturnPosition.X, exileReturnPosition.Y);
            exiled = false;
            timer = 0;
        }

        /// <summary>
        /// Puts an object into a stationary held state, also zeroes out their velocity, and acceleration.
        /// </summary>
        /// <param name="lengthOfHold"></param>
        public void ShadowHold(int lengthOfHold)
        {
            endTime = lengthOfHold;
            held = true;
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            timer = 0;
        }

        public void Confuse(int lengthOfConfusion)
        {
            endTime = lengthOfConfusion;
            timer = 0;
            confused = true;
        }
    }
}
