using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Genzo
{
    internal class Display
    {
        internal static string[,] DisplayState = new string[29, 120];
        internal static string[] A = new string[8] { "═", "║", "╔", "╗", "╚", "╝", "╠", "╣" };
        internal static void DisplayInit()
        {
            string[] InitRaw = File.ReadAllText(@"init.ui", Encoding.UTF8).Replace("\n", string.Empty).Replace("nd", string.Empty).Split("nl");
            short X = 0; short Y = 0;
            for (short i = 0; i < InitRaw.Length; i++) // specific line command
            {
                for (short ii = 0; ii < Convert.ToInt16(InitRaw[i].Split(":")[0].Replace("h", string.Empty)); ii++, Y++) // how much times to repeat said line
                {
                    X = 0;
                    for (short iii = 0; iii < Convert.ToInt16(InitRaw[i].Split(":")[1].Split(")").Length) - 1; iii++) // how many different charblocks will be there
                    {
                        for (short iiii = 0; iiii < Convert.ToInt16(InitRaw[i].Split(":")[1].Split(")")[iii + 1].Split("(")[0]); iiii++, X++) // how many chars will be in said block
                        {
                            DisplayState[Y, X] = InitRaw[i].Split(":")[1].Split(")")[iii].Split("(")[1].Replace("X", " ");
                        }
                    }
                }
            }
        }
        internal void DrawFrame(byte tState = 0)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            foreach (string n in DisplayState)
            {
                Console.Write($"{n}");
            }
        }
        public Display(string tTitle)
        {
            Console.Title = $"Genzo - {tTitle}";
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            DisplayInit();
            DrawFrame();
        }
    }
}
