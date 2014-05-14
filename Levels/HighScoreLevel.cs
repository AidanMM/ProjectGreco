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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;
using ProjectGreco.GameObjects;
using ProjectGreco.GameObjects.Buttons;

namespace ProjectGreco.Levels
{
    class HighScoreLevel : BaseState
    {
        public HighScoreLevel()
        {
            StreamReader stReader;
            try
            {
                stReader = new StreamReader(File.OpenRead("..\\Debug\\Content\\HighScores.txt"));
            }
            catch
            {
                StreamWriter stWriter = new StreamWriter(File.Create("..\\Debug\\Content\\HighScores.txt"));
                stReader = new StreamReader(File.OpenRead("..\\Debug\\Content\\HighScores.txt"));
            }
            string allText = stReader.ReadToEnd();
            stReader.Close();

            List<int> scoreList = new List<int>();

            string[] scores = allText.Split('\n');

            for (int i = 0; i < scores.Length -1; i++)
            {
                try
                {
                    scoreList.Add(System.Convert.ToInt32(scores[i]));
                }
                catch
                {

                }
            }

            for (int k = 0; k < scoreList.Count; k++)
            {
                for (int i = 0; i < scoreList.Count - 1; i++)
                {
                    if (scoreList[i] < scoreList[i + 1])
                    {
                        int temp = scoreList[i];
                        scoreList[i] = scoreList[i + 1];
                        scoreList[i + 1] = temp;
                    }
                }
            }
            
            AddObjectToHandler("Start button", new StartFromLeaderboard(scoreList));
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, 0), Game1.IMAGE_DICTIONARY["cursor"]));
            
            
        }
    }
}
