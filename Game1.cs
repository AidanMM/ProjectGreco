#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.GameObjects;
using ProjectGreco.Levels;
#endregion

namespace ProjectGreco
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// The overarching object handler for the game. Manages all game objects
        /// </summary>
        public static ObjectHandler OBJECT_HANDLER = new ObjectHandler();

        /// <summary>
        /// This is a safety texture so that all game objects have at least one texture
        /// </summary>
        public static Texture2D DEFAULT_TEXTURE;

        /// <summary>
        /// Conatains all basic textures in the game. All identified using the name of the image
        /// </summary>
        public static Dictionary<string,Texture2D> IMAGE_DICTIONARY;

        /// <summary>
        /// The animation dictionary to access any of the animations that have been added to the game
        /// </summary>
        public static Dictionary<string, List<Texture2D>> ANIMATION_DICTIONARY;

        /// <summary>
        /// This is the displacement of the camera (= displacement of character)
        /// </summary>
        public static Vector2 CAMERA_DISPLACEMENT;

        public static KeyboardState KBState = new KeyboardState();

        public static KeyboardState oldKBstate = new KeyboardState();

        public static Random RANDOM = new Random();


        /// <summary>
        /// This basic effect is for all primitives
        /// </summary>
        public static BasicEffect basicEffect;
        

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;

            // TODO: Add your initialization logic here
            IMAGE_DICTIONARY = new Dictionary<string, Texture2D>();
            ANIMATION_DICTIONARY = new Dictionary<string, List<Texture2D>>();
            CAMERA_DISPLACEMENT = new Vector2(0, 0);

            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, graphics.GraphicsDevice.Viewport.Width,     // left, right
                graphics.GraphicsDevice.Viewport.Height, 0,    // bottom, top
                0, 1);                                         // near, far plane


            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            

            #region LoadTextures

            DEFAULT_TEXTURE = Content.Load<Texture2D>("BlueBeat");
            IMAGE_DICTIONARY.Add("BlueBeat", DEFAULT_TEXTURE);
            IMAGE_DICTIONARY.Add("defaultEdge", Content.Load<Texture2D>("defaultEdge"));
            IMAGE_DICTIONARY.Add("dirtEdge", Content.Load<Texture2D>("dirtEdge"));
            IMAGE_DICTIONARY.Add("caveEdge", Content.Load<Texture2D>("caveEdge"));
            #endregion



            #region LoadAnimations
            ANIMATION_DICTIONARY.Add("Test", A_CreateAnimation("BlueBeat", "RedBeat", "RedBeat", "BlueBeat", "BlueBeatsafaw"));
            ANIMATION_DICTIONARY.Add("PlayerTest", A_CreateAnimation("THEHERO"));
            ANIMATION_DICTIONARY.Add("dirtBlock", A_CreateAnimation("dirtBlock"));
            ANIMATION_DICTIONARY.Add("grassBlock", A_CreateAnimation("grassBlock"));
            ANIMATION_DICTIONARY.Add("caveFillerBlock", A_CreateAnimation("caveFillerBlock"));
            ANIMATION_DICTIONARY.Add("caveFloorBlock", A_CreateAnimation("caveFloorBlock"));
            
            #region Frappy(Just Ignore this)
            ANIMATION_DICTIONARY.Add("Frappy", A_CreateAnimation("Frappy"));
            ANIMATION_DICTIONARY.Add("Pipe", A_CreateAnimation("Pipe"));
            ANIMATION_DICTIONARY.Add("Arrow", A_CreateAnimation("arrow"));

            #endregion
            #endregion

            OBJECT_HANDLER.ChangeState(new Level1());

            

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KBState = Keyboard.GetState();


            OBJECT_HANDLER.Update();

            oldKBstate = KBState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();


            //This line of code is for primitives
            basicEffect.CurrentTechnique.Passes[0].Apply();
            OBJECT_HANDLER.Draw(spriteBatch);

            


            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Creates textures out of the handed in strings, and then takes those textures and stores them as an animation list.  The handed in strings are also added to the static list of textures
        /// </summary>
        /// <param name="numbers">Any number of strings for pngs. If one of them does not exist, it will not be added to the list</param>
        /// <returns>A list with all of the textures added in the paramaters if they existed</returns>
        protected List<Texture2D> A_CreateAnimation(params string[] textures)
        {
            List<Texture2D> returnList = new List<Texture2D>();
            Texture2D temp = DEFAULT_TEXTURE;
            for (int i = 0; i < textures.Length; i++)
            {
                
                    //Try and see if the image has already been loaded in to the project
                    try
                    {
                        temp = IMAGE_DICTIONARY[textures[i]];
                        returnList.Add(temp);
                    }
                    //If it hasn't been. Try adding it to the dictionary
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        //try to add it to the image dictionary if it does not exist
                        try
                        {
                            
                            temp = Content.Load<Texture2D>(textures[i]);
                            IMAGE_DICTIONARY.Add(textures[i], temp);
                            returnList.Add(temp);
                        }
                        catch (Exception e2)
                        {
                            Console.WriteLine(e.Message + "Name does not lead correspond to a texture.");
                        }
                    }  
            }

            return returnList;
        }

        public static List<List<Texture2D>> A_CreateListOfAnimations(params List<Texture2D>[] animationsArray )
        {
            List<List<Texture2D>> animationsList = new List<List<Texture2D>>();

            for (int i = 0; i < animationsArray.Length; i++)
            {
                if (animationsArray[i] != null)
                {
                    animationsList.Add(animationsArray[i]);
                        
                }
            }
            return animationsList;
        }
    
    }
}
