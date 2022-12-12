using System;

namespace Genzo
{
	internal class Player
	{
		internal Caravan[] Caravans = new Caravan[3];
		internal Hand PlayerHand = new Hand();
		internal Deck PlayerDeck = new Deck();
		internal bool Control;
		public Player(bool tControl)
		{
			Control = tControl;
			PlayerHand.HandOwner = Control ? (byte)0 : (byte)1;
			for (byte i = 0; i < 3; i++)
			{
				Caravans[i] = new Caravan((byte)(Control ? 0 : 1), (byte)((Control ? 0 : 1) * 3 + i));
			}
			PlayerHand.HandSize = 8;
			PlayerHand.HandFill(PlayerDeck);
			PlayerHand.HandSize = 5;
		}
	}
}