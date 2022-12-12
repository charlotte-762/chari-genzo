using System;

namespace Genzo
{
    internal class Card // td: maybe solve the 10 issue
    {
        protected string Suit;
        protected byte Value;
        public string GetSuit() => Suit;
        public byte GetValue() => Value;
        public string GetCard() => $"{Suit}{(Value == 1 ? "A" : Value > 9 ? Value > 10 ? Value > 11 ? Value > 12 ? "K" : "Q" : "J" : "T" : Value.ToString())}";
        public Card(byte tValue = 0, string tSuit = "?")
        {
            Suit = tSuit;
            Value = tValue;
        }
    }
    internal class PlaydCard : Card
    {
        protected byte Slot = 0;
        public byte GetSlot() => Slot;
        public void SetSlot(byte tSlot = 0) { Slot = tSlot == 0 ? (byte)(Slot + 1) : tSlot; }
        public PlaydCard(Card tCard, byte tSlot)
        {
            Suit = tCard.GetSuit();
            Value = tCard.GetValue();
            Slot = tSlot;
        }
    }
}