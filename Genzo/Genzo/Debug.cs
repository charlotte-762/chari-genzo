using System;
using System.IO;

namespace Genzo
{
    public class Debug
    {
        private static readonly string[] DebugIcons = new string[4] { ">", "!", "¤", "@" }; // log, alert, debug, message
        private static byte MessageLength;
        internal void Logger(string tMessage, byte tMode = 0)
        {
            Console.SetCursorPosition(4, 26);
            for (int i = 0; i < MessageLength + 2; i++)
            {
                Console.Write(" ");
            }
            try
            {
                MessageLength = Convert.ToByte(tMessage.Length);
            }
            catch (Exception Ex)
            {
                File.AppendAllText(@"genzo.log", $"{DateTime.Today} : {Ex.Message}\n");
                MessageLength = 63;
                return;
            }
            Console.SetCursorPosition(4, 26);
            Console.WriteLine($"{DebugIcons[tMode]} {tMessage}");
        }
        public Debug()
        {
            MessageLength = 0;
            Logger("Debug module initialized.", 2);
        }
    }
}