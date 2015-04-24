using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;


namespace ProjectGreco.GameObjects
{
    class BackgroundTile : GameObject
    {
        public BackgroundTile(Vector2 startPos) : base(startPos, "BackgroundTile")
        {
            position = startPos;
            onScreen = false;
            zOrder = -2;
            
        }
        public BackgroundTile(Vector2 startPos, List<List<Texture2D>> aList)
            : base(aList, startPos, "BackgroundTile")
        {
            position = startPos;
            onScreen =false;
            zOrder = -2;
        }

        public override void Update()
        {

            OnScreenCheck();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationList[animationListIndex][frameIndex], new Rectangle(collisionBox.X - (int)Game1.CAMERA_DISPLACEMENT.X,
                collisionBox.Y - (int)Game1.CAMERA_DISPLACEMENT.Y, collisionBox.Width, collisionBox.Height),
                new Rectangle(0, 0, collisionBox.Width, collisionBox.Height), Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

    }
}
