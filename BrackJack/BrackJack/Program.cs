namespace BlackJack
{
	class Program
	{
		static void Main(string[] args)
		{

			var isRestartGame = true;
			//trueの場合、再ゲーム
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
