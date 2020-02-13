using System.Collections.Generic;

namespace BrackJack
{
	enum HaveA
	{
		None,
		Have,
		AddPoint
	}

	class Hand
	{
		/// <summary>
		/// 手札
		/// </summary>
		public List<Card> HandCards { get; set; }
		/// <summary>
		/// 点数
		/// </summary>
		public int Points { get; set; } = 0;
		/// <summary>
		/// 手札にAの有無
		/// </summary>
		private HaveA HaveA { get; set; } = HaveA.None;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Hand()
		{
			List<Card> cards = new List<Card>();
			this.HandCards = cards;

		}

		/// <summary>
		/// カード追加
		/// </summary>
		/// <param name="card"></param>
		public bool AddCard(Card card)
		{
			HandCards.Add(card);
			var convetedNumberPoint = ReturnConvertedNumber(card);
			Points += convetedNumberPoint;
			if (Points > 21) return true;

			return false;
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

			if ((HaveA != HaveA.None) || (card.DisplayNumber == "A"))
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
		public int ReturnConvetedJQK(Card card)
		{
			var convertedNumber = card.Number;

			if ((card.Number == 11) || (card.Number == 12) || (card.Number == 13))
				convertedNumber = 10;

			return convertedNumber;
		}

		/// <summary>
		/// Aを変換して返し、点数を計算する
		/// </summary>
		public int ReturnConvetedAAndRecalucatePoints(int cardNumber, Card card)
		{
			var points = Points;

			if (card.DisplayNumber == "A")
			{
				switch (HaveA)
				{
					case (HaveA.None):
						if ((points + 11) > 21) HaveA = HaveA.Have;
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
							HaveA = HaveA.Have;
							Points -= 10;
						}
						break;
				}
			}
			else
			{
				if ((HaveA == HaveA.AddPoint) && ((points + cardNumber) > 21))
				{
					HaveA = HaveA.Have;
					Points -= 10;
				}
			}

			return cardNumber;
		}
	}
}
