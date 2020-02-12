using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Linq;

namespace BrackJack
{
	enum Players
	{
		Player,
		Dealer
	}
	enum HaveA
	{
		None,
		Have,
		AddPoint
	}
	/// <summary>
	/// プレイヤー
	/// </summary>
	class Player
	{
		/// <summary>
		/// 手札
		/// </summary>
		public Hand Hand { get; set; }
		/// <summary>
		/// 点数
		/// </summary>
		public int Points { get; set; }
		/// <summary>
		/// バースト有無
		/// </summary>
		public bool IsBust { get; set; } = false;
		/// <summary>
		/// 手札にAの有無
		/// </summary>
		private HaveA HaveA { get; set; } = HaveA.None;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Player()
		{
			Hand hand = new Hand();
			this.Hand = hand;
			this.Points = 0;
		}

		/// <summary>
		/// ドローカード
		/// </summary>
		public void DrawCard(Card card)
		{
			this.Hand.AddCard(card);
			var point = ReturnConvertedNumber(card);
			this.Points += point;
			if (this.Points > 21) this.IsBust = true;
		}

		/// <summary>
		/// 点数と手札表示
		/// </summary>
		public void ShowPointsAndHand(bool isDealerFirstDraw, Players players)
		{
			Write($"{players}: ");

			if (!isDealerFirstDraw)
			{
				Write($" Total:{this.Points} ");

				foreach (var card in this.Hand.HandCards)
				{
					Write($"[{card.Mark} {card.DisplayNumber}]");
				}
				WriteLine();
			}
			else
			{
				var points = this.Points;
				if((this.Hand.HandCards[0].DisplayNumber != "A") && (this.Hand.HandCards[1].DisplayNumber == "A"))
				{
					Write($" Total:{points - 11} ");
				}
				else if(this.Hand.HandCards[0].DisplayNumber == "A")
				{
					Write($" Total: 11 ");
				}
				else
				{
					Write($" Total:{ReturnConvetedJQK(this.Hand.HandCards[0])} ");
				}

				WriteLine($"[{this.Hand.HandCards[0].Mark} {this.Hand.HandCards[0].DisplayNumber}]");
			}
		}

		/// <summary>
		/// バーストメッセージ
		/// </summary>
		/// <param name="user"></param>
		public void ShowBustMessage(Players players)
		{
			WriteLine($"{players}はバーストしました。");
		}

		/// <summary>
		/// 勝者、敗者メッセージ
		/// </summary>
		/// <param name="winner"></param>
		/// <param name="loser"></param>
		public void ShowResultMessage(Players winner, Players loser, bool IsDraw)
		{
			if (!IsDraw) WriteLine($"{winner}が勝利しました。{loser}の負けです。");
			else WriteLine("引き分けです。");
		}

		/// <summary>
		/// 点数変換
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		private int ReturnConvertedNumber(Card card)
		{
			var convertedNumber = card.Number;

			convertedNumber = ReturnConvetedJQK(card);

			if ((this.HaveA != HaveA.None) || (card.DisplayNumber == "A"))
			{
				convertedNumber = ReturnConvetedAAndRecalucatePoints(convertedNumber, card);
			}

			return convertedNumber;
		}

		/// <summary>
		/// JQKを変換して返す
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		private int ReturnConvetedJQK(Card card)
		{
			var convertedNumber = card.Number;

			if ((card.Number == 11) || (card.Number == 12) || (card.Number == 13))
				convertedNumber = 10;

			return convertedNumber;
		}

		/// <summary>
		/// Aを変換して返し、点数を計算する
		/// </summary>
		private int ReturnConvetedAAndRecalucatePoints(int cardNumber, Card card)
		{
			var points = this.Points;

			if (card.DisplayNumber == "A")
			{
				switch(HaveA)
				{
					case (HaveA.None):
						if((points + 11) > 21) this.HaveA = HaveA.Have;
						else
						{
							HaveA = HaveA.AddPoint;
							cardNumber = 11;
						}
						break;

					case (HaveA.Have):
						break;

					case (HaveA.AddPoint):
						if ((points + 1) > 21) 
						{
							this.HaveA = HaveA.Have;
							this.Points -= 10;
						}
						break;
				}
			}
			else
			{
				if((this.HaveA == HaveA.AddPoint) && ((points + cardNumber) > 21))
				{
					this.HaveA = HaveA.Have;
					this.Points -= 10;
				}
			}
			return cardNumber;
		}
	}
}
