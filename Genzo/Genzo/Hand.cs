using System;
using System.Collections.Generic;
using System.Linq;

namespace Genzo
{
    internal class Hand
    {
        internal byte HandId;
        internal List<PlaydCard> HandCards = new List<PlaydCard>();
        internal byte HandSize; // 0 for infinite, other for finite
        internal byte HandOwner;
        internal void HandAdvance()
        {
            HandCards.ForEach(x => x.SetSlot());
            HandCards.OrderBy(x => x.GetSlot());
        }
        internal void HandFill(Deck tDeck)
        {
            if (HandSize == 0)
            {
                return;
            }
            for (byte i = (byte)HandCards.Count; i < HandSize; i++)
            {
                HandCards.Add(new PlaydCard(tDeck.DeckDraw(), 0));
                HandAdvance();
            }
        }
    }
}
