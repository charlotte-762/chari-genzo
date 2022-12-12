using System;
using System.Collections.Generic;
using System.Linq;

namespace Genzo
{
    internal class Deck
    {
        //private static Debug DeckDebug = new Debug();
        private static readonly string[] DeckIcons = new string[4] { "♠", "♦", "♣", "♥" };
        private List<Card> DeckCurrent = new List<Card>();
        private List<Card> DeckDiscard = new List<Card>();
        private static readonly Random DeckRNG = new Random();
        private void DeckFill(bool isEmpty = false)
        {
            if (isEmpty)
            {
                for (byte i = 0; i < 4; i++)
                {
                    for (byte ii = 0; ii < 13; ii++)
                    {
                        DeckCurrent.Add(new Card((byte)(ii + 1), DeckIcons[i]));
                        //foreach (Card n in DeckCurrent)
                        //{
                        //    Console.WriteLine($"{n.GetCard()}");
                        //}
                        //Console.WriteLine($"{DeckCurrent.Last().GetCard()}");
                        //DeckDebug.Logger(DeckCurrent[i * 13 + ii].GetCard());
                        //System.Threading.Thread.Sleep(2000);
                    }
                }
                for (byte i = 0; i < 3; i++)
                {
                    DeckCurrent.Add(new Card(14, i == 0 ? "R" : i == 1 ? "B" : "W"));
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
            for (int i = 0; i < tMode + 1; i++)
            {
                DeckCurrent.Insert(DeckRNG.Next(0, DeckCurrent.Count + 1), DeckCurrent[0]);
                DeckCurrent.RemoveAt(0);
            }
        }
        internal string DeckPeek(bool isTop = true) //first, random
        {
            return DeckCurrent.Count == 0 ? "?0" : isTop ? DeckCurrent[0].GetCard() : DeckCurrent[DeckRNG.Next(0, DeckCurrent.Count)].GetCard();
        }
        internal Card DeckDraw(bool isTop = true) //first, random
        {
            Card Temp = DeckCurrent.Count == 0 ? new Card() : isTop ? DeckCurrent[0] : DeckCurrent[DeckRNG.Next(0, DeckCurrent.Count)];
            DeckCurrent.Remove(Temp);
            return Temp;
        }
        public Deck()
        {
            DeckFill(true);
        }
    }
}