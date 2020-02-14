using System.Collections.Generic;

namespace BlackJack
{
	class Hand
	{
		/// <summary>
		/// 手札
		/// </summary>
		public List<Card> HandCards { get; set; } = new List<Card>();
		/// <summary>
		/// 点数
		/// </summary>
		public int Points { get; set; } = 0;

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
		public void ConvertBrackJackNumber()
		{
			int points = 0;
			var i = 0;

			foreach (var card in HandCards)
			{
				if (card.DisplayNumber == "A" && card.BlackJackNumber == 1)
				{
					foreach (var handcard in HandCards)
					{
						points += (handcard.BlackJackNumber);
					}
					if(11 <= (21 - (points - 1))) HandCards[i].ConvertA();
					break;
				}
				else if(card.DisplayNumber == "A" && card.BlackJackNumber == 11)
				{
					foreach (var handcard in HandCards)
					{
						points += (handcard.BlackJackNumber);
					}
					if (11 > (21 - (points - 11))) HandCards[i].ConvertA();
					break;
				}
				i++;
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
