using System;

namespace Genzo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Display DP = new Display("v221209");
            Debug Game = new Debug();
            Deck Starter = new Deck();
            Starter.DeckShuffle();
            Game Gaem = new Game();
            //for (int i = 0; i < 4; i++)
            //{
            //    Yerevan.CaravanCardAdd(Starter.DeckDraw(), true);
            //}
            //Yerevan.CaravanCardAdd(new CaravanCard(new Card(10), 0), true);
            //Starter.DeckShuffle();
            //Console.WriteLine(Starter.DeckPeek(false));
            //foreach (Card n in Yerevan.CaravanCards)
            //{
            //    Console.WriteLine($"{n.GetCard()}");
            //}
            while (true)
            {
                Console.SetCursorPosition(6, 26);
                Game.Translator(Console.ReadLine());
                DP.DisplayHand(Gaem.Player1.PlayerHand);
                foreach (Caravan n in Gaem.Player1.Caravans)
                {
                    DP.DisplayCaravan(n);
                }
                DP.DisplayHand(Gaem.Player2.PlayerHand);
                foreach (Caravan n in Gaem.Player2.Caravans)
                {
                    DP.DisplayCaravan(n);
                }
            }
            //_ = Console.ReadKey();
        }
    }
}
