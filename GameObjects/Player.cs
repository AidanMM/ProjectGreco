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
    class Player : GameObject
    {
        float speed = .1f;
        bool applyGravity = true;

        VertexPositionColor[] vertices;
        
        
        public Player(Vector2 startPos) : base(startPos, "Player")
        {
            
            vertices = new VertexPositionColor[3];
            vertices[0].Position = new Vector3(100, 100, 0);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(500, 100, 0);
            vertices[1].Color = Color.Green;
            
        }
        public Player(Vector2 startPos, List<List<Texture2D>> aList)
            : base(aList, startPos, "Player")
        {
            

            
            vertices = new VertexPositionColor[2];
            vertices[0].Position = new Vector3(startPos.X, startPos.Y, 0);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(startPos.X + collisionBox.Width, startPos.Y, 0);
            vertices[1].Color = Color.Green;
            
        }

        public override void Update()
        {
            base.Update();

            if (Game1.KBState.IsKeyDown(Keys.Left))
            {
                velocity.X -= speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Right))
            {
                velocity.X += speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Down))
            {
                velocity.Y += speed;
            }
            if (Game1.KBState.IsKeyDown(Keys.Up) && !Game1.oldKBstate.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 10.0f;
                applyGravity = true;
            }
            if (Game1.KBState.IsKeyDown(Keys.B))
            {
                velocity.X = 0;
                velocity.Y = 0;
            }
            if (position.X > 800 + animationList[animationListIndex][frameIndex].Width)
            {
                position.X = 0 - animationList[animationListIndex][frameIndex].Width;
            }
            if (position.X < 0 - animationList[animationListIndex][frameIndex].Width)
            {
                position.X = 800;
            }
            if (position.Y < 0 - animationList[animationListIndex][frameIndex].Height)
            {
                position.Y = 480;
            }
            if (position.Y > 480 + animationList[animationListIndex][frameIndex].Height)
            {
                position.Y = 0 - animationList[animationListIndex][frameIndex].Height;
            }

            if (applyGravity == true)
            {
                acceleration.Y = .2f;
            }
            else
            {
                acceleration.Y = 0.0f;
            }
            //Update the primitives
            vertices[0].Position = new Vector3(collisionBox.X , collisionBox.Y - collisionBox.Height / 10, 0);
            vertices[1].Position = new Vector3(collisionBox.X + collisionBox.Width, collisionBox.Y - collisionBox.Height / 10, 0);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            
            Game1.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, vertices, 0, 1);

        }

        /// <summary>
        /// Override of the base collisions to incororate an action to happen when you collide with an object. This one checks what side it collided on and fixes the issues
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
            base.C_OnCollision(determineEvent);
            if (determineEvent.ObjectType == "Block")
            {
               

                 if (OldPosition.Y < determineEvent.Position.Y)
                {
                    position.Y = determineEvent.Position.Y - this.collisionBox.Height;
                    velocity.Y = 0;
                }
                 if (OldPosition.Y > determineEvent.Position.Y)
                {
                    position.Y = determineEvent.Position.Y + this.collisionBox.Height;
                    velocity.Y = 0;
                }
                 applyGravity = false;


                
            }
            
                
        }
    }
}
