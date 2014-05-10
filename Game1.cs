#region Using Statements
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
#endregion

namespace ProjectGreco
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        /// <summary>
        /// Set this to false if you don't want background tiles to be rendering for some reason.
        /// </summary>
        public const bool RENDER_BACKGROUNDS = true;

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

        /// <summary>
        /// The current keyboard state used by the game
        /// </summary>
        public static KeyboardState KBState = new KeyboardState();

        /// <summary>
        /// The keyboard state that existed one frame before the current keyboard state
        /// </summary>
        public static KeyboardState oldKBstate = new KeyboardState();

        /// <summary>
        /// The random object to be used by the entire game. This is to prevent multiple instances of random being created
        /// at the same time
        /// </summary>
        public static Random RANDOM = new Random();

        public SpriteFont DEFUALT_SPRITEFONT;

        public static int TIMER;


        /// <summary>
        /// This basic effect is for all primitives
        /// </summary>
        public static BasicEffect basicEffect;

        /// <summary>
        /// Hides, or shows the debug mode command string
        /// </summary>
        public bool debugMode;

        /// <summary>
        /// The debug command prompt for the game
        /// </summary>
        private CommandInput debugPrompt;

        /// <summary>
        /// This bool will toggle whether or not the object handler updates all of its objects
        /// </summary>
        public static bool pauseObjectUpdate;

        public static string TITLE_STRING = "Project Greco";

        /// <summary>
        /// The mouse state for the game to use
        /// </summary>
        public static MouseState mouseState;

        /// <summary>
        /// The previous mouse state of the last frame
        /// </summary>
        public static MouseState prevMouseState;

        /// <summary>
        /// The song library for the game, must load in mp3 files
        /// </summary>
        public static Dictionary<string, Song> SONG_LIBRARY;

        /// <summary>
        /// The sound effect library for the game.  This uses wav files
        /// </summary>
        public static Dictionary<string, SoundEffect> SOUND_LIBRARY;

        public static DialougeBox DIALOUGE_BOX;
        

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Project Greco";

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

            IsMouseVisible = true;

            debugMode = false;

            pauseObjectUpdate = false;

            debugPrompt = new CommandInput();

            TIMER = 0;

            SONG_LIBRARY = new Dictionary<string, Song>();

            SOUND_LIBRARY = new Dictionary<string, SoundEffect>();

            DIALOUGE_BOX = new DialougeBox();
            

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

            DEFUALT_SPRITEFONT = Content.Load<SpriteFont>("TempGameFont");
            

            #region LoadTextures

            DEFAULT_TEXTURE = Content.Load<Texture2D>("BlueBeat");
            IMAGE_DICTIONARY.Add("BlueBeat", DEFAULT_TEXTURE);
            IMAGE_DICTIONARY.Add("defaultEdge", Content.Load<Texture2D>("defaultEdge"));
            IMAGE_DICTIONARY.Add("dirtEdge", Content.Load<Texture2D>("dirtEdge"));
            IMAGE_DICTIONARY.Add("caveEdge", Content.Load<Texture2D>("caveEdge"));
            IMAGE_DICTIONARY.Add("cursor", Content.Load<Texture2D>("cursor"));
            IMAGE_DICTIONARY.Add("castleblock", Content.Load<Texture2D>("castleblock"));
            IMAGE_DICTIONARY.Add("caveDirtBlock", Content.Load<Texture2D>("caveDirtBlock"));
            IMAGE_DICTIONARY.Add("caveWall", Content.Load<Texture2D>("caveWall"));
            IMAGE_DICTIONARY.Add("desertFloor", Content.Load<Texture2D>("desertFloor"));
            IMAGE_DICTIONARY.Add("floodedcaveFloorBlock", Content.Load<Texture2D>("floodedcaveFloorBlock"));
            IMAGE_DICTIONARY.Add("hillsBlock", Content.Load<Texture2D>("hillsBlock"));
            IMAGE_DICTIONARY.Add("iceFloor", Content.Load<Texture2D>("iceFloor"));
            IMAGE_DICTIONARY.Add("iceEdge", Content.Load<Texture2D>("iceEdge"));
            IMAGE_DICTIONARY.Add("moonBlock", Content.Load<Texture2D>("moonBlock"));
            IMAGE_DICTIONARY.Add("forestEdge", Content.Load<Texture2D>("forestEdge"));
            IMAGE_DICTIONARY.Add("desertEdge", Content.Load<Texture2D>("desertEdge"));


            #endregion



            #region LoadAnimations
            ANIMATION_DICTIONARY.Add("Test", A_CreateAnimation("BlueBeat", "RedBeat", "RedBeat", "BlueBeat", "BlueBeatsafaw"));
            ANIMATION_DICTIONARY.Add("PlayerTest", A_CreateAnimation("THEHERO"));
            ANIMATION_DICTIONARY.Add("dirtBlock", A_CreateAnimation("dirtBlock"));
            ANIMATION_DICTIONARY.Add("grassBlock", A_CreateAnimation("grassBlock"));
            ANIMATION_DICTIONARY.Add("caveFillerBlock", A_CreateAnimation("caveFillerBlock"));
            ANIMATION_DICTIONARY.Add("caveFloorBlock", A_CreateAnimation("caveFloorBlock"));
            ANIMATION_DICTIONARY.Add("cursorTest", A_CreateAnimation("cursor"));
            ANIMATION_DICTIONARY.Add("caveDirtBlock", A_CreateAnimation("caveDirtBlock"));
            ANIMATION_DICTIONARY.Add("caveWall", A_CreateAnimation("caveWall"));
            ANIMATION_DICTIONARY.Add("desertFiller", A_CreateAnimation("desertFiller"));
            ANIMATION_DICTIONARY.Add("floodedcaveFillerBlock", A_CreateAnimation("floodedcaveFillerBlock"));
            ANIMATION_DICTIONARY.Add("forestDirtBlock", A_CreateAnimation("forestDirtBlock"));
            ANIMATION_DICTIONARY.Add("iceFiller", A_CreateAnimation("iceFiller"));
            ANIMATION_DICTIONARY.Add("moonFiller", A_CreateAnimation("moonFiller"));
            ANIMATION_DICTIONARY.Add("tempGround", A_CreateAnimation("TempGround"));
            ANIMATION_DICTIONARY.Add("Onion", A_CreateAnimation("Entities\\Onion"));
            ANIMATION_DICTIONARY.Add("FlyingEnemy", A_CreateAnimation("Entities\\FlyingEnemy"));
            ANIMATION_DICTIONARY.Add("GhostEnemy", A_CreateAnimation("Entities\\GhostEnemy"));
            ANIMATION_DICTIONARY.Add("GhostEnemyLarge", A_CreateAnimation("Entities\\GhostEnemyLarge"));
            ANIMATION_DICTIONARY.Add("GhostEnemySmall", A_CreateAnimation("Entities\\GhostEnemySmall"));
            ANIMATION_DICTIONARY.Add("GroundEnemyLarge", A_CreateAnimation("Entities\\GroundEnemyLarge"));
            ANIMATION_DICTIONARY.Add("GroundEnemySmall", A_CreateAnimation("Entities\\GroundEnemySmall"));
            ANIMATION_DICTIONARY.Add("FlyingEnemyLarge", A_CreateAnimation("Entities\\FlyingEnemyLarge"));
            ANIMATION_DICTIONARY.Add("FlyingEnemySmall", A_CreateAnimation("Entities\\FlyingEnemySmall"));
            ANIMATION_DICTIONARY.Add("homeWorld", A_CreateAnimation("homeworld"));

            // Boss Animations
            ANIMATION_DICTIONARY.Add("BossMain", A_CreateAnimation("Entities\\BossMain"));
            ANIMATION_DICTIONARY.Add("BossLeftHand", A_CreateAnimation("Entities\\BossLeftHand"));
            ANIMATION_DICTIONARY.Add("BossRightHand", A_CreateAnimation("Entities\\BossRightHand"));

            //Character Animations
            ANIMATION_DICTIONARY.Add("WalkRight", A_CreateAnimation("Entities\\PaladinWalking1", "Entities\\PaladinWalking2", "Entities\\PaladinWalking3", "Entities\\PaladinWalking4", "Entities\\PaladinWalking5",
                "Entities\\PaladinWalking6", "Entities\\PaladinWalking7", "Entities\\PaladinWalking8"));
            ANIMATION_DICTIONARY.Add("WalkLeft", A_CreateAnimation("Entities\\PaladinWalking1L", "Entities\\PaladinWalking2L", "Entities\\PaladinWalking3L", "Entities\\PaladinWalking4L", "Entities\\PaladinWalking5L",
                "Entities\\PaladinWalking6L", "Entities\\PaladinWalking7L", "Entities\\PaladinWalking8L"));

            // New Character Animation
            ANIMATION_DICTIONARY.Add("PlayerAnimation", A_CreateAnimation("Entities\\playerSprite0", "Entities\\playerSprite1", "Entities\\playerSprite2",
                "Entities\\playerSprite3", "Entities\\playerSprite4", "Entities\\playerSprite5", "Entities\\playerSprite6", "Entities\\playerSprite7"));

            //Button States
            ANIMATION_DICTIONARY.Add("ButtonStates", A_CreateAnimation("buttonstatic", "buttonover", "buttondown"));

            ANIMATION_DICTIONARY.Add("Swords", A_CreateAnimation("TempSword"));

            ANIMATION_DICTIONARY.Add("OneWayBlock", A_CreateAnimation("TempOneWay"));

            ANIMATION_DICTIONARY.Add("LightWallBlock", A_CreateAnimation("TempVertical"));

            ANIMATION_DICTIONARY.Add("SkillBox", A_CreateAnimation("SkillBox\\CR","SkillBox\\CRay","SkillBox\\E","SkillBox\\Ghost","SkillBox\\LJ","SkillBox\\LW","SkillBox\\SD","SkillBox\\SH","SkillBox\\SP","SkillBox\\Dash","SkillBox\\AM","SkillBox\\AR","SkillBox\\FF","SkillBox\\JH","SkillBox\\JPlus","SkillBox\\S","SkillBox\\W" ));

            // Grass
            ANIMATION_DICTIONARY.Add("Grass", A_CreateAnimation("grasssway0", "grasssway1", "grasssway2", "grasssway3"));


            //Dialouge box
            ANIMATION_DICTIONARY.Add("DialougeBox", A_CreateAnimation("DialougeBox"));

            #region Frappy(Just Ignore this)
            ANIMATION_DICTIONARY.Add("Frappy", A_CreateAnimation("Frappy"));
            ANIMATION_DICTIONARY.Add("Pipe", A_CreateAnimation("Pipe"));
            ANIMATION_DICTIONARY.Add("Arrow", A_CreateAnimation("Entities\\arrow"));

            #endregion
            #endregion


            #region LoadSongs
            SONG_LIBRARY.Add("StartMusic", Content.Load<Song>("Music\\Song1wav.wav"));
            SONG_LIBRARY.Add("HomeWorldMusic", Content.Load<Song>("Music\\Song2wav.wav"));
            
            
            #endregion


            //OBJECT_HANDLER.ChangeState(new Level(LevelName.Desert, RENDER_BACKGROUNDS));
            OBJECT_HANDLER.ChangeState(new HomeWorld());

            

            
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
            mouseState = Mouse.GetState();
            TIMER++;

            if(pauseObjectUpdate == false)
            OBJECT_HANDLER.Update();

            if (debugMode == true)
            {
                debugPrompt.Update();
                this.Window.Title = TITLE_STRING;
            }

            if (KBState.IsKeyDown(Keys.LeftAlt) && KBState.IsKeyDown(Keys.OemTilde) && oldKBstate.IsKeyUp(Keys.OemTilde))
            {
                if (debugMode == true)
                    debugMode = false;
                else
                    debugMode = true;
            }

            DIALOUGE_BOX.Update();

            oldKBstate = KBState;
            prevMouseState = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            spriteBatch.Begin();


            //This line of code is for primitives
            basicEffect.CurrentTechnique.Passes[0].Apply();
            OBJECT_HANDLER.Draw(spriteBatch);

            if (debugMode == true)
            {
                //spriteBatch.DrawString(spFont1, commandString, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(DEFUALT_SPRITEFONT, debugPrompt.commandString, new Vector2(0, 0), Color.Black);
                
            }
            DIALOUGE_BOX.Draw(spriteBatch, DEFUALT_SPRITEFONT, new Vector2(0, 400));

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
