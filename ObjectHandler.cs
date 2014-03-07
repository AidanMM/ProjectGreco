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
        /// Adds a game object to the object Dictionary.  If the object already exists, the name will be appeneded with a number
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <param name="objectToAdd">The Game Object To add</param>
        public void AddObjectToHandler(string name, GameObject objectToAdd)
        {

            int nameIndex = 2;
            while (true)
            {
                try
                {
                    objectDictionary.Add(name, objectToAdd);
                    return;
                }

                catch (Exception e)
                {
                    string[] temp = name.Split('_');
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
            string[,] collidedObjects = new string[objectDictionary.Count, objectDictionary.Count];
            for (int x = 0; x < objectDictionary.Count; x++)
            {
                for (int y = 0; y < objectDictionary.Count; y++)
                {
                    if (x != y)
                    {
                        //Bounding Box Collisions

                        var itemX = objectDictionary.ElementAt(x);
                        var itemY = objectDictionary.ElementAt(y);

                        if (objectDictionary[itemX.Key].CollisionBox.Intersects(objectDictionary[itemY.Key].CollisionBox))
                        {
                            collidedObjects[x, y] = itemY.Key;
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

            for (int x = 0; x < objectDictionary.Count; x++)
            {
                for (int y = 0; y < objectDictionary.Count; y++)
                {
                    if (collidedObjects[x, y] != null)
                    {
                        var itemX = objectDictionary.ElementAt(x);
                        objectDictionary[itemX.Key].C_OnCollision(objectDictionary[collidedObjects[x,y]]);
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
                var itemX = objectDictionary.ElementAt(x);
                objectDictionary[itemX.Key].Update();
                TriggerCollisionEvents();
                    
                
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

                var itemX = objectDictionary.ElementAt(x);
                objectDictionary[itemX.Key].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Adds an object to the object dictionary
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <param name="objectToadd">The object to add</param>
        public void AddToObjectDictionary(string name, GameObject objectToadd)
        {
            objectDictionary.Add(name, objectToadd);
        }

        public void ChangeState(BaseState level)
        {
            objectDictionary = level.LevelObjectDictionary;
        }

    }
}
