using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.GameObjects;

namespace ProjectGreco.Levels
{
    class Level1 : BaseState
    {
        public Level1()
            : base()
        {
            AddObjectToHandler("Player", new Player(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["WalkRight"], Game1.ANIMATION_DICTIONARY["WalkLeft"])));
            AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Test"]), new Vector2(100, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64)));
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.IMAGE_DICTIONARY["cursor"]));

            Map myMap = new Map(AlgorithmType.Desert);

            int edgeTiles = 0;
            int backgroundTiles = 0;

            string mainTexture = "dirtBlock";
            string secondaryTexture = "caveFillerBlock";
            Texture2D mainEdge = Game1.IMAGE_DICTIONARY["dirtEdge"];
            Texture2D secondaryEdge = Game1.IMAGE_DICTIONARY["caveEdge"];

            Spawner mySpawner = new Spawner(myMap.Terrain, -1);


            for (int x = 0; x < LevelVariables.WIDTH; x ++)
            {
                for (int y = 0; y < LevelVariables.HEIGHT; y ++)
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

                    //// Main Background Tiles
                    //if (myMap.Terrain[x][y] == 'O')
                    //{
                    //    AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                    //        Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainTexture])), backgroundTiles);
                    //    backgroundTiles++;
                    //}
                    //// Secondary Background Tiles
                    //if (myMap.Terrain[x][y] == 'C')
                    //{
                    //    AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                    //        Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryTexture])), backgroundTiles);
                    //    backgroundTiles++;
                    //}


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
