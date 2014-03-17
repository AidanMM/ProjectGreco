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

namespace ProjectGreco.GameObjects
{
    class EdgeTile : GameObject
    {

        public EdgeTile(Vector2 startPos) : base(startPos, "EdgeTile")
        {
            position = startPos;
            onScreen = true;
            
        }
        public EdgeTile(Vector2 startPos, List<List<Texture2D>> aList)
            : base(aList, startPos, "EdgeTile")
        {
            position = startPos;
            onScreen = true;
        }

        public override void Update()
        {

        }

    }
}
