using System;
using static System.Console;

namespace BrackJack
{
	class Program
	{
		static void Main(string[] args)
		{

			var isRestartGame = true;
			while (isRestartGame)
			{
				//生成
				Player player = new Player();
				Player dealer = new Player();
				Deck deck = new Deck();
				Game game = new Game(player, dealer, deck);

				isRestartGame = game.Run();
			}
			return;
		}
	}
}
