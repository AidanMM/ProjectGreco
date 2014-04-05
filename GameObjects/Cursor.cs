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
        Vector2 screenPosition;
        Texture2D cursorSprite;


        public Cursor(Vector2 startPos, Texture2D cS) : base(startPos, "Cursor")
        {
            CheckForCollisions = true;
            startingPosition = new Vector2(600, 320);
            cursorSprite = cS;
            position = startPos;
            onScreen = true;
            zOrder = 10;
        }
        

        public override void Update()
        {
            OldPosition = new Vector2(position.X, position.Y);
            Game1.prevMouseState = Game1.mouseState;

            Game1.mouseState = Mouse.GetState();
            screenPosition.X = Game1.mouseState.X;
            screenPosition.Y = Game1.mouseState.Y;
            position.X = Game1.CAMERA_DISPLACEMENT.X + Game1.mouseState.X;
            position.Y = Game1.CAMERA_DISPLACEMENT.Y + Game1.mouseState.Y;

            UpdateCollisionBox();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(cursorSprite, new Rectangle((int)screenPosition.X, (int)screenPosition.Y, 20, 20), Color.White);
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

            Game1.TITLE_STRING = determineEvent.dictionaryName;
        }

    }
}
