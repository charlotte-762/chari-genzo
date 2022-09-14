using System;
using System.IO;
using System.Text;

namespace Genzo
{
    internal class Display
    {
        internal static string[] A = new string[8] { "═", "║", "╔", "╗", "╚", "╝", "╠", "╣" };
        internal void DrawFrame(byte tState = 0)
        {
            FileStream Stream = new FileStream(@"init.ui", FileMode.Open);
            StreamReader Reader = new StreamReader(Stream, Encoding.UTF8);
            Console.SetCursorPosition(0, 0);
            while (!Reader.EndOfStream)
            {
                string Line = Reader.ReadLine();
                for (int i = 0; i < 8; i++)
                {
                    Line = Line.Replace(i.ToString(), A[i]).Replace("X", " ");
                }
                Console.Write(Line);
            }
            Reader.Close();
            Stream.Close();
        }
        public Display(string tTitle)
        {
            Console.Title = $"Genzo - {tTitle}";
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            DrawFrame();
        }
    }
}
