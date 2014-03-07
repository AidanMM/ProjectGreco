using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

//------------------------------------------------------------------------------+
//Author: Aidan                                                                 |
//Purpose: The base game piece object, generic gamepieces must not be made, as  |
//They are meant to be a parent class.  Every game object will be added to the  |
//Object handlers Dictionary of objects.  All update and draws will be handled  |
//by the object handler.                                                        |
//------------------------------------------------------------------------------+


namespace ProjectGreco
{
    public class GameObject
    {
        /// <summary>
        /// This is a list of lists.  The reason for this being that to contain each of the seperate animations, you must have a seperate
        /// list to hold it.  For example, animationList[0][2] would be the third frame of the idle animation. Idle animation being the first animation
        /// in the list of animations.  One list contained in the animation list will be looped through create an animation
        /// </summary>
        protected List<List<Texture2D>> animationList;

        /// <summary>
        /// This is a list of lists.  The reason for this being that to contain each of the seperate animations, you must have a seperate
        /// list to hold it.  For example, animationList[0][2] would be the third frame of the idle animation. Idle animation being the first animation
        /// in the list of animations.  One list contained in the animation list will be looped through create an animation
        /// </summary>
        public List<List<Texture2D>> AnimationList
        {
            get { return animationList; }
            set { animationList = value; }
        }

        /// <summary>
        /// Index to use for animation
        /// </summary>
        protected int animationListIndex;

        /// <summary>
        /// Index of the frame in the animation
        /// </summary>
        protected int frameIndex;

        /// <summary>
        /// A bool to control whether or not the game object is animating
        /// Deafult value is false
        /// </summary>
        protected bool animating;

        /// <summary>
        /// A bool to control whether or not the object's animation will loop
        /// Default value is true
        /// </summary>
        protected bool looping;

        /// <summary>
        /// This holds the x and y position of the object.  The sprite is drawn to this position
        /// </summary>
        protected Vector2 position;

        /// <summary>
        /// This holds the x and y position of the object.  The sprite is drawn to this position
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 oldPosition;

        protected Vector2 OldPosition
        {
            get { return oldPosition; }
            set { oldPosition = value; }
        }

        /// <summary>
        /// The velocity is added to the position every game loop.
        /// </summary>
        protected Vector2 velocity;

        /// <summary>
        /// The velocity is added to the position every game loop.
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// Accelerationn is added to the velocity every game loop.
        /// </summary>
        protected Vector2 acceleration;

        /// <summary>
        /// Accelerationn is added to the velocity every game loop.
        /// </summary>
        public Vector2 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        /// <summary>
        /// The collision box of the given game object
        /// </summary>
        protected Rectangle collisionBox;

        public Rectangle CollisionBox
        {
            get { return collisionBox; }
            set { collisionBox = value; }
        }

        public string ObjectType;
        

       
        /// <summary>
        /// Default constructor, simply initializes a base texture and a base position
        /// </summary>
        public GameObject()
        {
            animationList = new List<List<Texture2D>>();
            animationList.Add(new List<Texture2D>());
            animationList[0].Add(Game1.DEFAULT_TEXTURE);

            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            animationListIndex = 0;
            frameIndex = 0;
            looping = true;
            animating = false;

            collisionBox = new Rectangle((int)position.X, (int)position.Y, animationList[0][0].Width, animationList[0][0].Height);

            ObjectType = "Basic_Object";

        }

        /// <summary>
        /// Paramaterized constructor to create a game object with a degfault image, starting position, and a object type
        /// </summary>
        /// <param name="pos">The position to start the object at in the world</param>
        /// <param name="GameObjectType">The type of object that it is</param>
        public GameObject(Vector2 pos, string GameObjectType)
        {
            animationList = new List<List<Texture2D>>();
            animationList.Add(new List<Texture2D>());
            animationList[0].Add(Game1.DEFAULT_TEXTURE);

            position = pos;
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            animationListIndex = 0;
            frameIndex = 0;
            looping = true;
            animating = false;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, animationList[0][0].Width, animationList[0][0].Height);

            ObjectType = GameObjectType;

        }

        /// <summary>
        /// Paramaterized constructor to create a game object with a given set of animations, starting position, and a object type
        /// </summary>
        /// <param name="animations">The set of animaitons for the object to have</param>
        /// <param name="pos">The position to start the object at in the world</param>
        /// <param name="GameObjectType">The type of object that it is</param>
        public GameObject(List<List<Texture2D>> animations, Vector2 pos, string GameObjectType)
        {
            animationList = animations;
            position = pos;
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            animationListIndex = 0;
            frameIndex = 0;
            looping = true;
            animating = false;
            collisionBox = new Rectangle((int)position.X, (int)position.Y, animationList[0][0].Width, animationList[0][0].Height);

            ObjectType = GameObjectType;
            
        }

        /// <summary>
        /// Base update function. Adds velocity to positon, adds accelerations to velocity
        /// </summary>
        public virtual void Update()
        {
            oldPosition = new Vector2(position.X, position.Y);

            position -= Game1.CAMERA_DISPLACEMENT;
            position += velocity;
            velocity += acceleration;

            UpdateCollisionBox();

            if (animating == true)
            {
                frameIndex++;
                if (frameIndex >= animationList[animationListIndex].Count && looping == true)
                    frameIndex = 0;
                else if (frameIndex >= animationList[animationListIndex].Count && looping == false)
                    A_StopAnimating();

                
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(animationList[animationListIndex][frameIndex], collisionBox, Color.White);
        }

        /// <summary>
        /// Start the animating process
        /// </summary>
        public void A_BeginAnimation()
        {
            animating = true;
        }

        /// <summary>
        /// Stop the animation process
        /// </summary>
        public void A_StopAnimating()
        {
            animating = false;
        }

        /// <summary>
        /// Switch animation index to the given index
        /// </summary>
        /// <param name="index">Index to switch to</param>
        public void A_SwitchAnimationIndex(int index)
        {
            animationListIndex = index;
            frameIndex = 0;
        }

        /// <summary>
        /// Goes to the next animation index if it exists
        /// </summary>
        public void A_GoToNextAnimationIndex()
        {
            if (animationList[animationListIndex + 1][0] != null)
            {
                animationListIndex++;
                frameIndex = 0;
            }
        }
        /// <summary>
        /// Goes to the previous animation index if it exists
        /// </summary>
        public void A_GoToPreviousAnimationIndex()
        {
            if (animationList[animationListIndex - 1][0] != null && animationListIndex != 0)
            {
                animationListIndex--;
                frameIndex = 0;
            }
        }

        /// <summary>
        /// Goes to the next frame if it exists, or loops around to the begining if looping is set to true.
        /// </summary>
        public void A_GoToNextFrame()
        {
            if (animationList[animationListIndex][frameIndex + 1] != null)
            {
                frameIndex++;
            }
            else if (looping == true)
            {
                animationListIndex = 0;
            }
        }

        /// <summary>
        /// Goes to the previous frame if it exists
        /// </summary>
        public void A_GoToPreviousFrame()
        {
            if (animationList[animationListIndex][frameIndex - 1] != null && animationListIndex != 0)
            {
                frameIndex--;
            }
        }
        /// <summary>
        /// Goes to the chosen index if it exists
        /// </summary>
        /// <param name="index">Index to go to</param>
        public void A_GoToFrameIndex(int index)
        {
            if(animationList[animationListIndex][index] != null)
            {
                frameIndex = index;
            }
        }

        /// <summary>
        /// Toggles whether or not looping is enabled
        /// </summary>
        public void A_ToggleLooping()
        {
            if (looping == true)
            {
                looping = false;
            }
            else if (looping == false)
            {
                looping = true;
            }
        }

        /// <summary>
        /// This function updates the collision box to match the new position and the new animation
        /// </summary>
        public void UpdateCollisionBox()
        {
            collisionBox.X = (int)position.X;
            collisionBox.Y = (int)position.Y;
            collisionBox.Width = animationList[animationListIndex][frameIndex].Width;
            collisionBox.Height = animationList[animationListIndex][frameIndex].Height;
        }

        /// <summary>
        /// This is the default function for on collision.  Must be overriden
        /// Use the determineEvent.ObjectType to decide how to react with each collision
        /// </summary>
        public virtual void C_OnCollision(GameObject determineEvent)
        {

        }


        
        
    }
}
