using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects
{
    class Cursor : GameObject
    {
        Vector2 startingPosition;
        MouseState mouseState;
        MouseState prevMouseState;

        public Cursor(Vector2 startPos) : base(startPos, "Cursor")
        {
            CheckForCollisions = true;
            startingPosition = new Vector2(600, 320);
            position = startPos;
            onScreen = true;
            zOrder = 9001;
        }

        public override void Update()
        {
            OldPosition = new Vector2(position.X, position.Y);
            prevMouseState = mouseState;

            mouseState = Mouse.GetState();
            position.X = mouseState.X;
            position.Y = mouseState.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);     // yo aidan, I currently have a commented out hide the cursor in Game1, but how do I specify the texture of a cursor object?
        }
        /// <summary>
        /// Override of the base collisions to incorporate an action to happen when you collide with an object. This one will check to see if the cursor is interacting with a button
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
            if (determineEvent.ObjectType == "Button")
            {

            }
        }

    }
}
