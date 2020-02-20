using static System.Console;
using System.Linq;
using System;

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
	/// <summary>
	/// 手番
	/// </summary>
	enum Turn
	{
		None,
		PlayerTurn,
		DealerTurn,
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
		bool IsRestartGame { get; set; }
		/// <summary>
		/// 手番
		/// </summary>
		Turn Turn { get; set; }

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
			InitializeGame();

			//開始ドロー
			FirstDraw();

			//プレイヤーターン
			PlayerTurn();
			if (IsRestartGame) return IsRestartGame;

			//ディーラーターン
			DealerTurn();
			if (IsRestartGame) return IsRestartGame;

			//結果
			ComfirmResult();

			//再ゲームするか確認
			ComfirmRestartGame();
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
		/// Game初期化
		/// </summary>
		private void InitializeGame()
		{
			Player.InitializeHand();
			Dealer.InitializeHand();
			Deck.AttemptInitializeDeckList();
		}

		/// <summary>
		/// ゲーム開始時ドロー
		/// </summary>
		private void FirstDraw()
		{
			Player.DrawCard(Deck);
			Player.DrawCard(Deck);
			Dealer.DrawCard(Deck);
			Dealer.DrawCard(Deck);

			ShowPointsAndHand(Player);
			ShowPointsAndHand(Dealer);
		}

		/// <summary>
		/// プレイヤーターン
		/// </summary>
		/// <param name="inputKey"></param>
		private void PlayerTurn()
		{
			Turn = Turn.PlayerTurn;
			var playerAction = ComfirmPlayerAction();

			while (playerAction == PlayerAction.Hit)
			{
				Player.DrawCard(Deck);

				ShowPointsAndHand(Player);
				ShowPointsAndHand(Dealer);

				if (!Player.Hand.IsBust)
				{
					playerAction = ComfirmPlayerAction();
				}
				else break;
			}

			//バースト
			if (Player.Hand.IsBust)
			{
				ShowBustMessage(nameof(Player));
				ShowResultMessage(Result.Lose);

				ComfirmRestartGame();
			}
		}

		/// <summary>
		/// ディーラーターン
		/// </summary>
		private void DealerTurn()
		{
			Turn = Turn.DealerTurn;

			while (!Dealer.IsFinishedDealerDraw)
			{
				if (!Deck.HasDeckRunOut)
				{
					Dealer.DrawCard(Deck);
				}
				else
				{
					ComfirmRestartGame();
				}

				if (Dealer.Hand.IsBust)
				{
					ShowPointsAndHand(Player);
					ShowPointsAndHand(Dealer);

					ShowBustMessage(nameof(Dealer));
					ShowResultMessage(Result.Win);

					ComfirmRestartGame();
				}
			}
		}

		/// <summary>
		/// 結果確認
		/// </summary>
		private void ComfirmResult()
		{
			ShowPointsAndHand(Player);
			ShowPointsAndHand(Dealer);

			var result = Result.None;

			if (Player.Hand.Points > Dealer.Hand.Points) result = Result.Win;
			else if (Player.Hand.Points < Dealer.Hand.Points) result = Result.Lose;
			else result = Result.Draw;

			ShowResultMessage(result);
		}


		/// <summary>
		/// ヒットしたかスタンドしたかを取得する
		/// </summary>
		private PlayerAction GetPlayerAction()
		{
			var inputKey = ComfirmHitOrStand();

			while (!(inputKey == "h" || inputKey == "s")) inputKey = ComfirmHitOrStand();

			if (inputKey == "h") return PlayerAction.Hit;
			else return PlayerAction.Stand;
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
		/// 勝敗を取得する
		/// 入力キーを確認する
		/// </summary>
		/// <returns></returns>
		private Result GetResult()
		private string ComfirmInputKey()
		{
			if (Player.Hand.Points > Dealer.Hand.Points) return Result.Win;
			else if (Player.Hand.Points < Dealer.Hand.Points) return Result.Lose;
			else return Result.Draw;
			var inputKey = ReadLine();
			WriteLine();

			return inputKey;
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
		private void ShowPointsAndHand(Player player)
		{
			if(player == Player) Write("Player: ");
			else Write("Dealer: ");

			if (player == Dealer && Turn != Turn.DealerTurn)
			{
				Write($" Total:{player.Hand.HandCards.FirstOrDefault().BlackJackNumber} ");

				Write($"[{player.Hand.HandCards.FirstOrDefault().Mark} {player.Hand.HandCards.FirstOrDefault().DisplayNumber}]");
				WriteLine();
			}
			else
			{
				Write($" Total:{player.Hand.Points} ");

				foreach (var card in player.Hand.HandCards)
				{
					Write($"[{card.Mark} {card.DisplayNumber}]");
				}
				WriteLine();
			}
		}
	}
}
