using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using ProjectGreco.Levels;
using ProjectGreco.GameObjects;

namespace ProjectGreco.GameObjects
{
    class Projectile : GameObject
    {
        /// <summary>
        /// Set this bool to true to have the object be destroyed in the next ran update cycle
        /// </summary>
        protected bool destroy = false;

        /// <summary>
        /// The angle that the object will be rotated
        /// </summary>
        protected float angle = 0.0f;

        /// <summary>
        /// Constructor for the projectile base class to set a few key details for the object
        /// </summary>
        /// <param name="vel">The speed and direction for the object to go</param>
        /// <param name="pos">The position for the object to spawn at</param>
        /// <param name="aList">the animations to use</param>
        /// <param name="name">the name of this type of projectile</param>
        public Projectile(Vector2 vel, Vector2 pos, List<List<Texture2D>> aList, string name, float angleToSet)
            : base(aList, pos, name)
        {
            velocity = vel;
            position = pos;
            Game1.OBJECT_HANDLER.currentState.AddObjectToHandler("Name", this);
            checkForCollisions = true;
            angle = angleToSet;
        }

        public override void Update()
        {
            base.Update();
            if (destroy == true)
            {
                Destroy();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(animationList[animationListIndex][frameIndex], 
            //new Rectangle(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X, 
            //collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, collisionBox.Width, collisionBox.Height), Color.White);

            spriteBatch.Draw(animationList[animationListIndex][frameIndex],
                new Rectangle(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X, collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, collisionBox.Width, collisionBox.Height),
                new Rectangle(0, 0, collisionBox.Width, collisionBox.Height),
                Color.White,
                angle,
                new Vector2(collisionBox.Width / 2, collisionBox.Height / 2),
                SpriteEffects.None,
                1);
                

        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            base.C_OnCollision(determineEvent);

            if (determineEvent.ObjectType == "EdgeTile")
            {
                destroy = true;
            }
        }
        
    }
}
