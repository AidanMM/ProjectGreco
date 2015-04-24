using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using Microsoft.Xna.Framework.Media;
using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects.Buttons
{
    class StartFromLeaderboard : Button
    { 
        bool cursorCollision = false;
        List<int> scoreList;

        public StartFromLeaderboard(List<int> scores)
            : base(new Vector2(1000, 600), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "        Start", true)
        {
           
            buttonText = "         Start!";
            scoreList = scores;
        }

        

        public override void Update()
        {
            

            if (cursorCollision == false)
            {
                A_GoToFrameIndex(0);
            }

            cursorCollision = false;
        }

        public override void DoThisOnClick()
        {

            Game1.OBJECT_HANDLER.ChangeState(new HomeWorld());
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
            spriteBatch.Draw(Game1.ANIMATION_DICTIONARY["highScoreTable"][0], new Vector2(0, 0), Color.White);

            if (scoreList.Count < 10)
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    if(i < 5)
                        spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, "" + scoreList[i], new Vector2(position.X - 600, position.Y - 410 + i * 90), Color.White);
                    else
                        spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, "" + scoreList[i], new Vector2(position.X - 280, position.Y - 410 + (i - 5) * 90), Color.White);
                }
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (i < 5)
                        spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, "" + scoreList[i], new Vector2(position.X - 600, position.Y - 410 + i * 90), Color.White);
                    else
                        spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, "" + scoreList[i], new Vector2(position.X - 280, position.Y - 410 + (i - 5) * 90), Color.White);
                }
            }

            base.Draw(spriteBatch);

            
        }

        
    }
}

