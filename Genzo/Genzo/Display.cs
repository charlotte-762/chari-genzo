using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Genzo
{
    internal class Display // td: save, load, guide with F1, read data from savegame 
    {
        internal static string[,] DisplayState = new string[29, 120];
        //internal static string[] A = new string[8] { "═", "║", "╔", "╗", "╚", "╝", "╠", "╣" };
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
        internal void RefreshLine(byte tLine = 30)
        {
            if (tLine == 30)
            {
                for (int ii = 0; ii < 29; ii++)
                {
                    Console.SetCursorPosition(0, ii);
                    for (int i = 0; i < 120; i++)
                    {
                        //FCS(DisplayState[ii, i]);
                        Console.Write($"{(DisplayState[ii, i].Contains("#") ? DisplayState[ii, i].Split("#")[0] : DisplayState[ii, i])}");
                    }
                }
                return;
            }
            Console.SetCursorPosition(0, tLine);
            for (int i = 0; i < 120; i++)
            {
                _ = FCS(DisplayState[tLine, i]);
                Console.Write($"{(DisplayState[tLine, i].Contains("#") ? DisplayState[tLine, i].Split("#")[0] : DisplayState[tLine, i])}");
            }
        }
        private static bool FCS(string tChar) //Foreground Color Setter
        {
            if (!tChar.Contains("#"))
            {
                return false;
            }
            string Temp = tChar.Split('#')[1];
            switch (Temp)
            {
                case "0":
                    Console.ForegroundColor = ConsoleColor.Black; break;
                case "1":
                    Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case "2":
                    Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case "3":
                    Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case "4":
                    Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case "5":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta; break;
                case "6":
                    Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case "7":
                    Console.ForegroundColor = ConsoleColor.Gray; break;
                case "8":
                    Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case "9":
                    Console.ForegroundColor = ConsoleColor.Blue; break;
                case "A":
                    Console.ForegroundColor = ConsoleColor.Green; break;
                case "B":
                    Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "C":
                    Console.ForegroundColor = ConsoleColor.Red; break;
                case "D":
                    Console.ForegroundColor = ConsoleColor.Magenta; break;
                case "E":
                    Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "F":
                    Console.ForegroundColor = ConsoleColor.White; break;
                default:
                    return false;
            }
            return true;
        } // tbi
        internal void DisplayCaravan(Caravan tCaravan) // name \n cards in order \n value
        {
            byte CLeft = (byte)(tCaravan.CaravanOwner ? 57 - tCaravan.CaravanName.Length : 63), CTop = (byte)((tCaravan.HandId < 3 ? tCaravan.HandId : tCaravan.HandId - 3) * 5 + 3);
            foreach (char n in tCaravan.CaravanName)
            {
                DisplayState[CTop, CLeft++] = n.ToString();
            }
            RefreshLine(CTop);
            CLeft = (byte)(tCaravan.CaravanOwner ? 57 - tCaravan.HandCards.Count * 3 : 63);
            CTop = (byte)((tCaravan.HandId < 3 ? tCaravan.HandId : tCaravan.HandId - 3) * 5 + 4);
            foreach (PlaydCard n in tCaravan.HandCards)
            {
                DisplayState[CTop, CLeft++] = " #F"; DisplayState[CTop, CLeft++] = $"{n.GetCard()[0]}#{(n.GetSuit() == "♠" || n.GetSuit() == "♣" ? "0" : "C")}"; DisplayState[CTop, CLeft++] = n.GetCard()[1].ToString();
            }
            RefreshLine(CTop);
            Console.SetCursorPosition(tCaravan.CaravanOwner ? 57 - tCaravan.GetCaravanValue().ToString().Length : 63, (tCaravan.HandId < 3 ? tCaravan.HandId : tCaravan.HandId - 3) * 5 + 5);
            Console.WriteLine(tCaravan.GetCaravanValue());
        }
        internal void DisplayHand(Hand tHand)
        {
            Console.SetCursorPosition(tHand.HandOwner == 0 ? 51 - tHand.HandCards.Count * 3 : 69, 21);
            Console.Write(tHand.HandOwner == 0 ? " " : "");
            foreach (PlaydCard n in tHand.HandCards)
            {
                Console.Write($"{(tHand.HandOwner == 0 ? n.GetCard() : "?0")} ");
            }
        }
        public Display(string tTitle)
        {
            Console.Title = $"Genzo - {tTitle}";
            Console.WindowWidth = 120;
            Console.WindowHeight = 30;
            Console.CursorVisible = false;
            DisplayInit();
            RefreshLine();
        }
    }
}
