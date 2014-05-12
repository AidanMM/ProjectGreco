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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;
using ProjectGreco.GameObjects.Buttons;


namespace ProjectGreco.Levels
{
    class TitleSreen : BaseState
    {

        public TitleSreen()
            : base()
        {
            AddObjectToHandler("StartButton", new StartButton());
            AddObjectToHandler("Cursor", new Cursor(new Vector2(200, 0), Game1.IMAGE_DICTIONARY["cursor"]));
            
        }

    }
}
