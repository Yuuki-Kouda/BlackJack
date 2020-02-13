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
		/// Aを11としたときに点数が21を超えているか
		/// </summary>
		private bool IsOverFlowByA { get; set; } = false;

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
			CaluculatePoints(convetedNumberPoint);
			ConvertBrackJackNumber();
			CaluculatePoints();
			if (Points > 21) return true;

			return false;
		}

		/// <summary>
		/// 点数変換
		/// 点数の変換
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		private int ReturnConvertedNumber(Card card)
		private void ConvertBrackJackNumber()
		{
			var convertedNumber = card.Number;

			convertedNumber = ReturnConvetedJQK(card);

			if ((HaveA != HaveA.None) || (card.DisplayNumber == "A"))
			var handCards = HandCards;
			int points = new int();
			var i = 0;
			foreach (var card in HandCards)
			{
				convertedNumber = ReturnConvetedAAndRecalucatePoints(convertedNumber, card);
				if (card.DisplayNumber == "A")
					break;
				i++;
			}

			return convertedNumber;
		}
		/// <summary>
		/// 点数計算処理
		/// </summary>
		/// <param name="convetedNumberPoint"></param>
		private void CaluculatePoints(int convetedNumberPoint)
		{
			if (IsOverFlowByA)
			handCards.RemoveAt(i);
			foreach(var card in handCards)
			{
				Points -= 10;
				Points += convetedNumberPoint;
				IsOverFlowByA = false;
				points += card.BlackJackNumber;
			}
			else Points += convetedNumberPoint;

			return;
			if (((11 > points) && (HandCards[i].BlackJackNumber == 11)) ||
				((11 <= points) && (HandCards[i].BlackJackNumber == 1))) 
				HandCards[i].ConvertA();
		}

		/// <summary>
		/// JQKを変換して返す
		/// 点数計算
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
		/// Aを変換して返す
		/// </summary>
		public int ReturnConvetedAAndRecalucatePoints(int cardNumber, Card card)
		private void CaluculatePoints()
		{
			var points = Points;
			InitializePoints();

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
							IsOverFlowByA = true;
						}
						break;
				}
			}
			else
			foreach(var card in HandCards)
			{
				if ((HaveA == HaveA.AddPoint) && ((points + cardNumber) > 21))
				{
					HaveA = HaveA.Have;
					IsOverFlowByA = true;
				}
				Points += card.BlackJackNumber;
			}
		}

			return cardNumber;
		private void InitializePoints()
		{
			Points = 0;
		}
	}
}
