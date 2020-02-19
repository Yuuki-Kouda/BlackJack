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
	/// <summary>
	/// ゲームプレイヤー
	/// </summary>
	enum GamePlayer
	{
		Player,
		Dealer
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
			Player.InitializeHand();
			Dealer.InitializeHand();
			Deck.InitializeDeckList();
			IsRestartGame = false;
			Turn = Turn.None;

			//開始ドロー
			if (!Deck.HasDeckRunOut)
			{
				Player.DrawCard(Deck.DrawCard());
			}
			else
			{
				ShowDeckRunsOutMessage();
				SetIsRestartGame();
				return IsRestartGame;
			}

			if (!Deck.HasDeckRunOut)
			{
				 Player.DrawCard(Deck.DrawCard()); 
			}
			else
			{
				ShowDeckRunsOutMessage();
				SetIsRestartGame();
				return IsRestartGame;
			}

			if (!Deck.HasDeckRunOut)
			{
				Dealer.DrawCard(Deck.DrawCard());
			}
			else
			{
				ShowDeckRunsOutMessage();
				SetIsRestartGame();
				return IsRestartGame;
			}

			if (!Deck.HasDeckRunOut)
			{
				Dealer.DrawCard(Deck.DrawCard());
			}
			else
			{
				ShowDeckRunsOutMessage();
				SetIsRestartGame();
				return IsRestartGame;
			}

			//表示
			ShowPointsAndHand(GamePlayer.Player, Player.Hand, nameof(Player));
			ShowPointsAndHand(GamePlayer.Dealer, Dealer.Hand, nameof(Dealer));

			//プレイヤーターン
			Turn = Turn.PlayerTurn;
			var playerAction = GetPlayerAction();

			while (playerAction == PlayerAction.Hit)
			{
				if (!Deck.HasDeckRunOut)
				{
					Player.DrawCard(Deck.DrawCard());
				}
				else
				{
					ShowDeckRunsOutMessage();
					SetIsRestartGame();
					return IsRestartGame;
				}

				ShowPointsAndHand(GamePlayer.Player, Player.Hand, nameof(Player));
				ShowPointsAndHand(GamePlayer.Dealer, Dealer.Hand, nameof(Dealer));

				if (!Player.Hand.IsBust)
				{
					playerAction = GetPlayerAction();
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
			Turn = Turn.DealerTurn;

			while (!Dealer.IsFinishedDealerDraw)
			{
				if (!Deck.HasDeckRunOut)
				{
					Dealer.DrawCard(Deck.DrawCard());
				}
				else
				{
					ShowDeckRunsOutMessage();
					SetIsRestartGame();
					return IsRestartGame;
				}

				if (Dealer.Hand.IsBust)
				{
					ShowPointsAndHand(GamePlayer.Player, Player.Hand, nameof(Player));
					ShowPointsAndHand(GamePlayer.Dealer, Dealer.Hand, nameof(Dealer));

					ShowBustMessage(nameof(Dealer));
					ShowResultMessage(Result.Win);

					SetIsRestartGame();
					return IsRestartGame;
				}
			}

			//勝負
			ShowPointsAndHand(GamePlayer.Player, Player.Hand, nameof(Player));
			ShowPointsAndHand(GamePlayer.Dealer, Dealer.Hand, nameof(Dealer));

			var result = GetResult();
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
		/// 山札切れメッセージ出力
		/// </summary>
		private void ShowDeckRunsOutMessage()
		{
			WriteLine("山札にカードがありません。");
		}

		/// <summary>
		/// ヒットするかスタンドするかを確認する
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
		/// </summary>
		/// <returns></returns>
		private Result GetResult()
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
		private void ShowPointsAndHand(GamePlayer gamePlayer, Hand playersHand, string player)
		{
			Write($"{player}: ");

			if (gamePlayer == GamePlayer.Dealer && Turn != Turn.DealerTurn)
			{
				Write($" Total:{playersHand.HandCards.FirstOrDefault().BlackJackNumber} ");

				Write($"[{playersHand.HandCards.FirstOrDefault().Mark} {playersHand.HandCards.FirstOrDefault().DisplayNumber}]");
				WriteLine();
			}
			else
			{
				Write($" Total:{playersHand.Points} ");

				foreach (var card in playersHand.HandCards)
				{
					Write($"[{card.Mark} {card.DisplayNumber}]");
				}
				WriteLine();
			}
		}
	}
}
