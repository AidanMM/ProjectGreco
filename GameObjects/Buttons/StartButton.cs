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
    class StartButton : Button
    {
        bool cursorCollision = false;


        public StartButton()
            : base (new Vector2(50,50), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["ButtonStates"]), "        Start", true)
        {
            position = new Vector2(500, 500);
            buttonText = "         Start!";
        }

        

        public override void Update()
        {
            base.Update();
            position = Game1.OBJECT_HANDLER.objectDictionary["WanderingCam"].Position;

            if (cursorCollision == false)
            {
                A_GoToFrameIndex(0);
            }

            cursorCollision = false;
        }

        public override void DoThisOnClick()
        {
            // start game
            MediaPlayer.Play(Game1.SONG_LIBRARY["HomeWorldMusic"]);  
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
            
            spriteBatch.Draw(Game1.ANIMATION_DICTIONARY["Overlay"][0], new Vector2(collisionBox.X - 1280 / 2 - (int)Game1.CAMERA_DISPLACEMENT.X ,
                collisionBox.Y - 720 / 2 - (int)Game1.CAMERA_DISPLACEMENT.Y), Color.White);

            spriteBatch.Draw(Game1.ANIMATION_DICTIONARY["titleLogo"][0], new Vector2(330, 78), Color.White);

            base.Draw(spriteBatch);

            
        }

        
    }
}
