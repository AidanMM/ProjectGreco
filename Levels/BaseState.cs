using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.GameObjects;

//------------------------------------------------------------------------------+
//Author: Aidan                                                                 |
//Purpose: The base state manages overall level logic and what objects that will|
//be created and managed in the game.                                           |
//------------------------------------------------------------------------------+


namespace ProjectGreco.Levels
{
    public class BaseState
    {
        /// <summary>
        /// The Object Dictionary for each individual level. 
        /// </summary>
        private Dictionary<string, GameObject> levelObjectDictionary;

        public Dictionary<string, GameObject> LevelObjectDictionary
        {
            get { return levelObjectDictionary; }
            set { levelObjectDictionary = value; }
        }

        public BaseState()
        {
            levelObjectDictionary = new Dictionary<string, GameObject>();

            
        }

        /// <summary>
        /// Adds a game object to the object Dictionary.  If the object already exists, the name will be appeneded with a number
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <param name="objectToAdd">The Game Object To add</param>
        protected void AddObjectToHandler(string name, GameObject objectToAdd)
        {

            int nameIndex = 2;
            while (true)
            {
                if (levelObjectDictionary.ContainsKey(name) == false)
                {
                    levelObjectDictionary.Add(name, objectToAdd);
                    Game1.OBJECT_HANDLER.collisionList.Add(name);
                    return;
                }
                else
                {
                    string[] temp = name.Split('_');
                    name = String.Format(temp[0] + "_" + "{0}", nameIndex);
                    nameIndex++;

                }
            }
        }
        /// <summary>
        /// Adds a game object to the object Dictionary.  If the object already exists, the name will be appeneded with a number
        /// This function with a start index will add the index to the object initially and move on from that point if it does not work
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <param name="objectToAdd">The Game Object To add</param>
        /// <param name="startIndex",>The index that you would like to start the adding process at.
        public void AddObjectToHandler(string name, GameObject objectToAdd, int startIndex)
        {

            int nameIndex = startIndex;
            string[] temp = name.Split('_');
            name = String.Format(temp[0] + "_" + "{0}", nameIndex);
            nameIndex++;
            while (true)
            {
                if (levelObjectDictionary.ContainsKey(name) == false)
                {
                    levelObjectDictionary.Add(name, objectToAdd);
                    Game1.OBJECT_HANDLER.collisionList.Add(name);
                    return;
                }
                else
                {
                    temp = name.Split('_');
                    name = String.Format(temp[0] + "_" + "{0}", nameIndex);
                    nameIndex++;

                }
            }
        }
    }
}
