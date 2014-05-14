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
using Microsoft.Xna.Framework.Media;
using ProjectGreco.Levels;


namespace ProjectGreco.GameObjects.Buttons
{
    class HighScores : Button
    {
         bool cursorCollision = false;


        public HighScores()
            : base (new Vector2(50,50), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "        Scores", true)
        {
            position = new Vector2(500, 500);
            buttonText = "        Scores!";
        }

        

        public override void Update()
        {
            base.Update();
            position = new Vector2(Game1.OBJECT_HANDLER.objectDictionary["WanderingCam"].Position.X, Game1.OBJECT_HANDLER.objectDictionary["WanderingCam"].Position.Y + 100) ;

            if (cursorCollision == false)
            {
                A_GoToFrameIndex(0);
            }

            cursorCollision = false;
        }

        public override void DoThisOnClick()
        {
            // start game
            Game1.OBJECT_HANDLER.ChangeState(new HighScoreLevel());

           


        }

        public override void C_OnCollision(GameObject determineEvent)
        {
            if (determineEvent.ObjectType == "Cursor")
            {
                cursorCollision = true;
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

        public override void Draw(SpriteBatch spriteBatch)
        {

            base.Draw(spriteBatch);

            
        }
    }
}
