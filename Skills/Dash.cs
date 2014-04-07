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
using ProjectGreco.Skills;
using ProjectGreco.GameObjects;

namespace ProjectGreco.Skills
{
    enum Direction
    {
        Left,
        Right,
    }

    class Dash
    {

        public Dash(Player myPlayer, Direction myDirection)
        {
            if (myDirection == Direction.Left)
            {
                myPlayer.dashing = true;
                myPlayer.dashTimer++;
                if (myPlayer.dashing == true)
                {
                    myPlayer.Velocity = new Vector2(-myPlayer.dashVelocity, myPlayer.Velocity.Y);
                    myPlayer.Velocity = new Vector2(myPlayer.Velocity.X, 0);
                    myPlayer.Acceleration = new Vector2(0, myPlayer.Acceleration.Y);
                    if (myPlayer.dashTimer > myPlayer.dashTimeLength)
                    {
                        myPlayer.dashTimer = 0;
                        myPlayer.Velocity = new Vector2(0, myPlayer.Velocity.Y);
                        myPlayer.canDash = false;

                    }
                }
            }
            else
            {
                myPlayer.dashing = true;
                myPlayer.dashTimer++;
                if (myPlayer.dashing == true)
                {
                    myPlayer.Velocity = new Vector2(myPlayer.dashVelocity, myPlayer.Velocity.Y);
                    myPlayer.Velocity = new Vector2(myPlayer.Velocity.X, 0);
                    myPlayer.Acceleration = new Vector2(0, myPlayer.Acceleration.Y);
                    if (myPlayer.dashTimer > myPlayer.dashTimeLength)
                    {
                        myPlayer.dashTimer = 0;
                        myPlayer.Velocity = new Vector2(0, myPlayer.Velocity.Y);
                        myPlayer.canDash = false;

                    }
                }
            }
        }


    }
}
