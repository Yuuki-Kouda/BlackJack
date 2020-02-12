using static System.Console;

namespace BrackJack
{
	/// <summary>
	/// 勝敗
	/// </summary>
	enum Result
	{
		Win,
		Lose,
		Draw
	}

	class Game
	{
		/// <summary>
		/// プレイヤー
		/// </summary>
		Player Player { get; set; }
		/// <summary>
		/// ディーラー
		/// </summary>
		Player Dealer { get; set; }
		/// <summary>
		/// 山札
		/// </summary>
		Deck Deck { get; set; }
		/// <summary>
		/// リスタート有無
		/// </summary>
		bool IsRestartGame { get; set; } = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="player"></param>
		/// <param name="dealer"></param>
		/// <param name="deck"></param>
		public Game(Player player, Player dealer, Deck deck)
		{
			Player = player;
			Dealer = dealer;
			Deck = deck;
		}

		public bool Run()
		{
			ShowStartMessage();

			//開始ドロー
			Player.DrawCard(Deck.DrawnCard());
			Player.DrawCard(Deck.DrawnCard());
			Dealer.DrawCard(Deck.DrawnCard());
			Dealer.DrawCard(Deck.DrawnCard());

			//表示
			Player.ShowPointsAndHand(false, Players.Player);
			Dealer.ShowPointsAndHand(true, Players.Dealer);

			//プレイヤーターン
			var isPlayerHit = ReturnIsPlayerHit();
			while(isPlayerHit)
			{
				Player.DrawCard(Deck.DrawnCard());

				Player.ShowPointsAndHand(false, Players.Player);
				Dealer.ShowPointsAndHand(true, Players.Dealer);

				if (!Player.IsBust)
				{
					isPlayerHit = ReturnIsPlayerHit();
				}
				else break;
			}

			//バースト
			if (Player.IsBust)
			{
				Player.ShowBustMessage(Players.Player);
				Dealer.ShowResultMessage(Players.Dealer, Players.Player, false);

				SetIsRestartGame();
				return IsRestartGame;
			}
			//ディーラーターン
			else
			{
				while (Dealer.Points < 17)
					Dealer.DrawCard(Deck.DrawnCard());
				if (Dealer.IsBust)
				{
					Player.ShowPointsAndHand(false, Players.Player);
					Dealer.ShowPointsAndHand(false, Players.Dealer);

					Dealer.ShowBustMessage(Players.Dealer);
					Player.ShowResultMessage(Players.Player, Players.Dealer, false);

					SetIsRestartGame();
					return IsRestartGame;
				}
			}
				//勝負
				Player.ShowPointsAndHand(false, Players.Player);
				Dealer.ShowPointsAndHand(false, Players.Dealer);

				var result = ReturnResult();
				ShowResultMessage(result);

				SetIsRestartGame();
				return IsRestartGame;		
		}

		/// <summary>
		/// 開始メッセージ出力
		/// </summary>
		public void ShowStartMessage()
		{
			WriteLine("ブラックジャックゲームへようこそ");
			WriteLine();
		}

		/// <summary>
		/// プレイヤーヒット有無を返す
		/// </summary>
		private bool ReturnIsPlayerHit()
		{
			var isPlayerHit = false;
			var ShowText = "ヒットする場合は\"h\"、スタンドの場合は\"s\"を入力してEnter ";

			WriteLine();
			Write(ShowText);
			var inputKey = ReadLine();
			WriteLine();

			while (!(inputKey == "h" || inputKey == "s"))
			{
				Write(ShowText);
				inputKey = ReadLine();
				WriteLine();
			}

			if (inputKey == "h") isPlayerHit = true;

			return isPlayerHit;
		}

		/// <summary>
		/// リスタート有無を設定する
		/// </summary>
		private void SetIsRestartGame()
		{
			var ShowText = "もう一度ゲームをする場合は\"r\"、ゲームを終了する場合は\"e\"を入力してEnter ";

			WriteLine();
			Write(ShowText);
			var inputKey = ReadLine();
			WriteLine();

			while (!(inputKey == "r" || inputKey == "e"))
			{
				Write(ShowText);
				inputKey = ReadLine();
				WriteLine();
			}

			//ゲーム終了
			if (inputKey == "e") return;

			//ゲームリスタート
			IsRestartGame = true;
			return;
		}

		/// <summary>
		/// 勝敗を返す
		/// </summary>
		/// <returns></returns>
		private Result ReturnResult()
		{
			if (Player.Points > Dealer.Points) return Result.Win;
			else if (Player.Points < Dealer.Points) return Result.Lose;
			else return Result.Draw;
		}

		/// <summary>
		/// 勝敗メッセージ出力
		/// </summary>
		/// <param name="result"></param>
		private void ShowResultMessage(Result result)
		{
			switch (result)
			{
				case Result.Win:
					WriteLine("Playerが勝利しました。Dealerの負けです。");
					break;

				case Result.Lose:
					WriteLine("Dealerが勝利しました。Playerの負けです。");
					break;

				case Result.Draw:
					WriteLine("引き分けです。");
					break;
			}
		}
	}
}
