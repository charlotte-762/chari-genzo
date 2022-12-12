using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Genzo
{
    internal class Caravan : Hand
    {
        internal byte CaravanDirection; // 0 off 1 asc 2 desc
        internal string CaravanSuit;
        internal string CaravanName;
        internal bool CaravanOwner;
        private readonly string[] CaravanNames = new string[6] { "Boneyard", "The Hub", "Shady Sands", "New Reno", "Redding", "Dayglow" };
        internal bool CaravanCardAdd(Card tCard, bool tOwner, byte tSlot = 0)
        {
            Console.WriteLine(tCard.GetCard() + " " + CaravanDirection + " " + CaravanSuit);
            Thread.Sleep(2000);
            if (tCard.GetValue() < 11 && tOwner != CaravanOwner) // number card on opponent
            {
                return false;
            }
            if (HandCards == null || HandCards.Count == 0 && tCard.GetValue() < 11) // first card
            {
                HandCards.Add(new PlaydCard(tCard, tSlot));
                CaravanUpdate();
                return true;
            }
            else if (HandCards == null || HandCards.Count == 0) // empty caravan face card 
            {
                return false;
            }
            //if (HandCards.Count == 1 && tCard.GetValue() == HandCards[0].GetValue()) // same value of first two cards
            //{
            //    return false;
            //}
            if (tCard.GetSuit() != CaravanSuit && CaravanSuit != "*") // wrong suit and..
            {
                if (CaravanDirection == 1 && HandCards.FindLast( x => x.GetValue() < 11).GetValue() >= tCard.GetValue()) // ..small on asc
                {
                    return false;
                }
                if (CaravanDirection == 2 && HandCards.FindLast(x => x.GetValue() < 11).GetValue() <= tCard.GetValue()) // ..big on desc
                {
                    return false;
                }
            }
            HandCards.Add(new PlaydCard(tCard, tSlot));
            CaravanUpdate();
            return true;
        }
        internal void CaravanUpdate()
        {
            HandAdvance();
            List<PlaydCard> Temp = HandCards.ToArray().ToList();
            CaravanSuit = HandCards.Last().GetValue() > 11 ? HandCards.Last().GetValue() == 12 ? "*" : CaravanSuit : HandCards.Last().GetSuit();
            if (HandCards.Count == 1) // first card ignored
            {
                return;
            }
            if (HandCards.Last().GetValue() == 12)
            {
                CaravanDirection = 0;
                CaravanSuit = "*";
                return;
            }
            Temp.RemoveAll(delegate (PlaydCard x) { return x.GetValue() >= 11; });
            Temp.RemoveRange(0, Temp.Count - 2);
            CaravanDirection = Temp[0].GetValue() < Temp[1].GetValue() ? (byte)1 : (byte)2;
        }
        internal ushort GetCaravanValue() // 2738
        {
            ushort E = 0;
            ushort Temp = 0;
            foreach (PlaydCard n in HandCards)
            {
                Temp = n.GetValue() == 13 ? (byte)(Temp * 2) : n.GetValue() < 11 ? n.GetValue() : Temp; // gets card value, ignores if jack queen or joker, uses king effect
                E += HandCards.FindIndex(x => x == n) == HandCards.Count - 1 || HandCards[HandCards.FindIndex(x => x == n) + 1].GetValue() < 11 ? Temp : (ushort)0;
            }
            return E;
        }
        internal void Reset()
        {
            HandCards.Clear();
            CaravanDirection = 0;
            CaravanSuit = "*";
        }
        public Caravan(byte tOwner, byte tId)
        {
            CaravanOwner = tOwner == 0;
            HandId = tId;
            CaravanName = CaravanNames[HandId];
            HandCards = new List<PlaydCard>();
            Reset();
        }
    }
}