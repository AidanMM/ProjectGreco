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
        Cursor cO;

        public Button(Vector2 startPos, List<List<Texture2D>> aList)
            : base(aList, startPos, "Button")
        {
            checkForCollisions = true;
            buttonList = aList;
            position = startPos;
            onScreen = true;
            buttonState = 0;
            zOrder = 20;            
        }

        public override void Update()
        {
            UpdateCollisionBox();            
            //base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationList[0][buttonState], new Rectangle((int)position.X, (int)position.Y, 128, 64), Color.White);
        }
        
        /// <summary>
        /// Override of the base collisions to incorporate an action to happen when you collide with an object. This one will check to see if the cursor is interacting with a button
        /// </summary>
        /// <param name="determineEvent"></param>
        public override void C_OnCollision(GameObject determineEvent)
        {
            if (determineEvent.ObjectType == "Cursor")
            {
                
            }
        }
    }
}
