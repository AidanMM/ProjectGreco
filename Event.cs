using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using ProjectGreco.GameObjects;
using ProjectGreco.Levels;

namespace ProjectGreco
{
    public class Event
    {
        public bool Happened = false;

        public int index = 0;

        public Event(int indexOfDialouge)
        {
            index = indexOfDialouge;
        }

        public virtual bool Update()
        {

            return false;
        }

    }
}
