using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects
{
    class Cursor : GameObject
    {
        Vector2 startingPosition;
        Vector2 screenPosition;
        Texture2D cursorSprite;
        bool mouseClicked;  // bool to determine if the mouse was clicked

        public bool MouseClicked { get { return mouseClicked; } }


        public Cursor(Vector2 startPos, Texture2D cS) : base(startPos, "Cursor")
        {
            CheckForCollisions = true;
            startingPosition = new Vector2(600, 320);
            cursorSprite = cS;
            position = startPos;
            mouseClicked = true;
            onScreen = true;
            zOrder = 10;
            animationList = Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Cursor"]);
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
            
            ///
            /// Checks to see if the mouse button was clicked
            ///
            #region ClickedChecks
            if ((Game1.mouseState.LeftButton == ButtonState.Released) && (Game1.prevMouseState.LeftButton == ButtonState.Released))
            {
                mouseClicked = false;
            }

            if ((Game1.mouseState.LeftButton == ButtonState.Pressed) && (Game1.prevMouseState.LeftButton == ButtonState.Released))
            {
                mouseClicked = true;
            }

            if ((Game1.mouseState.LeftButton == ButtonState.Pressed) && (Game1.prevMouseState.LeftButton == ButtonState.Pressed))
            {
                mouseClicked = true;
            }

            if ((Game1.mouseState.LeftButton == ButtonState.Released) && (Game1.prevMouseState.LeftButton == ButtonState.Pressed))
            {
                mouseClicked = false;
            }

            #endregion
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationList[animationListIndex][frameIndex], new Rectangle((int)screenPosition.X - 10, (int)screenPosition.Y - 10, (int)Width, (int)Height), Color.White);
        }
        /// <summary>
        /// Override of the base collisions to incorporate an action to happen when you collide with an object. This one will check to see if the cursor is interacting with a button
        /// 
        /// Determined for the time being that button does not need collision. Correct in the future if needed.
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
            Game1.TITLE_STRING = determineEvent.dictionaryName;
        }
        
    }
}
