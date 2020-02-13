﻿using static System.Console;

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
			this.Player = player;
			this.Dealer = dealer;
			this.Deck = deck;
		}

		public bool Run()
		{
			//ゲーム初期化
			Player.InitializeHand();
			Dealer.InitializeHand();
			Deck.InitializeDeckList();

			ShowStartMessage();

			//開始ドロー
			Player.DrawCard(Deck.DrawnCard());
			Player.DrawCard(Deck.DrawnCard());
			Dealer.DrawCard(Deck.DrawnCard());
			Dealer.DrawCard(Deck.DrawnCard());

			//表示
			ShowPointsAndHand(false, Player.Hand, nameof(Player));
			ShowPointsAndHand(true, Dealer.Hand, nameof(Dealer));

			//プレイヤーターン
			var isPlayerHit = ReturnIsPlayerHit();
			while (isPlayerHit)
			{
				Player.DrawCard(Deck.DrawnCard());

				ShowPointsAndHand(false, Player.Hand, nameof(Player));
				ShowPointsAndHand(true, Dealer.Hand, nameof(Dealer));

				if (!Player.IsBust)
				{
					isPlayerHit = ReturnIsPlayerHit();
				}
				else break;
			}

			//バースト
			if (Player.IsBust)
			{
				ShowBustMessage(nameof(Player));
				ShowResultMessage(Result.Lose);

				SetIsRestartGame();
				return IsRestartGame;
			}
			//ディーラーターン
			else
			{
				while (Dealer.Hand.Points < 17)
					Dealer.DrawCard(Deck.DrawnCard());
				if (Dealer.IsBust)
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
			return;
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

			return;
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
				if ((playersHand.HandCards[0].DisplayNumber != "A") && (playersHand.HandCards[1].DisplayNumber == "A"))
				{
					Write($" Total:{playersHand.Points - 11} ");
				}
				else if (playersHand.HandCards[0].DisplayNumber == "A")
				{
					Write($" Total:11 ");
				}
				else
				{
					Write($" Total:{playersHand.ReturnConvetedJQK(playersHand.HandCards[0])} ");
				}

				WriteLine($"[{playersHand.HandCards[0].Mark} {playersHand.HandCards[0].DisplayNumber}]");
			}

			return;
		}
	}
}
