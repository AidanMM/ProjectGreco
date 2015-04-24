using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects
{
    public class Pipe : GameObject
    {
        private EndPipe endPipe;
        public bool doOnce = true;
        

        public Pipe(List<List<Texture2D>> aList, Vector2 pos)
            : base(aList, pos, "Pipe")
        {
            velocity.X = -3;
            onScreen = true;
            endPipe = new EndPipe(this);
            
        }
        

        public override void Update()
        {
            if (doOnce == true)
            {
                Game1.OBJECT_HANDLER.AddObjectToHandler("EndPipe", endPipe);
                doOnce = false;
            }
            if ((Game1.OBJECT_HANDLER.currentState as FlappyBird).gameStart == true)
            {
                base.Update();
                
                if (position.X < -184)
                {
                    position.X = 1280;
                    position.Y = Game1.RANDOM.Next(-750, -300);
                    endPipe.Position = new Vector2(position.X, position.Y + + 800 + Game1.RANDOM.Next(200, 300));
                    
                
                }
                
                
            }

        }

       

    }
}
