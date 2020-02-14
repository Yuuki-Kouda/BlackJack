namespace BlackJack
{
	class Program
	{
		static void Main(string[] args)
		{
			Player player = new Player();
			Dealer dealer = new Dealer();
			Deck deck = new Deck();
			Game game = new Game(player, dealer, deck);

			var isRestartGame = true;
			//trueの場合、再ゲーム
			while (isRestartGame)
			{
				//生成
				isRestartGame = game.Run();
			}
		}
	}
}
