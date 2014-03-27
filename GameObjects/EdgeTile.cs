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
using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects
{
    class EdgeTile : GameObject
    {
        /// <summary>
        /// Stores location of X position in the array
        /// </summary>
        private int terrainX;
        /// <summary>
        /// Stores location of Y position in the array
        /// </summary>
        private int terrainY;
        /// <summary>
        /// Stores the sprite needed for the edges of the block
        /// </summary>
        private Texture2D edgeSprite;
        /// <summary>
        /// Stores the map of every piece in the terrain.
        /// </summary>
        private char[][] terrain;

        private bool leftAir;
        private bool rightAir;
        private bool upAir;
        private bool downAir;

        public EdgeTile(Vector2 startPos, Texture2D edgeSprite, char[][] terrain) 
            : base(startPos, "EdgeTile")
        {
            position = startPos;
            onScreen = true;
            zOrder = 2;

            // Undo everything done to the index.
            terrainX = (int)(startPos.X / 64);
            terrainY = (int)(LevelVariables.HEIGHT - startPos.Y / 64);

            this.edgeSprite = edgeSprite;
            this.terrain = terrain;

            edgeDetection();
            
        }
        public EdgeTile(Vector2 startPos, Texture2D edgeSprite, char[][] terrain,
            List<List<Texture2D>> aList)
            : base(aList, startPos, "EdgeTile")
        {
            position = startPos;
            onScreen = true;
            zOrder = 2;

            // Undo everything done to the index.
            terrainX = (int)(startPos.X / 64);
            terrainY = (int)(LevelVariables.HEIGHT - startPos.Y / 64);

            this.edgeSprite = edgeSprite;
            this.terrain = terrain;

            edgeDetection();

        }

        public void edgeDetection()
        {
            leftAir = (terrainX - 1 >= 0 && (terrain[terrainX - 1][terrainY] == ' ' || terrain[terrainX - 1][terrainY] == 'C'));
            rightAir = (terrainX + 1 < terrain.Length && (terrain[terrainX + 1][terrainY] == ' ' || terrain[terrainX + 1][terrainY] == 'C'));
            upAir = (terrainY + 1 < terrain[0].Length && (terrain[terrainX][terrainY + 1] == ' ' || terrain[terrainX][terrainY + 1] == 'C'));
            downAir = (terrainY - 1 >= 0 && (terrain[terrainX][terrainY - 1] == ' ' || terrain[terrainX][terrainY - 1] == 'C'));
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationList[animationListIndex][frameIndex], new Rectangle(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X,
                collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, collisionBox.Width, collisionBox.Height),
                new Rectangle(0, 0, collisionBox.Width, collisionBox.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            
            // Now do edge detection.
            // Start by checking the left and right sides.  We want these drawn
            // the furthest back.
            


            // Left side, check for air and then draw
            if (leftAir)
            {
                spriteBatch.Draw(edgeSprite, new Vector2(position.X - 12 - (int)Game1.CAMERA_DISPLACEMENT.X,
                    position.Y - 12 - (int)Game1.CAMERA_DISPLACEMENT.Y),
                    new Rectangle(88, 0, 24, 88), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            // Right side, check for air and then draw with flip
            if (rightAir)
            {
                spriteBatch.Draw(edgeSprite, new Vector2(position.X + 64 - 11 - (int)Game1.CAMERA_DISPLACEMENT.X,
                    position.Y - 12 - (int)Game1.CAMERA_DISPLACEMENT.Y),
                    new Rectangle(88, 0, 24, 88), Color.White, 0, Vector2.Zero, 1, SpriteEffects.FlipHorizontally, 0);
            }

            // Check up and down

            if (upAir)
            {
                spriteBatch.Draw(edgeSprite, new Vector2(position.X - 12 - (int)Game1.CAMERA_DISPLACEMENT.X,
                    position.Y - 12 - (int)Game1.CAMERA_DISPLACEMENT.Y),
                    new Rectangle(0, 0, 88, 24), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            if (downAir)
            {
                spriteBatch.Draw(edgeSprite, new Vector2(position.X - 12 - (int)Game1.CAMERA_DISPLACEMENT.X,
                    position.Y + 64 - 11 - (int)Game1.CAMERA_DISPLACEMENT.Y),
                    new Rectangle(0, 24, 88, 24), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            } 
        }



    }
}
