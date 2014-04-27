﻿using System;
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
        public int counter = 0;
        public int ghostCounter = 0;
        public int ghostDashTimer = 0;
        public bool chasing = false;
        public bool overshoot = false;
        public int chaseDistance = 64 * 7; // The 64 is for the width of a block, enemies should start pursuing at a distance of about seven blocks for now.
        public Player myPlayer;

        private const float FLYING_VELOCITY_MAX = .4f;


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
        public BaseEnemy(List<List<Texture2D>> animationList, Vector2 pos, EnemyType ai, Random rand, Player myPlayer)
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


            CheckForCollisions = true;

            currentHealth = maxHealth;


        }

        /// <summary>
        /// Base Enemy update also updates the vertices of the primitives
        /// </summary>
        public override void Update()
        {


            vertices[0].Position = new Vector3(position.X - 200, position.Y - 100, 0);

            vertices[1].Position = new Vector3(collisionBox.X + (float)(this.collisionBox.Width * (float)((float)currentHealth / (float)maxHealth)), vertices[0].Position.Y, 0);
            base.Update();
            // vertices[0].Position = new Vector3(position.X - (int)Game1.CAMERA_DISPLACEMENT.X - 400, collisionBox.Y - collisionBox.Height / 10 - (int)Game1.CAMERA_DISPLACEMENT.Y, 0);

            if (Game1.KBState.IsKeyDown(Keys.D1) && Game1.oldKBstate.IsKeyUp(Keys.D1) && Game1.KBState.IsKeyDown(Keys.LeftAlt))
            {
                currentHealth--;
            }



            #region Artificial Intelligence Update
            if (onScreen)
            {
                double distanceToPlayer = Math.Pow(
                    Math.Pow(position.X - myPlayer.Position.X, 2) +
                    Math.Pow(position.Y - myPlayer.Position.Y, 2),
                    .5);

                if (distanceToPlayer > Math.Abs(velocity.X) *64)
                {
                    overshoot = false;
                }
                if (distanceToPlayer < 32)
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
                                    velocity.X = -0.2f;
                                }
                                else if (ghostCounter <= 120)
                                {
                                    velocity.X = 0.2f;
                                }
                                if (ghostCounter <= 30 || ghostCounter > 90)
                                {
                                    velocity.Y = -0.2f;
                                }
                                else if (ghostCounter > 30 || ghostCounter <= 90)
                                {
                                    velocity.Y = 0.2f;
                                }

                            }
                            if (chasing)
                            {
                                float totalVelocity = 3.1f;
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


                            if (velocity.X >= 6.0f)
                            {
                                velocity.X = 6.0f;
                            }
                            if (velocity.X <= -6.0f)
                            {
                                velocity.X = 6.0f;
                            }
                            if (velocity.Y >= 6.0f)
                            {
                                velocity.Y = 6.0f;
                            }
                            if (velocity.Y <= -6.0f)
                            {
                                velocity.Y = 6.0f;
                            }

                        }




                        break;

                    #endregion
                    case EnemyType.Ground:

                        // Ground enemies should occasionally hop to mix up movement a bit.
                        if (counter >= 10)
                        {
                            int roll = rand.Next(0, 20);
                            if (roll == 0 && jumpCounter == 0)
                            {
                                velocity.Y = -10.5f;
                                jumpCounter++;
                            }
                        }

                        if (applyGravity == true)
                        {
                            acceleration.Y = 0.3f;
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

                        break;
                    case EnemyType.Flying:

                        if (applyGravity == true)
                        {
                            acceleration.Y = 0.3f;
                        }
                        else
                        {
                            acceleration.Y = 0.0f;
                        }
                        if (objectBelow == false)
                        {
                            applyGravity = true;

                        }
                        // Have the enemy flap semi-randomly.
                        if (counter >= 10)
                        {
                            int roll = rand.Next(0, 9);
                            if (roll > 2)
                            {
                                velocity.Y = -2.5f;
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




                        break;


                }
            }
            position += velocity;
            velocity += acceleration;
            UpdateCollisionBox();
            #endregion
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
            spriteBatch.Draw(animationList[animationListIndex][frameIndex], drawRec, Color.White);

            vertices[0].Position.X = drawRec.X;
            vertices[0].Position.Y = drawRec.Y;


            vertices[1].Position.X = drawRec.X + 100;
            vertices[1].Position.Y = drawRec.Y;


            Game1.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            if (determineEvent.ObjectType == "EdgeTile" && ai != EnemyType.Ghost)
            {
                OldPosition = new Vector2(OldPosition.X - velocity.X, OldPosition.Y - velocity.Y);
                if (Math.Floor(OldPosition.X + Width) <= determineEvent.Position.X
                    && ((OldPosition.Y + Height >= determineEvent.Position.Y &&
                    OldPosition.Y + Height <= determineEvent.Position.Y + determineEvent.Height)
                    || (OldPosition.Y <= determineEvent.Position.Y + determineEvent.Height
                    && OldPosition.Y >= determineEvent.Position.Y)))
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X - Width;
                    acceleration.X = 0;
                }
                else if (OldPosition.X >= determineEvent.Position.X + determineEvent.Width
                    && ((OldPosition.Y + Height >= determineEvent.Position.Y &&
                    OldPosition.Y + Height <= determineEvent.Position.Y + determineEvent.Height)
                    || (OldPosition.Y <= determineEvent.Position.Y + determineEvent.Height
                    && OldPosition.Y >= determineEvent.Position.Y)))
                {
                    velocity.X = 0;
                    position.X = determineEvent.Position.X + determineEvent.Width;
                    acceleration.X = 0;
                }
                else if (Math.Floor(OldPosition.Y + Height) <= determineEvent.Position.Y
                    && ((OldPosition.X + Width >= determineEvent.Position.X
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
                else if (Math.Floor(OldPosition.Y) > determineEvent.Position.Y + determineEvent.Height
                    && position.Y < determineEvent.Position.Y + determineEvent.Height
                    && ((OldPosition.X + Width >= determineEvent.Position.X
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
        }

        public override void C_NoCollisions()
        {

        }
    }
}
