using System;
using System.Collections.Generic;

namespace Genzo
{
    internal class Game
    {
        internal Debug GameDebug = new Debug();
        internal Player Player1 = new Player(true);
        internal Player Player2 = new Player(false);
        internal void GameTurn(Player tPlayer)
        {
            if (!tPlayer.Control)
            {
                BotMove();
                return;
            }
            GameDebug.Translator(Console.ReadLine());
        }
        internal void BotMove()
        {

        }
    }
}
