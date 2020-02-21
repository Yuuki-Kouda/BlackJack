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
		None,
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
	/// プレイヤーアクション
	/// </summary>
	enum PlayerAction
	{
		None,
		Hit,
		Stand
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
			//リスタート有無
			var isRestartGame = false;

			//ゲーム初期化
			InitializeGame();

			//開始ドロー
			FirstDraw();

			//プレイヤーターン
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

			//プレイヤーがバーストしてなければ
			if (!Player.Hand.IsBust)
			{
				//ディーラーターン
				Turn = Turn.DealerTurn;

				while (!Dealer.CanDraw)
				{
					Dealer.DrawCard(Deck);
				}

				//ディーラーがバーストしていなければ
				if (!Dealer.Hand.IsBust)
				{
					//結果確認
					ComfirmResult();
				}
				else
				{
					ShowPointsAndHand(Player);
					ShowPointsAndHand(Dealer);

					ShowBustMessage(Dealer);
					ShowResultMessage(Result.Win);
				}
			}
			else
			{
				ShowBustMessage(Player);
				ShowResultMessage(Result.Lose);
			}

			//再ゲームするか確認
			isRestartGame = ComfirmRestartGame();
			return isRestartGame;
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
			Turn = Turn.None;
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
		/// ヒットしたかスタンドしたかを確認する
		/// </summary>
		private PlayerAction ComfirmPlayerAction()
		{
			var ShowText = "ヒットする場合は\"h\"、スタンドの場合は\"s\"を入力してEnter ";

			WriteLine();
			Write(ShowText);

			var inputKey = ComfirmInputKey();

			while (!(inputKey == "h" || inputKey == "s"))
			{
				WriteLine();
				Write(ShowText);
				inputKey = ComfirmInputKey();
			}

			if (inputKey == "h") return PlayerAction.Hit;
			else return PlayerAction.Stand;
		}

		/// <summary>
		/// 再ゲームするか確認する
		/// </summary>
		private bool ComfirmRestartGame()
		{
			var ShowText = "もう一度ゲームをする場合は\"r\"、ゲームを終了する場合は\"e\"を入力してEnter ";

			WriteLine();
			Write(ShowText);

			var inputKey = ComfirmInputKey();

			while (!(inputKey == "r" || inputKey == "e")) inputKey = ComfirmInputKey();

			//ゲーム終了
			if (inputKey == "e") return false;
			//ゲームリスタート
			return true;
		}

		/// <summary>
		/// 入力キーを確認する
		/// </summary>
		/// <returns></returns>
		private string ComfirmInputKey()
		{
			var inputKey = ReadLine();
			WriteLine();

			return inputKey;
		}

		/// <summary>
		/// バーストメッセージ表示
		/// </summary>
		/// <param name="user"></param>
		private void ShowBustMessage(Player player)
		{
			if(player == Player) 
				WriteLine("Playerはバーストしました。");
			else
				WriteLine("Dealerはバーストしました。");
		}

		/// <summary>
		/// 勝者、敗者メッセージ表示
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
			if (player == Player) Write("Player: ");
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