using System;
using System.Collections.Generic;

namespace Genzo
{
    internal class Deck
    {
        //private static Debug DeckDebug = new Debug();
        private static readonly string[] DeckIcons = new string[4] { "♠", "♦", "♣", "♥" };
        internal static List<string> DeckCurrent = new List<string>();
        internal static List<string> DeckDiscard = new List<string>();
        private static readonly Random DeckRNG = new Random();
        private static void DeckFill(bool isEmpty = false)
        {
            if (isEmpty)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int ii = 0; ii < 13; ii++)
                    {
                        string Temp = string.Empty;
                        Temp += $"{DeckIcons[i]}{(ii + 1 == 1 ? "A" : ii + 1 > 10 ? ii + 1 > 11 ? ii + 1 > 12 ? "K" : "Q" : "J" : (ii + 1).ToString())}";
                        DeckCurrent.Add(Temp);
                        //DeckDebug.Logger(Temp);
                    }
                }
                return;
            }
            DeckCurrent = DeckDiscard;
        }
        internal void DeckShuffle(byte tMode = 52) // full, top x
        {
            tMode = tMode > 52 ? Convert.ToByte(52) : tMode <= 0 ? Convert.ToByte(52) : tMode;
            if (DeckCurrent.Count == 0)
            {
                DeckFill();
                tMode = 52;
            }
            byte Temp;
            for (int i = 0; i < tMode + 1; i++)
            {
                Temp = Convert.ToByte(DeckRNG.Next(0, DeckCurrent.Count + 1));
                DeckCurrent.Insert(Temp, DeckCurrent[0]);
                DeckCurrent.RemoveAt(0);
            }
        }
        internal string DeckGetCard(bool isTop = true) //first, random
        {
            return DeckCurrent.Count == 0 ? "?0" : isTop ? DeckCurrent[0] : DeckCurrent[DeckRNG.Next(0, DeckCurrent.Count)];
        }

        public Deck()
        {
            DeckFill(true);
        }
    }
}