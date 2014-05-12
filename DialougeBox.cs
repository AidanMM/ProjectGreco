using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;
using ProjectGreco.Levels;
using ProjectGreco.Events;
using System.IO;


namespace ProjectGreco
{
    public class DialougeBox
    {
        /// <summary>
        /// The text that is displayed on the actual dialouge box
        /// </summary>
        public string displayText;

        /// <summary>
        /// The entire dialouge to be displayed on the dialouge box
        /// </summary>
        public string dialougeText;

        /// <summary>
        /// All of the text in the game to be used for displayTexts
        /// </summary>
        public string allText;

        /// <summary>
        /// Whether or not to show text and add character at a time to the display box
        /// </summary>
        public bool showText;

        /// <summary>
        /// The ammount of characters to show per line
        /// </summary>
        public int charLimit;

        /// <summary>
        /// The ammount of lines to show per dialouge box
        /// </summary>
        public int lineLimit;

        public int dialougeTextIndex;

        public int charsPerLine;

        public int curLines;

        public bool waitForInput;

        public bool scaleUpBox = true;

        string[] dialouges;

        public List<Event> eventList;

        Vector2 scale;

        public DialougeBox()
        {
            displayText = "";
            dialougeText = "This is a test Text message, this is made so that we can test, well, this message.  It is a message for testing You know, like test test test test, awefboiawebfoaebfouhabwefipawefpiawefpiubwfpiubfepiuwbefpiubawefpiubawefpiuabwefpiuabefpiuabewfiuabfpiufpiuibewfpaiuwebfapwiubfapwibfawpebfapwebfapwiebfpiubawpeiufbawepfiubawepfiubawefpiubwefpiuabwrfpiubawefpiubwefpiubawefpiubawefpiubawefpiubwaefpiubawefpiubawefpiubawefpiubawefpibawefpibawefpiubwaefpiubawefpibawefpibwefpiubweafpibwaefpiubwaefpiubawefpiubwefpiubwefpiubwefpiubawefpiubawefpibawefpiubwefpiubawefpiubwaefpiubwefpiubawefpiubwaefpiubawefpiubawefpiubwaefpiuawefpiubawefpiubwaefpiubaefpubsvdfslhreb;njfewhoybife jr;osgf;esbrhnobsreuingiufweligsdnrgwefnaehfavrluahrowbrueg;semoihpoirghmnbrshtuvrgtlusrhlgynr;ofhse8;orhvwebnbyurewgirguprwgyqevliufaewboy8oabratrp9ebf8awbroi3no8rw4mt";
            allText = "";
            showText = false;
            charLimit = 140;
            lineLimit = 10;
            dialougeTextIndex = 0;
            curLines = 0;
            waitForInput = false;
            charsPerLine = 0;
            StreamReader stReader = new StreamReader(File.OpenRead("..\\Debug\\Content\\AllText.txt"));
            allText = stReader.ReadToEnd();
            stReader.Close();
            dialouges = allText.Split('\n');

            eventList = new List<Event>();
            eventList.Add(new SkillTreeEvent(3));
            scale = new Vector2(0, 0);
        }

        public void Update()
        {
            if (showText == true)
            {
                if (scaleUpBox == false)
                {

                    if (waitForInput == false)
                    {
                        displayText += dialougeText[dialougeTextIndex];
                        charsPerLine++;
                        dialougeTextIndex++;
                        if (dialougeTextIndex >= dialougeText.Length)
                        {
                            waitForInput = true;
                        }
                        if (charsPerLine > charLimit)
                        {
                            displayText += "\n";
                            charsPerLine = 0;
                            curLines++;
                            if (curLines == lineLimit)
                            {
                                waitForInput = true;
                            }
                        }
                    }
                    else if (Game1.KBState.IsKeyDown(Keys.Space) || Game1.KBState.IsKeyDown(Keys.Enter))
                    {
                        if (dialougeTextIndex >= dialougeText.Length)
                        {
                            HideTextBox();
                        }
                        else
                            MoveToNextChunk();
                    }
                }

            }

            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].Update() && eventList[i].Happened == false)
                {
                    ShowNewDialouge(eventList[i].index);
                    eventList[i].Happened = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont spriteFont, Vector2 pos)
        {
            if (showText == true)
            {
                if (scaleUpBox == false)
                {
                    spriteBatch.Draw(Game1.ANIMATION_DICTIONARY["DialougeBox"][0], pos, Color.White);
                    spriteBatch.DrawString(spriteFont, displayText, new Vector2(pos.X + 20, pos.Y + 20), Color.White);
                    spriteBatch.DrawString(spriteFont, "Press space to continue...", new Vector2(pos.X + 1050, pos.Y + 170),Color.White);
                }
                else
                {
                    /*spriteBatch.Draw(animationList[animationListIndex][frameIndex],
                    new Vector2(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X + Width / 2, collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y + Height / 2),
                    new Rectangle(0, 0, collisionBox.Width, collisionBox.Height),
                    Color.White,
                    angle,
                    new Vector2(Width / 2, Height / 2),
                    scale,
                    SpriteEffects.None,
                    1);*/
                    spriteBatch.Draw(Game1.ANIMATION_DICTIONARY["DialougeBox"][0],
                        pos,
                        new Rectangle(0, 0, Game1.ANIMATION_DICTIONARY["DialougeBox"][0].Width, Game1.ANIMATION_DICTIONARY["DialougeBox"][0].Height),
                        Color.White,
                        0,
                        new Vector2(0,0),
                        scale,
                        SpriteEffects.None,
                        1);
                    

                    scale = new Vector2(scale.X + .1f, scale.Y + .1f);
                    if (scale.X >= 1)
                    {
                        scaleUpBox = false;
                        scale = new Vector2(0, 0);
                    }

                }
            }
        }

        public void ShowDialouge()
        {
            showText = true;
            Game1.pauseObjectUpdate = true;
        }

        public void ShowNewDialouge()
        {
            ShowDialouge();
            curLines = 0;
            displayText = "";
            dialougeTextIndex = 0;
            charsPerLine = 0;
            waitForInput = false;
        }

        public void ShowNewDialouge(int index)
        {
            ShowDialouge();
            curLines = 0;
            displayText = "";
            dialougeTextIndex = 0;
            charsPerLine = 0;
            waitForInput = false;
            if (index >= dialouges.Length)
            {
                index = dialouges.Length - 1;
            }
            dialougeText = dialouges[index];
            dialougeText = dialougeText.Remove(0, 2);
            
            
        }

        public void MoveToNextChunk()
        {
            curLines = 0;
            waitForInput = false;
            displayText = "";
        }

        public void HideTextBox()
        {
            showText = false;
            Game1.pauseObjectUpdate = false;
            

        }
    }
}
