using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using ProjectGreco.Levels;
using ProjectGreco.Skills;


namespace ProjectGreco.GameObjects
{
    class Sword : GameObject
    {
        Player player;
        int timer = 0;
        bool left;
        
        public Sword(Player playerToPass)
            : base(Game1.A_CreateListOfAnimations(Game1.ANIMATION_DICTIONARY["Swords"]), playerToPass.Position, "Sword")
        {
            player = playerToPass;
            Game1.OBJECT_HANDLER.currentState.AddObjectToHandler("Sword", this);
            left = player.HFlip;
            if (left == false)
            {
                angle = -(float)Math.PI / 2;
            }
            else
                angle = (float)Math.PI / 2;

            UpdateCollisionBox();

            
        }

        public override void Update()
        {
            base.Update();
            
            timer++;

            if (timer > 20)
            {
                destroyThis = true;
            }
            

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (left == false)
            {
                angle += .12f;
                spriteBatch.Draw(animationList[animationListIndex][frameIndex],
                    new Vector2(player.Position.X - (int)Game1.CAMERA_DISPLACEMENT.X + Width / 2, player.Position.Y - (int)Game1.CAMERA_DISPLACEMENT.Y + Height / 2),
                    new Rectangle(0, 0, collisionBox.Width, collisionBox.Height),
                    Color.White,
                    angle,
                    new Vector2(0, Height / 2),
                    scale,
                    SpriteEffects.None,
                    1);
            }
            else if (left == true)
            {
                angle -= .12f;
                spriteBatch.Draw(animationList[animationListIndex][frameIndex],
                    new Vector2(player.Position.X - (int)Game1.CAMERA_DISPLACEMENT.X + Width / 2, player.Position.Y - (int)Game1.CAMERA_DISPLACEMENT.Y + Height / 2),
                    new Rectangle(0, 0, collisionBox.Width, collisionBox.Height),
                    Color.White,
                    angle,
                    new Vector2(Width, Height / 2),
                    scale,
                    SpriteEffects.FlipHorizontally,
                    1);
            }
        }

        
    }
}
