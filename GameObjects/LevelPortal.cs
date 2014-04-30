using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using ProjectGreco.Levels;

namespace ProjectGreco.GameObjects 
{
	class LevelPortal : GameObject
	{
		/// <summary>
		/// The level that this portal will send the player to
		/// </summary>
		protected Level levelToSend;

		protected Player playerToSend;

		public Player PlayerToSend
		{
			get { return playerToSend; }
		}

		protected bool changeState = false;
		
		public LevelPortal(Vector2 startPos, List<List<Texture2D>> aList, Level goLevel, Player toPass)
            : base(aList, startPos, "LevelPortal")
        {
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

			if (changeState == true)
			{
				Game1.OBJECT_HANDLER.ChangeState(levelToSend);
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
