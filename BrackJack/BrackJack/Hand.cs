using System.Collections.Generic;

namespace BlackJack
{
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
			ConvertBrackJackNumber();
			CaluculatePoints();
			if (Points > 21) return true;

			return false;
		}

		/// <summary>
		/// 点数の変換
		/// </summary>
		private void ConvertBrackJackNumber()
		{
			var handCards = HandCards;
			bool hasA = false;
			int points = new int();
			var i = 0;
			foreach (var card in HandCards)
			{
				if (card.DisplayNumber == "A")
				{
					hasA = true;
					break;
				}
				i++;
			}

			if (hasA)
			{
				handCards.RemoveAt(i);
				foreach (var card in handCards)
				{
					points += card.BlackJackNumber;
				}

				if (((11 > points) && (HandCards[i].BlackJackNumber == 11)) ||
					((11 <= points) && (HandCards[i].BlackJackNumber == 1)))
					HandCards[i].ConvertA();
			}
		}

		/// <summary>
		/// 点数計算
		/// </summary>
		private void CaluculatePoints()
		{
			InitializePoints();

			foreach(var card in HandCards)
			{
				Points += card.BlackJackNumber;
			}
		}

		private void InitializePoints()
		{
			Points = 0;
		}
	}
}
