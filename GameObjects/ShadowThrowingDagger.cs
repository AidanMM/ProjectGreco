using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;
using ProjectGreco.Skills;

namespace ProjectGreco.GameObjects
{
    class ShadowThrowingDagger : ThrowingDagger
    {
        public Player playerToHold;

        public ShadowThrowingDagger(Vector2 vel, Vector2 startPos, Player toPass)
            : base(vel, startPos)
        {
            ObjectType = "ShadowDagger";
            playerToHold = toPass;
            checkForCollisions = true;
            if (toPass.HFlip == true)
            {
                this.hFlip = true;
                velocity = new Vector2(-vel.X, 0);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (hFlip == false)
            {
                spriteBatch.Draw(animationList[animationListIndex][frameIndex],
                    new Vector2(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X + Width / 2, collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y + Height / 2),
                    new Rectangle(0, 0, collisionBox.Width, collisionBox.Height),
                    Color.Black,
                    angle,
                    new Vector2(Width / 2, Height / 2),
                    scale,
                    SpriteEffects.None,
                    1);
            }
            else if (hFlip == true)
            {
                spriteBatch.Draw(animationList[animationListIndex][frameIndex],
                    new Vector2(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X + Width / 2, collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y + Height / 2),
                    new Rectangle(0, 0, collisionBox.Width, collisionBox.Height),
                    Color.Black,
                    angle,
                    new Vector2(Width / 2, Height / 2),
                    scale,
                    SpriteEffects.FlipHorizontally,
                    1);
            }
        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            base.C_OnCollision(determineEvent);

            if (determineEvent.ObjectType == "enemy")
            {
                if (hFlip != true)
                {
                    playerToHold.Position = new Vector2(determineEvent.Position.X + determineEvent.Width + 10, determineEvent.Position.Y);
                }
                else
                {
                    playerToHold.Position = new Vector2(determineEvent.Position.X - playerToHold.Width - 10, determineEvent.Position.Y);
                }
                destroyThis = true;
                new Ghost(playerToHold);
            }
        }

        

    }
}
