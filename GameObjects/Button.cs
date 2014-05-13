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
    class Button : GameObject
    {
        int buttonState;    // controls button state: 0- base 1- over 2- down
        List<List<Texture2D>> buttonList;
        protected bool clickable;

        /// <summary>
        /// The text to display on this button
        /// </summary>
        protected string buttonText;

        public Button(Vector2 startPos, List<List<Texture2D>> aList, string bTxt, bool click)
            : base(aList, startPos, "Button")
        {
            checkForCollisions = true;
            buttonList = aList;
            position = startPos;
            onScreen = true;
            clickable = click;
            buttonState = 0;
            buttonText = "";
            zOrder = 9;            
        }

        public override void Update()
        {
            UpdateCollisionBox();            
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
			base.Draw(spriteBatch);
            spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, buttonText, new Vector2(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X,
                collisionBox.Y + Height / 2 - 13  - (int)Game1.CAMERA_DISPLACEMENT.Y), Color.White);
			
        }
        
        /// <summary>
        /// Override of the base collisions to incorporate an action to happen when you collide with an object. This one will check to see if the cursor is interacting with a button
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
            if (determineEvent.ObjectType == "Cursor")
            {

                if (clickable == true)
                {
                    if ((determineEvent as Cursor).MouseClicked == false)
                    {
                        A_GoToFrameIndex(1);
                    }
                    else if ((determineEvent as Cursor).MouseClicked == true)
                    {
                        A_GoToFrameIndex(2);
                        DoThisOnClick();
                    }
                    else
                    {
                        A_GoToFrameIndex(0);
                    }
                }

            }
            
			
        }

		public override void C_NoCollisions()
		{
			A_GoToFrameIndex(0);
		}

        public virtual void DoThisOnClick()
        {
        }
    }
}
