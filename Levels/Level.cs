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
using ProjectGreco.GameObjects;


namespace ProjectGreco.Levels
{
    enum LevelName
    {
        Forest,
        Hills,
        Desert,
        Ice
    }

    /// <summary>
    /// Creates a level when information is put in.
    /// </summary>
    class Level : BaseState
    {
        private bool renderBackground;
        private LevelName levelType;

        public bool RenderBackground { get { return renderBackground; } }
        public LevelName LevelType { get { return levelType; } }

        private string mainTexture;
        private string secondaryTexture;
        private Texture2D mainEdge;
        private Texture2D secondaryEdge;

        public string MainTexture { get { return mainTexture; } }
        public string SecondaryTexture { get { return secondaryTexture; } }
        public Texture2D MainEdge { get { return mainEdge; } }
        public Texture2D SecondaryEdge { get { return secondaryEdge; } }

        private Map myMap;
        private Spawner mySpawner;

        public Map MyMap { get { return myMap; } }
        public Spawner MySpawner { get { return mySpawner; } }

        /// <summary>
        /// Creates a level based on the leveltype supplied
        /// </summary>
        /// <param name="levelType">The LevelType you want to create</param>
        /// <param name="renderBackground">Bool that determines whether or not we create background blocks</param>
        public Level(LevelName levelType, bool renderBackground = true) : base()
        {
            this.renderBackground = renderBackground;
            this.levelType = levelType;

            ChooseTextures();
            CreateMap();
            SetupLevel();
        }

        /// <summary>
        /// Chooses which textures are to be used in the creation of blocks.
        ///         // NEEDS TEXTURES TO BE IMPLEMENTED
        /// </summary>
        public void ChooseTextures()
        {
            switch(levelType)
            {
                case LevelName.Desert:
                    this.mainTexture = "desertFiller";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    break;

                case LevelName.Forest:
                    this.mainTexture = "forestDirtBlock";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    break;

                case LevelName.Hills:
                    this.mainTexture = "dirtBlock";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    break;

                case LevelName.Ice:
                    this.mainTexture = "iceFiller";
                    this.secondaryTexture = "caveFillerBlock";
                    this.mainEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    this.secondaryEdge = Game1.IMAGE_DICTIONARY["defaultEdge"];
                    break;
            }
        }

        /// <summary>
        /// Creates the map based on the level type chosen.
        ///         // NEEDS ALGORITHMS TO BE IMPLEMENTED
        /// </summary>
        public void CreateMap()
        {
            switch(levelType)
            {
                case LevelName.Desert:
                    myMap = new Map(AlgorithmType.Desert);
                    break;
                case LevelName.Forest:
                    myMap = new Map(AlgorithmType.Desert);
                    break;
                case LevelName.Hills:
                    myMap = new Map(AlgorithmType.HillsDesert);
                    break;
                case LevelName.Ice:
                    myMap = new Map(AlgorithmType.HillsDesert);
                    break;
            }
            
        }

        /// <summary>
        /// Creates the level based on the information supplied.
        /// </summary>
        public void SetupLevel()
        {
            // Create the player
            AddObjectToHandler("Player", new Player(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["WalkRight"], Game1.ANIMATION_DICTIONARY["WalkLeft"])));
            // Create the cursor
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.IMAGE_DICTIONARY["cursor"]));

            mySpawner = new Spawner(myMap.Terrain, -1);

            int edgeTiles = 0;
            int backgroundTiles = 0;

            for (int x = 0; x < LevelVariables.WIDTH; x++)
            {
                for (int y = 0; y < LevelVariables.HEIGHT; y++)
                {
                    // Main Edge Tiles
                    if (myMap.Terrain[x][y] == 'E')
                    {
                        AddObjectToHandler("EdgeTile", new EdgeTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            mainEdge, myMap.Terrain,
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainTexture])), edgeTiles);
                        edgeTiles++;
                    }
                    // Secondary Edge Tiles
                    if (myMap.Terrain[x][y] == 'M')
                    {
                        AddObjectToHandler("EdgeTile", new EdgeTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            secondaryEdge, myMap.Terrain,
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryTexture])), edgeTiles);
                        edgeTiles++;
                    }

                    if (renderBackground)
                    {
                        // Main Background Tiles
                        if (myMap.Terrain[x][y] == 'O')
                        {
                            AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                                Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainTexture])), backgroundTiles);
                            backgroundTiles++;
                        }
                        // Secondary Background Tiles
                        if (myMap.Terrain[x][y] == 'C')
                        {
                            AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                                Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryTexture])), backgroundTiles);
                            backgroundTiles++;
                        }
                    }
                }
            }

            // Look at every enemy
            foreach (TemporaryEnemy myEnemy in mySpawner.EnemyData)
            {
                string enemyType = "";
                switch (myEnemy.movementType)
                {
                    case EnemyType.Flying:
                        enemyType = "Frappy";
                        break;
                    case EnemyType.Ghost:
                        enemyType = "cursorTest";
                        break;
                    case EnemyType.Ground:
                        enemyType = "desertFiller";
                        break;
                }

                switch (myEnemy.size)
                {
                    case EnemySize.Large:
                        AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[enemyType]),
                            new Vector2(myEnemy.xPosition * 64, (LevelVariables.HEIGHT - myEnemy.yPosition) * 64)));
                        break;
                    case EnemySize.Medium:
                        AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[enemyType]),
                            new Vector2(myEnemy.xPosition * 64, (LevelVariables.HEIGHT - myEnemy.yPosition) * 64)));
                        break;
                    case EnemySize.Small:
                        AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[enemyType]),
                            new Vector2(myEnemy.xPosition * 64, (LevelVariables.HEIGHT - myEnemy.yPosition) * 64)));
                        break;

                }
            }

            //Sort all of the objects by their zOrder
            SortByZorder();
        }
    }
}
