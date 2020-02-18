using static System.Console;

namespace BlackJack
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
		Dealer Dealer { get; set; }
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
		public Game(Player player, Dealer dealer, Deck deck)
		{
			this.Player = player;
			this.Dealer = dealer;
			this.Deck = deck;

			ShowStartMessage();
		}

		public bool Run()
		{
			//ゲーム初期化
			Player.InitializePlayer();
			Dealer.InitializeDealer();
			Deck.InitializeDeckList();

			//開始ドロー
			Player.DrawCard(Deck.DrawnCard());
			Player.DrawCard(Deck.DrawnCard());
			Dealer.DrawCard(Deck.DrawnCard());
			Dealer.DrawCard(Deck.DrawnCard());

			//表示
			ShowPointsAndHand(false, Player.Hand, nameof(Player));
			ShowPointsAndHand(true, Dealer.Hand, nameof(Dealer));

			//プレイヤーターン
			SetPlayerAction();
			while (Player.PlayerAction == PlayerAction.Hit)
			{
				Player.DrawCard(Deck.DrawnCard());

				ShowPointsAndHand(false, Player.Hand, nameof(Player));
				ShowPointsAndHand(true, Dealer.Hand, nameof(Dealer));

				if (!Player.Hand.IsBust)
				{
					SetPlayerAction();
				}
				else break;
			}

			//バースト
			if (Player.Hand.IsBust)
			{
				ShowBustMessage(nameof(Player));
				ShowResultMessage(Result.Lose);

				SetIsRestartGame();
				return IsRestartGame;
			}
			//ディーラーターン
				while (!Dealer.IsFinishedDraw)
				{
					Dealer.DrawCard(Deck.DrawnCard());
					if (Dealer.Hand.IsBust)
					{
						ShowPointsAndHand(false, Player.Hand, nameof(Player));
						ShowPointsAndHand(false, Dealer.Hand, nameof(Dealer));

						ShowBustMessage(nameof(Dealer));
						ShowResultMessage(Result.Win);

						SetIsRestartGame();
						return IsRestartGame;
					}
				}

			//勝負
			ShowPointsAndHand(false, Player.Hand, nameof(Player));
			ShowPointsAndHand(false, Dealer.Hand, nameof(Dealer));

			var result = ReturnResult();
			ShowResultMessage(result);

			SetIsRestartGame();
			return IsRestartGame;
		}

		/// <summary>
		/// 開始メッセージ出力
		/// </summary>
		private void ShowStartMessage()
		{
			WriteLine("ブラックジャックゲームへようこそ");
			WriteLine();
		}

		/// <summary>
		/// ヒットするかスタンドするかを受け取る
		/// </summary>
		/// <returns></returns>
		private string ComfirmHitOrStand()
		{
			WriteLine();
			Write("ヒットする場合は\"h\"、スタンドの場合は\"s\"を入力してEnter ");
			var inputKey = ReadLine();
			WriteLine();

			return inputKey;
		}
		/// <summary>
		/// ヒットかスタンドかを設定する
		/// </summary>
		private void SetPlayerAction()
		{
			var inputKey = ComfirmHitOrStand();

			while (!(inputKey == "h" || inputKey == "s")) inputKey = ComfirmHitOrStand();

			if (inputKey == "h") Player.PlayerAction = PlayerAction.Hit;
			else Player.PlayerAction = PlayerAction.Stand;
		}

		/// <summary>
		/// リスタートするか確認する
		/// </summary>
		/// <returns></returns>
		private string ComfirmRestartGame()
		{
			var ShowText = "もう一度ゲームをする場合は\"r\"、ゲームを終了する場合は\"e\"を入力してEnter ";

			WriteLine();
			Write(ShowText);
			var inputKey = ReadLine();
			WriteLine();

			return inputKey;
		}

		/// <summary>
		/// リスタート有無を設定する
		/// </summary>
		private void SetIsRestartGame()
		{
			var inputKey = ComfirmRestartGame();

			while (!(inputKey == "r" || inputKey == "e")) inputKey = ComfirmRestartGame();

			//ゲーム終了
			if (inputKey == "e") return;
			//ゲームリスタート
			IsRestartGame = true;
		}

		/// <summary>
		/// 勝敗を返す
		/// </summary>
		/// <returns></returns>
		private Result ReturnResult()
		{
			if (Player.Hand.Points > Dealer.Hand.Points) return Result.Win;
			else if (Player.Hand.Points < Dealer.Hand.Points) return Result.Lose;
			else return Result.Draw;
		}

		/// <summary>
		/// バーストメッセージ
		/// </summary>
		/// <param name="user"></param>
		private void ShowBustMessage(string player)
		{
			WriteLine($"{player}はバーストしました。");
		}

		/// <summary>
		/// 勝者、敗者メッセージ
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

		/// <summary>
		/// 点数と手札表示
		/// </summary>
		private void ShowPointsAndHand(bool isDealerFirstDraw, Hand playersHand, string player)
		{
			Write($"{player}: ");

			if (!isDealerFirstDraw)
			{
				Write($" Total:{playersHand.Points} ");

				foreach (var card in playersHand.HandCards)
				{
					Write($"[{card.Mark} {card.DisplayNumber}]");
				}
				WriteLine();
			}
			else
			{
				Write($" Total:{playersHand.HandCards[0].BlackJackNumber} ");

				Write($"[{playersHand.HandCards[0].Mark} {playersHand.HandCards[0].DisplayNumber}]");
				WriteLine();
			}
		}
	}
}
