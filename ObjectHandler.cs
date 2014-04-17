using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using System.Linq;
using ProjectGreco.Levels;

//------------------------------------------------------------------------------ +
//Author: Aidan                                                                 |
//Purpose: The object handler holds, updates, and detects collisions between all|
//game objects.                                                                 |
//------------------------------------------------------------------------------ +


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
        /// The list of strings used to manage all of the objects that need to collide.
        /// </summary>
        public List<string> collisionList = new List<string>();


        /// <summary>
        /// This list of objects that are on screen
        /// </summary>
        public List<string> onScreenList = new List<string>();

        /// <summary>
        /// The objects that willl actually be collision checked
        /// </summary>
        public List<string> collisionCheckList = new List<string>();

        /// <summary>
        /// The level state of the game.  This holds the levels variables and so forth.
        /// </summary>
        public BaseState currentState;

        /// <summary>
        /// This bool tells the central update loop whether or not to stop looping.
        /// </summary>
        public bool escapeBool = false;
        

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
                    objectToAdd.dictionaryName = name;
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
                    objectToAdd.dictionaryName = name;
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
            
            for (int x = 0; x < collisionCheckList.Count; x++)
            {
                objectDictionary[collisionCheckList[x]].ObjectBelow = false;
                for (int y = 0; y < onScreenList.Count; y++)
                {
                    if (collisionCheckList[x] != onScreenList[y])
                    {
                        float distanceY = objectDictionary[collisionCheckList[x]].CollisionBox.Y - objectDictionary[onScreenList[y]].CollisionBox.Y;
                        Vector2 distanceBetweenObjects = new Vector2(objectDictionary[collisionCheckList[x]].CollisionBox.X - objectDictionary[onScreenList[y]].CollisionBox.X, Math.Abs(distanceY));

                        if ((distanceBetweenObjects.X >= 0 && distanceBetweenObjects.X < objectDictionary[onScreenList[y]].CollisionBox.Width) || (distanceBetweenObjects.X <= 0 && Math.Abs(distanceBetweenObjects.X) < objectDictionary[collisionCheckList[x]].CollisionBox.Width))
                        {
                            if (distanceBetweenObjects.Y < objectDictionary[collisionCheckList[x]].CollisionBox.Height)
                            {
                                collidedObjects[x, y] = onScreenList[y];
                            }
                            if (distanceY > -objectDictionary[collisionCheckList[x]].CollisionBox.Height - 5 && distanceY < 0)
                            {
                                if (objectDictionary[onScreenList[y]].ObjectType == "EdgeTile" || objectDictionary[onScreenList[y]].ObjectType == "OneWayBlock")
                                objectDictionary[collisionCheckList[x]].ObjectBelow = true;
                                
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

            for (int x = 0; x < collisionCheckList.Count; x++)
            {
                bool collideCall = false;
                for (int y = 0; y < onScreenList.Count; y++)
                {
                    if (collidedObjects[x, y] != null )
                    {
                        if (objectDictionary.ContainsKey(collidedObjects[x, y]))
                        {
                            objectDictionary[collisionCheckList[x]].C_OnCollision(objectDictionary[collidedObjects[x, y]]);
                            collideCall = true;
                        }
                    }
                }
                if (collideCall == false)
                {
                    objectDictionary[collisionCheckList[x]].C_NoCollisions();
                }
            }
        }

        /// <summary>
        /// Updates every game object in the object Dictionary.  Also collide the OnScreen List and the collision list
        /// </summary>
        public void Update()
        {
            onScreenList.Clear();
            collisionCheckList.Clear();
            for (int x = 0; x < objectDictionary.Count; x++)
            {

                objectDictionary[collisionList[x]].Update();
                if (escapeBool == true)
                {
                    escapeBool = false;
                    return;
                }
                if (objectDictionary[collisionList[x]].OnScreen == true)
                {
                    onScreenList.Add(collisionList[x]);
                    if (objectDictionary[collisionList[x]].CheckForCollisions == true)
                        collisionCheckList.Add(collisionList[x]);
                }
            }
            TriggerCollisionEvents();
        }

        /// <summary>
        /// Draws every object in the object Dictionary
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < onScreenList.Count; x++)
            {
                if (objectDictionary[onScreenList[x]].OnScreen)
                {
                    objectDictionary[onScreenList[x]].Draw(spriteBatch);
                }
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

        /// <summary>
        /// This function will change the state of the game to the passed in state
        /// </summary>
        /// <param name="level"></param>
        public void ChangeState(BaseState level)
        {
            currentState = level;
            objectDictionary = currentState.LevelObjectDictionary;
			onScreenList = new List<string>();
            collisionList = currentState.collisionList;
            Game1.CAMERA_DISPLACEMENT = new Vector2(0, 0);
            escapeBool = true;
        }

        /// <summary>
        /// This function will sort the object dictionary and collision dictionary by the zOrder so that they will be drawn correctly
        /// </summary>
        public void SortByZorder()
        {
            objectDictionary = objectDictionary.OrderBy(x => x.Value.ZOrder).ToDictionary(x => x.Key, x => x.Value);

            collisionList = new List<string>();

            foreach (var key in objectDictionary)
            {
                collisionList.Add(key.Key);
            }

        }

    }
}
