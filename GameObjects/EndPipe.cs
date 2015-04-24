using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using System.Linq;
using ProjectGreco.Levels;
using ProjectGreco.GameObjects;

namespace ProjectGreco.GameObjects
{
    class EndPipe : GameObject
    {
        public EndPipe(Pipe pipe) : base(pipe.AnimationList, new Vector2(pipe.Position.X, pipe.Position.Y + 1000), "Pipe")
        {
            velocity.X = -3;
            onScreen = true;
        }

        public override void Update()
        {
           if ((Game1.OBJECT_HANDLER.currentState as FlappyBird).gameStart == true)
            {
                
                base.Update(); 
                
            }
        }
    }
}
