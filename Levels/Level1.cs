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

            AddObjectToHandler("Player", new Player(new Vector2(200, (LevelVariables.HEIGHT - LevelVariables.GROUND_HEIGHT - 3) * 64), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["PlayerTest"])));
          //  AddObjectToHandler("Player", new Player(new Vector2(0,1000), Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["PlayerTest"])));
          //  LevelObjectDictionary["Player"].A_BeginAnimation();
            AddObjectToHandler("Enemy", new BaseEnemy(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Test"]), new Vector2(400, 400)));

            /*
            for (int i = 0; i < 50; i++)
            {
                AddObjectToHandler("Block", new GameObject(new Vector2(500 + i * 51, 600 - i * 20), "Block"), i);
            }
            */
            Map myMap = new Map(AlgorithmType.HillsDesert);

            int edgeTiles = 0;
            int backgroundTiles = 0;

            string mainEdgeTexture = "grassBlock";
            string secondaryEdgeTexture = "caveFloorBlock";
            string mainBackgroundTexture = "dirtBlock";
            string secondaryBackgroundTexture = "caveFillerBlock";
            

            for (int x = 0; x < LevelVariables.WIDTH; x ++)
            {
                for (int y = 0; y < LevelVariables.HEIGHT; y ++)
                {
                    // Main Edge Tiles
                    if (myMap.Terrain[x][y] == 'E')
                    {
                        AddObjectToHandler("EdgeTile", new EdgeTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainEdgeTexture])), edgeTiles); 
                        edgeTiles++;
                    }
                    // Secondary Edge Tiles
                    if (myMap.Terrain[x][y] == 'M')
                    {
                        AddObjectToHandler("EdgeTile", new EdgeTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryEdgeTexture])), edgeTiles); 
                        edgeTiles++;
                    }
                    
                    // Main Background Tiles
                    if (myMap.Terrain[x][y] == 'O')
                    {
                        AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[mainBackgroundTexture])), backgroundTiles); 
                        backgroundTiles++;
                    }
                    // Secondary Background Tiles
                    if (myMap.Terrain[x][y] == 'C')
                    {
                        AddObjectToHandler("BackgroundTile", new BackgroundTile(new Vector2(x * 64, (LevelVariables.HEIGHT - y) * 64),
                            Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY[secondaryBackgroundTexture])), backgroundTiles); 
                        backgroundTiles++;
                    }


                }


            }


        }
    }
}
