using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Genzo
{
    public class Debug
    {
        private static readonly string[] DebugIcons = new string[4] { ">", "!", "¤", "@" }; // log, alert, debug, message
        //private static readonly string[] Syntax = new string[] { "play", "discard", "discardc", "exit" };
        private static byte MessageLength;
        internal void WipeMessage()
        {
            Console.SetCursorPosition(4, 26);
            Console.Write("@");
            for (int i = 0; i < MessageLength + 1; i++)
            {
                Console.Write(" ");
            }
        }
        internal void Logger(string tMessage, byte tMode = 0)
        {
            WipeMessage();
            try
            {
                MessageLength = Convert.ToByte(tMessage.Length);
            }
            catch (Exception Ex)
            {
                File.AppendAllText(@"genzo.log", $"{DateTime.Now} : {Ex.Message}\n");
                MessageLength = 63;
                return;
            }
            Console.SetCursorPosition(4, 26);
            Console.WriteLine($"{DebugIcons[tMode]} {tMessage}");
            Thread.Sleep(1000);
            WipeMessage();
        }
        internal bool Translator(string tLine)
        {
            try
            {
                MessageLength = Convert.ToByte(tLine.Length);
            }
            catch (Exception Ex)
            {
                File.AppendAllText(@"genzo.log", $"{DateTime.Now} : {Ex.Message}\n");
                MessageLength = 63;
                return false;
            }
            //if (!Syntax.Contains(tLine.Trim().Split(' ')[0]))
            //{
            //    Logger("Syntax error", 1);
            //}
            switch (tLine.Split(' ')[0])
            {
                case "play":
                    break;
                case "discard":
                    break;
                case "discardc":
                    break;
                case "exit":
                    Environment.Exit(0); // temporary, will only close debug console
                    break;
                default:
                    Logger("Syntax error", 1);
                    return false;
            }
            return true;
        }
        public Debug()
        {
            MessageLength = 0;
            Logger("Debug module initialized.", 2);
        }
    }
}