using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

using ProjectGreco.GameObjects;
using ProjectGreco.Levels;


//------------------------------------------------------------------------------ +
//Author: Aidan                                                                 |
//Purpose: Debug command prompt for the game.  Handles key presses and takes    |
//commands to control the game while it is running                              |
//------------------------------------------------------------------------------ +

namespace ProjectGreco
{
    class CommandInput
    {
        /// <summary>
        /// The current index in the commandString
        /// </summary>
        private int stringIndex;
        /// <summary>
        /// The index for which the string index is not allowed to go before
        /// </summary>
        private int noBackLength;
        /// <summary>
        /// The length of the prompt given by the programmer
        /// </summary>
        private int promptLength;
        /// <summary>
        /// The number of lines the current commandString is using
        /// </summary>
        private int numLines;
        /// <summary>
        /// The limit to the number of lines allowed by the command string
        /// </summary>
        private int lineLimit = 15;
        /// <summary>
        /// The prompt for the user to input
        /// </summary>
        private string promptString = "Enter a command>";
        /// <summary>
        /// The list of commands that the user has given in the current session
        /// </summary>
        List<string> prevCommandList = new List<string>();
        /// <summary>
        /// The index within the previous command list
        /// </summary>
        private int prevCommandIndex;
        /// <summary>
        /// Whether or not to display the current index pipe
        /// </summary>
        private bool displayPipe = false;
        /// <summary>
        /// The timer used to manage the index indicator
        /// </summary>
        private int timer = 0;
        /// <summary>
        /// The string that is printed and typed into by the user
        /// </summary>
        public string commandString;

        public CommandInput()
        {
            commandString = promptString;

            stringIndex = commandString.Length;
            promptLength = commandString.Length;
            noBackLength = commandString.Length;
        }
        
        public void Update()
        {
            
            timer++;
            #region KeyTypes
            if (Game1.KBState.IsKeyDown(Keys.A) && !(Game1.oldKBstate.IsKeyDown(Keys.A)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'a';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "a");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.B) && !(Game1.oldKBstate.IsKeyDown(Keys.B)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'b';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "b");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.C) && !(Game1.oldKBstate.IsKeyDown(Keys.C)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'c';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "c");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D) && !(Game1.oldKBstate.IsKeyDown(Keys.D)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'd';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "d");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.E) && !(Game1.oldKBstate.IsKeyDown(Keys.E)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'e';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "e");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.F) && !(Game1.oldKBstate.IsKeyDown(Keys.F)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'f';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "f");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.G) && !(Game1.oldKBstate.IsKeyDown(Keys.G)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'g';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "g");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.H) && !(Game1.oldKBstate.IsKeyDown(Keys.H)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'h';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "h");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.I) && !(Game1.oldKBstate.IsKeyDown(Keys.I)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'i';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "i");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.J) && !(Game1.oldKBstate.IsKeyDown(Keys.J)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'j';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "j");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.K) && !(Game1.oldKBstate.IsKeyDown(Keys.K)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'k';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "k");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.L) && !(Game1.oldKBstate.IsKeyDown(Keys.L)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'l';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "l");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.M) && !(Game1.oldKBstate.IsKeyDown(Keys.M)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'm';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "m");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.N) && !(Game1.oldKBstate.IsKeyDown(Keys.N)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'n';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "n");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.O) && !(Game1.oldKBstate.IsKeyDown(Keys.O)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'o';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "o");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.P) && !(Game1.oldKBstate.IsKeyDown(Keys.P)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'p';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "p");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.Q) && !(Game1.oldKBstate.IsKeyDown(Keys.Q)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'q';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "q");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.R) && !(Game1.oldKBstate.IsKeyDown(Keys.R)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'r';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "r");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.S) && !(Game1.oldKBstate.IsKeyDown(Keys.S)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 's';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "s");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.T) && !(Game1.oldKBstate.IsKeyDown(Keys.T)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 't';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "t");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.U) && !(Game1.oldKBstate.IsKeyDown(Keys.U)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'u';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "u");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.V) && !(Game1.oldKBstate.IsKeyDown(Keys.V)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'v';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "v");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.W) && !(Game1.oldKBstate.IsKeyDown(Keys.W)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'w';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "w");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.X) && !(Game1.oldKBstate.IsKeyDown(Keys.X)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'x';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "x");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.Y) && !(Game1.oldKBstate.IsKeyDown(Keys.Y)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'y';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "y");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.Z) && !(Game1.oldKBstate.IsKeyDown(Keys.Z)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += 'z';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "z");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.Space) && !(Game1.oldKBstate.IsKeyDown(Keys.Space)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += ' ';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, " ");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.OemComma) && !(Game1.oldKBstate.IsKeyDown(Keys.OemComma)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += ',';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, ",");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.OemPeriod) && !(Game1.oldKBstate.IsKeyDown(Keys.OemPeriod)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '.';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, ".");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.OemPlus) && !(Game1.oldKBstate.IsKeyDown(Keys.OemPlus)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '=';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "=");
                    stringIndex++;
                }
            }

            if (Game1.KBState.IsKeyDown(Keys.D0) && !(Game1.oldKBstate.IsKeyDown(Keys.D0)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '0';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "0");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D1) && !(Game1.oldKBstate.IsKeyDown(Keys.D1)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '1';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "1");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D2) && !(Game1.oldKBstate.IsKeyDown(Keys.D2)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '2';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "2");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D3) && !(Game1.oldKBstate.IsKeyDown(Keys.D3)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '3';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "3");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D4) && !(Game1.oldKBstate.IsKeyDown(Keys.D4)))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '4';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "4");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D5) && !Game1.oldKBstate.IsKeyDown(Keys.D5))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '5';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "5");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D6) && !Game1.oldKBstate.IsKeyDown(Keys.D6))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '6';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "6");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D7) && !Game1.oldKBstate.IsKeyDown(Keys.D7))
            {
                
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '7';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "7");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D8) && !Game1.oldKBstate.IsKeyDown(Keys.D8))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '8';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "8");
                    stringIndex++;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.D9) && !Game1.oldKBstate.IsKeyDown(Keys.D9))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '9';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "9");
                    stringIndex++;
                }
            }
            
            if (Game1.KBState.IsKeyDown(Keys.OemMinus) && !Game1.oldKBstate.IsKeyDown(Keys.OemMinus) && Game1.KBState.IsKeyDown(Keys.LeftAlt))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '_';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "_");
                    stringIndex++;
                }
            }
            else if (Game1.KBState.IsKeyDown(Keys.OemMinus) && !Game1.oldKBstate.IsKeyDown(Keys.OemMinus))
            {
                if (stringIndex == commandString.Length)
                {
                    HandleLine();
                    commandString += '-';
                    stringIndex++;

                }
                else
                {
                    HandleLine();
                    timer = 25;
                    commandString = commandString.Insert(stringIndex, "-");
                    stringIndex++;
                }
            }


            #endregion


            if (Game1.KBState.IsKeyDown(Keys.Enter) && !Game1.oldKBstate.IsKeyDown(Keys.Enter))
            {
                HandleLine();
                prevCommandList.Add(commandString.Substring(noBackLength));
                CallCommand(commandString.Substring(noBackLength));
                commandString += "\n";
                numLines++;
                while (numLines > lineLimit)
                {
                    numLines--;
                    string[] promptLines = commandString.Split('\n');
                    int tempLength = promptLines[0].Length + 1;
                    commandString = commandString.Remove(0, tempLength);
                }
                stringIndex++;
                commandString += promptString;
                stringIndex = commandString.Length;
                noBackLength = commandString.Length;
                prevCommandIndex = prevCommandList.Count;

            }

            if (Game1.KBState.IsKeyDown(Keys.Back) && !Game1.oldKBstate.IsKeyDown(Keys.Back))
            {
                HandleLine();
                if (stringIndex > noBackLength)
                {
                    commandString = commandString.Remove(stringIndex - 1, 1);
                    stringIndex--;
                }
            }
            if (Game1.KBState.IsKeyDown(Keys.LeftShift))
            {
                if (commandString.Length > 0)
                {
                    char temp = commandString[commandString.Length - 1];
                    string tempString = "" + temp;
                    tempString = tempString.ToUpper();
                    commandString = commandString.Remove(commandString.Length - 1, 1);
                    commandString += tempString;

                }
            }
            if (Game1.KBState.IsKeyDown(Keys.RightShift))
            {
                if (commandString.Length > 0)
                {
                    char temp = commandString[commandString.Length - 1];
                    string tempString = "" + temp;
                    tempString = tempString.ToLower();
                    commandString = commandString.Remove(commandString.Length - 1, 1);
                    commandString += tempString;

                }
            }
            if (Game1.KBState.IsKeyDown(Keys.Left) && Game1.oldKBstate.IsKeyUp(Keys.Left))
            {
                HandleLine();
                stringIndex--;
                if (stringIndex < noBackLength)
                {
                    stringIndex = noBackLength;
                }
                
            }
            if (Game1.KBState.IsKeyDown(Keys.Right) && Game1.oldKBstate.IsKeyUp(Keys.Right))
            {
                HandleLine();
                stringIndex++;
                if (stringIndex > commandString.Length)
                    stringIndex = commandString.Length;
            }

            if (Game1.KBState.IsKeyDown(Keys.Up) && Game1.oldKBstate.IsKeyUp(Keys.Up))
            {
                if (prevCommandList.Count > 0)
                {
                    HandleLine();
                    prevCommandIndex--;
                    if (prevCommandIndex < 0)
                    {
                        prevCommandIndex = 0;
                    }
                    if (noBackLength != commandString.Length)
                    {
                        try
                        {
                            commandString = commandString.Remove(noBackLength);
                        }
                        catch(Exception e)
                        {
                            stringIndex = commandString.Length;
                        }
                    }
                    commandString += prevCommandList[prevCommandIndex];
                    stringIndex = commandString.Length;

                }
            }
            if (Game1.KBState.IsKeyDown(Keys.Down) && Game1.oldKBstate.IsKeyUp(Keys.Down))
            {
                if (prevCommandList.Count > 0 && prevCommandIndex != prevCommandList.Count)
                {
                    HandleLine();
                    prevCommandIndex++;
                    if (prevCommandIndex >= prevCommandList.Count)
                    {
                        try
                        {
                            prevCommandIndex = prevCommandList.Count;
                            commandString = commandString.Remove(noBackLength);
                            stringIndex = noBackLength;
                        }
                        catch (Exception e)
                        {
                            stringIndex = noBackLength;
                        }
                    }
                    else
                    {
                        if (noBackLength != commandString.Length)
                            commandString = commandString.Remove(noBackLength);
                        commandString += prevCommandList[prevCommandIndex];
                    }
                    

                }
            }


            if (timer == 50)
            {
                displayPipe = true;
                try
                {
                    commandString = commandString.Insert(stringIndex, "|");
                    stringIndex++;
                }
                catch (Exception e)
                {
                    stringIndex = commandString.Length;

                }
            }
            if (timer == 100)
            {
                displayPipe = false;
                commandString = commandString.Remove(stringIndex - 1, 1);
                stringIndex--;
                timer = 0;
            }
        }
        /// <summary>
        /// This function handles the blinking index indicator for the command string. This needs to be called whenever 
        /// the index is goinfg to be altered
        /// </summary>
        public void HandleLine()
        {
            if (displayPipe == true)
            {
                commandString = commandString.Remove(stringIndex - 1, 1);
                stringIndex--;
                displayPipe = false;
            }
            timer = 25;
        }

        /// <summary>
        /// This function is called to make the passed in command by the user happen. These commands are to be programmed here
        /// </summary>
        /// <param name="command"></param>
        public void CallCommand(string command)
        {
            string[] parts = command.Split(' ');
            command = command.ToLower();
            if (command.Contains("line limit"))
            {
                if (command.Contains("="))
                {
                    try
                    {
                        if ((int)ReturnNumberAfterEquals(command) > 2)
                        {
                            lineLimit = (int)ReturnNumberAfterEquals(command);
                            string tempString = "Line limit succesfully changed to " + lineLimit;
                            AddString(tempString);
                        }
                        else
                            AddString("Line limit given was too small.");
                    }
                    catch (Exception e)
                    {
                        AddString("Not a valid input for line limit command.  Line limit must be 'line limit = #'");
                    }
                }
                else
                {
                    AddString("Not a valid input for line limit command.  Line limit must be 'line limit = #'");
                }
            }
            else if (command.Contains("pause"))
            {
                if (Game1.pauseObjectUpdate == false)
                    Game1.pauseObjectUpdate = true;
                else
                    Game1.pauseObjectUpdate = false;

            }
            else if (command.Contains("set "))
            {
                try
                {
                    if (Game1.OBJECT_HANDLER.objectDictionary.ContainsKey(parts[1]))
                    {
                        if (command.Contains("position"))
                        {
                            try
                            {
                                Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position = ReturnNumbersSerperatedByCommas(command);
                                AddString("Done!" + parts[1] + "position has been set to( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position.Y + " )");
                            }
                            catch (Exception e)
                            {
                                AddString("Not a valid input for altering position, the format is 'set nameOfObject position = #,#'");
                            }
                        }
                        else if (command.Contains("velocity"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity = ReturnNumbersSerperatedByCommas(command);
                            AddString("Done!" + parts[1] + "velocity has been set to( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity.Y + " )");
                        }
                        else if (command.Contains("acceleration"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration = ReturnNumbersSerperatedByCommas(command);
                            AddString("Done!" + parts[1] + "acceleration has been set to( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration.Y + " )");
                        }
                        else if (command.Contains("zorder"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].ZOrder = (int)ReturnNumberAfterEquals(command);
                            Game1.OBJECT_HANDLER.SortByZorder();
                            AddString("Done!" + parts[1] + "zOrder has been set to " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].ZOrder);
                        }
                        else if (command.Contains("angle"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Angle = (int)ReturnNumberAfterEquals(command);
                            AddString("Done!" + parts[1] + " angle has been set to " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Angle);
                        }
                        else if (command.Contains("frame"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].A_GoToFrameIndex((int)ReturnNumberAfterEquals(command));
                            AddString("Done!" + parts[1] + "frame has been set" );
                        }
                        else if (command.Contains("animation"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].A_GoToAnimationIndex((int)ReturnNumberAfterEquals(command));
                            AddString("Done!" + parts[1] + " animation has been set ");
                        }
                        else if (command.Contains("scale"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Scale = ReturnNumbersSerperatedByCommas(command);
                            AddString("Done!" + parts[1] + " scale has been set (" + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Scale.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Scale.Y + " )");
                        }
                        else if (command.Contains("jumps"))
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).MaximumJumps = (int)ReturnNumberAfterEquals(command);
                            AddString("Done!");
                        }
                    }
                    else
                    {
                        AddString("Not a valid gameObject, check spelling and capitalization");
                    }
                }
                catch (Exception e)
                {
                    AddString("There was an issue with your input");
                }
            }
            else if (commandString.Contains("mod"))
            {
                try
                {
                    if (Game1.OBJECT_HANDLER.objectDictionary.ContainsKey(parts[1]))
                    {
                        if (command.Contains("position"))
                        {
                            try
                            {
                                Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position += ReturnNumbersSerperatedByCommas(command);
                                AddString("Done!" + parts[1] + "position has been set to( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position.Y + " )");
                            }
                            catch (Exception e)
                            {
                                AddString("Not a valid input for altering position, the format is 'set nameOfObject position = #,#'");
                            }
                        }
                        else if (command.Contains("velocity"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity += ReturnNumbersSerperatedByCommas(command);
                            AddString("Done!" + parts[1] + "velocity has been set to( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity.Y + " )");
                        }
                        else if (command.Contains("acceleration"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration += ReturnNumbersSerperatedByCommas(command);
                            AddString("Done!" + parts[1] + "acceleration has been set to( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration.Y + " )");
                        }
                        else if (command.Contains("zorder"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].ZOrder += (int)ReturnNumberAfterEquals(command);
                            Game1.OBJECT_HANDLER.SortByZorder();
                            AddString("Done!" + parts[1] + "zOrder has been set to " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].ZOrder);
                        }
                    }
                    else
                    {
                        AddString("Not a valid gameObject, check spelling and capitalization");
                    }
                }
                catch (Exception e)
                {
                    AddString("There was an issue with your input");
                }
            }
            else if (commandString.Contains("return"))
            {
                try
                {
                    if (Game1.OBJECT_HANDLER.objectDictionary.ContainsKey(parts[1]))
                    {
                        if (commandString.Contains("position"))
                        {
                            AddString(parts[1] + " position is ( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Position.Y + " )");
                        }
                        if (commandString.Contains("velocity"))
                        {
                            AddString(parts[1] + " velocity is ( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Velocity.Y + " )");
                        }
                        if (commandString.Contains("acceleration"))
                        {
                            AddString(parts[1] + " acceleration is ( " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration.X + " , " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].Acceleration.Y + " )");
                        }
                        if (command.Contains("zorder"))
                        {
                            AddString(parts[1] + "zOrder is " + Game1.OBJECT_HANDLER.objectDictionary[parts[1]].ZOrder);
                        }
                    }
                    else
                    {
                        AddString("Not a valid gameObject, check spelling and capitalization");
                    }
                }
                catch (Exception e)
                {
                    AddString("There was an issue with your input");
                }
            }
            else if (commandString.Contains("change"))
            {
                try
                {
                    if (commandString.Contains("level"))
                    {
                        if (commandString.Contains("level1"))
                        {
                            Game1.OBJECT_HANDLER.ChangeState(new Level(LevelName.Hills, Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player, true));
                            AddString("Level has been changed");
                        }
                        if (command.Contains("home"))
                        {
                            Game1.OBJECT_HANDLER.ChangeState(new HomeWorld());
                            AddString("Level has been changed");
                        }
                        
                    }
                    else
                    {
                        AddString("Not a valid gameObject, check spelling and capitalization");
                    }
                }
                catch (Exception e)
                {
                    AddString("There was an issue with your input");
                }
            }
            
            else if (command.Contains("help"))
            {
                AddString("Set commands for position, velocity, and acceleration, zorder, frame, and animation follows this set up:");
                AddString("set objectName variable = #,#");
                AddString("Mod position, velocity, acceleration and zOrder by a set ammount");
                AddString("mod objectName variable = #,#");
                AddString("Return commands for position, velocity, and acceleration follows this set up:");
                AddString("return objectName variable");
                AddString("pause & unpause will both toggle the game being paused.");
                AddString("You can toggle animating, colliding");
                AddString("toggle gameObject animating");
                AddString("Change the line limit of the debug prompt with: ");
                AddString("line limit = #");
            }
            else if (command.Contains("toggle"))
            {
                try
                {
                    if (command.Contains("chaoticreset"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillChaoticReset == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillChaoticReset = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.ChaoticReset);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillChaoticReset == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.ChaoticReset)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("confuseray"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillConfuseRay == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillConfuseRay = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.ConfuseRay);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillConfuseRay == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.ConfuseRay)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("exile"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.Exile);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.Exile)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("ghost"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillGhost == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.Ghost);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillGhost == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.Ghost)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("lightjump"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillLightJump == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.LightJump);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillLightJump == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.LightJump)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("lightwall"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillLightWall == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.LightWall);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillLightWall == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.LightWall)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("shadowdagger"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillShadowDagger == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.ShadowDagger);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillShadowDagger == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.ShadowDagger)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("shadowhold"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillShadowHold == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.ShadowHold);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillShadowHold == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.ShadowHold)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if (command.Contains("shadowpush"))
                    {
                        if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillShadowPush == false)
                        {
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillExile = true;
                            (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Add(ActionSkills.ShadowPush);
                        }
                        else if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillShadowPush == true)
                        {
                            for (int i = 0; i < (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.Count; i++)
                            {
                                if ((Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills[i] == ActionSkills.ShadowPush)
                                {
                                    (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).availableSkills.RemoveAt(i);
                                    break;
                                }
                            }
                        }
                        AddString("Done");
                        return;
                    }
                    if(command.Contains("dash"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillDash = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillDash;
                        AddString("Done!");
                    }
                    if (command.Contains("fastfall"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillFastFall = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillFastFall;
                        AddString("Done!");
                    }
                    if (command.Contains("jumpheight"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillJumpHeight = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillJumpHeight;
                        AddString("Done!");
                    }
                    if (command.Contains("meleeair"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillMeleeAir = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillMeleeAir;
                        AddString("Done!");
                    }
                    if (command.Contains("rangedair"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillRangedAir = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillRangedAir;
                        AddString("Done!");
                    }
                    if (command.Contains("speed"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillSpeed = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillSpeed;
                        AddString("Done!");
                    }
                    if (command.Contains("wings"))
                    {
                        (Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillWings = !(Game1.OBJECT_HANDLER.objectDictionary["Player"] as Player).SkillWings;
                        AddString("Done!");
                    }

                    
                    if (Game1.OBJECT_HANDLER.objectDictionary.ContainsKey(parts[1]))
                    {
                        if (command.Contains("animating"))
                        {
                            Game1.OBJECT_HANDLER.objectDictionary[parts[1]].A_ToggleAnimating();
                        }
                        
                        if (command.Contains("colliding"))
                        {
                            if (Game1.OBJECT_HANDLER.objectDictionary[parts[1]].CheckForCollisions == true)
                                Game1.OBJECT_HANDLER.objectDictionary[parts[1]].CheckForCollisions = false;
                            else
                                Game1.OBJECT_HANDLER.objectDictionary[parts[1]].CheckForCollisions = true;
                        }
                        else
                        {
                            AddString("Not a valid toggle input");
                        }
                    }
                    

                }
                catch (Exception e)
                {
                    AddString("There was an issue with your input");
                }
            }
            else
            {
                AddString("Not a valid command.");
            }
        }

        /// <summary>
        /// This function adds the given string to the command string as a new line
        /// </summary>
        /// <param name="toAdd">the new line to add to the command string</param>
        public void AddString(string toAdd)
        {
            commandString += "\n" + toAdd;
            numLines++;
        }
        /// <summary>
        /// Returns the number in a string that appears after an equal sign
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public double ReturnNumberAfterEquals(string command)
        {
            string[] temp = command.Substring(command.IndexOf('=')).Split(' ');
            double toReturn = System.Convert.ToDouble(temp[1]);
            return toReturn;
        }

        /// <summary>
        /// Returns the two numbers in a string after an equal sign sepereated by commas
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public Vector2 ReturnNumbersSerperatedByCommas(string command)
        {
            string[] temp = command.Substring(command.IndexOf('=')).Split(' ');
            string[] values = temp[1].Split(',');
            Vector2 toReturn = new Vector2((float)System.Convert.ToDouble(values[0]), (float)System.Convert.ToDouble(values[1]));
            return toReturn;
        }
        
    }
}
