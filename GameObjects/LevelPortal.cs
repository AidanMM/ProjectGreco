using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;

using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects 
{
	class LevelPortal : GameObject
	{
		/// <summary>
		/// The level that this portal will send the player to
		/// </summary>
		protected BaseState levelToSend;

		protected Player playerToSend;


		public Player PlayerToSend
		{
			get { return playerToSend; }
		}

		protected bool changeState = false;

        protected bool closed = false;

        public bool Closed
        {
            get { return closed; }
            set { closed = value; }
        }
		
		public LevelPortal(Vector2 startPos, List<List<Texture2D>> aList, BaseState goLevel, Player toPass)
            : base(aList, startPos, "LevelPortal")
        {
            zOrder = 5;
            checkForCollisions = true;
            animationList = aList;
            position = startPos;
            onScreen = true;
            zOrder = -5;
			levelToSend = goLevel;
			playerToSend = toPass;
        }

		public override void Update()
		{
			base.Update();

			if (changeState == true && closed == false)
			{
                
				//Check to see if the level being sent to is an platforming stage
                if (levelToSend is Level)
                {
                    (levelToSend as Level).PositionPlayer();
                    MediaPlayer.Play(Game1.SONG_LIBRARY["StartMusic"]);
                }
                //Check to see if the level being sent to is the homeworld stage
                if (levelToSend is HomeWorld)
                {
                    Game1.OBJECT_HANDLER.objectDictionary["Player"].Position = new Vector2(0, 200);
                    Game1.OBJECT_HANDLER.objectDictionary["Player"].Velocity = new Vector2(0, 0);
                    MediaPlayer.Play(Game1.SONG_LIBRARY["HomeWorldMusic"]);
                }
                Game1.OBJECT_HANDLER.ChangeState(levelToSend);
                MediaPlayer.Volume = .2f;
                
			}
		}

		public override void C_OnCollision(GameObject determineEvent)
		{
			if (Game1.KBState.IsKeyDown(Keys.W) && determineEvent.ObjectType == "Player")
			{
				changeState = true;
			}
		}
	}
}
