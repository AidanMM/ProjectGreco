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
            spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, "Highest Scores obtained:", new Vector2(position.X - 1000, position.Y - 600), Color.Black);
            for (int i = 0; i < scoreList.Count; i++)
            {
                spriteBatch.DrawString(Game1.DEFUALT_SPRITEFONT, "" +scoreList[i], new Vector2(position.X - 900, position.Y - 580 + i * 40), Color.Black);
            }

            base.Draw(spriteBatch);

            
        }

        
    }
}

