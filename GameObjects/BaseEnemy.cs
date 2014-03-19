using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace ProjectGreco.GameObjects
{
    class BaseEnemy : GameObject
    {
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

        /// <summary>
        /// Basic paramaterized constructor for the base enemy object. Does not account for health, use the health paramaterzied constructor for that
        /// </summary>
        /// <param name="animationList">The animations for the enemy</param>
        /// <param name="pos">The position to spawn the enemy at</param>
        public BaseEnemy(List<List<Texture2D>> animationList, Vector2 pos)
            : base(animationList, pos, "enemy")
        {

            //Create the vertices and their color
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(pos.X, pos.Y, 0);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(pos.X, pos.Y, 0);
            vertices[1].Color = Color.Green;

            currentHealth = maxHealth;
        }

        /// <summary>
        /// Base Enemy update also updates the vertices of the primitives
        /// </summary>
        public override void Update()
        {


            vertices[0].Position = new Vector3(position.X - (int)Game1.CAMERA_DISPLACEMENT.X - 200, position.Y - (int)Game1.CAMERA_DISPLACEMENT.Y - 100, 0);

            vertices[1].Position = new Vector3(collisionBox.X + (float)(this.collisionBox.Width * (float)((float)currentHealth / (float)maxHealth)) - Game1.CAMERA_DISPLACEMENT.X, vertices[0].Position.Y, 0);
            base.Update();
           // vertices[0].Position = new Vector3(position.X - (int)Game1.CAMERA_DISPLACEMENT.X - 400, collisionBox.Y - collisionBox.Height / 10 - (int)Game1.CAMERA_DISPLACEMENT.Y, 0);

            if (Game1.KBState.IsKeyDown(Keys.D1) && Game1.oldKBstate.IsKeyUp(Keys.D1) && Game1.KBState.IsKeyDown(Keys.LeftAlt))
            {
                currentHealth--;
            }
        }

        /// <summary>
        /// Includes the drawing of primitives
        /// </summary>
        /// <param name="spriteBatch">spritebatch for which to use to draw</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            
            Game1.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);
        }
    }
}
