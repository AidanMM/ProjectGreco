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
    public class BossWeapon : GameObject
    {
        public enum BossState
        {
            Smash,
            Hover,
            Drag,
            Pause
        }
        public enum Direction
        {
            left,
            right
        }

        BossState myState;
        Vector2 originalPosition;
        public int health;
        private List<int> projectiles = new List<int>();
        public Player myPlayer;
        public int shakeTimer = 0;
        public int smashTimer;
        public int dragTimer = 0;
        private Random rand;
        public Direction myDirection;


        public BossWeapon(List<List<Texture2D>> animationList, Vector2 pos, Player myPlayer, Random rand)
            : base(animationList, pos, "BossWeapon")
        {
            this.myState = BossState.Pause;
            this.originalPosition = pos;
            this.health = 50;
            this.myPlayer = myPlayer;
            this.rand = rand;
            this.myDirection = Direction.left;
            smashTimer = 300;
            checkForCollisions = true;
            
        }

        public override void Update()
        {
            

            base.Update();
            onScreen = true;

            if (smashTimer > 0)
            {
                smashTimer-= rand.Next(1,4);
            }

            if (shakeTimer > 0)
            {
                myPlayer.Position = new Vector2(myPlayer.Position.X + rand.Next(-8, 9), myPlayer.Position.Y + rand.Next(-4, 1));
                shakeTimer--;
            }

            if (myState == BossState.Hover && (Math.Abs(position.X - originalPosition.X) < 8) && (Math.Abs(position.Y - originalPosition.Y) < 8))
            {
                myState = BossState.Pause;
                velocity.X = 0;
                velocity.Y = 0;
                acceleration.X = 0;
                acceleration.Y = 0;
                smashTimer = 300;
            }

            if (myState == BossState.Hover)
            {

                float distanceToOrigin = (float)Math.Pow((float)Math.Pow((originalPosition.X - position.X), 2) + (float)Math.Pow((originalPosition.Y - position.Y), 2), 0.5);

                float totalVelocity = 12.0f;
                float xPercent = (originalPosition.X - position.X) / (float)distanceToOrigin;
                float yPercent = (originalPosition.Y - position.Y) / (float)distanceToOrigin;

                velocity.X = totalVelocity * xPercent;
                velocity.Y = totalVelocity * yPercent;
            }

            if (myState == BossState.Pause && smashTimer <= 0)
            {
                myState = BossState.Smash;
                acceleration.Y = 1.0f;
            }

            if (myState == BossState.Drag && dragTimer >= 0)
            {
                if (myDirection == Direction.left)
                {
                    acceleration.X = -1.0f;
                }
                if (myDirection == Direction.right)
                {
                    acceleration.X = 1.0f;
                }

                dragTimer--;
                if (dragTimer == 0)
                {
                    myState = BossState.Hover;
                }
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
                        if (!destroyThis)
                        {
                            destroyThis = true;
                            PlayerStats.mula += 2000;
                        }
                    }
                }
            }

            if (determineEvent.ObjectType == "Sword")
            {
                health--;
                if (health <= 0)
                {
                    if (!destroyThis)
                    {
                        destroyThis = true;
                        PlayerStats.mula += 2000;
                    }
                }
            }

            if (determineEvent.ObjectType == "Player" && myPlayer.FlashCounter == 0)
            {
                myPlayer.FlashCounter = 60;
                myPlayer.health--;

                if (myPlayer.health == 0)
                {
                    myPlayer.health = 3;
                    if (Game1.OBJECT_HANDLER.currentState.LevelType != LevelName.Home)
                    {
                        (Game1.OBJECT_HANDLER.currentState as Level).PositionPlayer();
                    }
                    PlayerStats.mula -= 1000;
                    if (PlayerStats.mula < 0)
                        PlayerStats.mula = 0;
                }
            }

            if (determineEvent.ObjectType == "EdgeTile" && myState == BossState.Smash)
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

                velocity.Y = 0;
                velocity.X = 0;
                acceleration.Y = 0;
                acceleration.X = 0;
                shakeTimer = 30;
                myState = BossState.Drag;
                if (myPlayer.Position.X > position.X)
                {
                    myDirection = Direction.right;
                }
                else
                {
                    myDirection = Direction.left;
                }
                dragTimer = 45;
            }
        }
    }
}
