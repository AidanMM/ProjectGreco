using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Linq;
using ProjectGreco.Levels;

//------------------------------------------------------------------------------+
//Author: Aidan                                                                 |
//Purpose: The object handler holds, updates, and detects collisions between all|
//game objects.                                                                 |
//------------------------------------------------------------------------------+


namespace ProjectGreco
{
    public class ObjectHandler
    {
        public Dictionary<String, GameObject> objectDictionary;

        public ObjectHandler()
        {
            objectDictionary = new Dictionary<string, GameObject>();
        }

        /// <summary>
        /// The list of strings used to manage all of the collisions.  This is used to loop through all of the items in the object dictionary
        /// </summary>
        public List<string> collisionList = new List<string>();

        /// <summary>
        /// Adds a game object to the object Dictionary.  If the object already exists, the name will be appeneded with a number
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <param name="objectToAdd">The Game Object To add</param>
        public void AddObjectToHandler(string name, GameObject objectToAdd)
        {

            int nameIndex = 2;
            while (true)
            {
                if(objectDictionary.ContainsKey(name) == false)
                {
                    objectDictionary.Add(name, objectToAdd);
                    collisionList.Add(name);
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
                if (objectDictionary.ContainsKey(name) == false)
                {
                    objectDictionary.Add(name, objectToAdd);
                    collisionList.Add(name);
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

        /// <summary>
        /// Detects all of the collisions that are currently happening on the game screen
        /// </summary>
        /// <returns>An array that contains the name of the objects that are colliding</returns>
        public string[,] DetectCollisions()
        {

            string[,] collidedObjects = new string[collisionList.Count, collisionList.Count];
            for (int x = 0; x < collisionList.Count; x++)
            {
                if (objectDictionary[collisionList[x]].CheckForCollisions == true)
                {
                    for (int y = 0; y < objectDictionary.Count; y++ )
                    {
                        if (objectDictionary[collisionList[y]].OnScreen == true && x != y)
                        {
                            if (objectDictionary[collisionList[x]].CollisionBox.Intersects(objectDictionary[collisionList[y]].CollisionBox))
                            {
                                collidedObjects[x, y] = collisionList[y];
                            }
                        }
                    }
                }
            }
            
            return collidedObjects;
        }

        /// <summary>
        /// Takes the collisions that were detected and calls of the the onColision functions of existing game objects
        /// </summary>
        public void TriggerCollisionEvents()
        {
            string[,] collidedObjects = DetectCollisions();

            for (int x = 0; x < collisionList.Count; x++)
            {
                for (int y = 0; y < collisionList.Count; y++)
                {
                    if (collidedObjects[x, y] != null)
                    {
                        objectDictionary[collisionList[x]].C_OnCollision(objectDictionary[collidedObjects[x,y]]);
                    }
                }
            }
        }

        /// <summary>
        /// Updates every game object in the object Dictionary
        /// </summary>
        public void Update()
        {
            for (int x = 0; x < objectDictionary.Count; x++)
            {
                TriggerCollisionEvents();
                objectDictionary[collisionList[x]].Update();

            }
        }

        /// <summary>
        /// Draws every object in the object Dictionary
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < objectDictionary.Count; x++)
            {
                objectDictionary[collisionList[x]].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds an object to the object dictionary, also adds the object to the collision list.
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <param name="objectToadd">The object to add</param>
        public void AddToObjectDictionary(string name, GameObject objectToadd)
        {
            objectDictionary.Add(name, objectToadd);
            collisionList.Add(name);
        }

        public void ChangeState(BaseState level)
        {
            objectDictionary = level.LevelObjectDictionary;
        }

    }
}
