using System.Collections.Generic;
using System.Linq;

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
		/// バーストしてるかどうか
		/// </summary>
		public bool IsBust 
		{ 
			get
			{
				if (Points > 21) return true;
				else return false;
			} 
		}

		/// <summary>
		/// カード追加
		/// </summary>
		/// <param name="card"></param>
		public void AddCard(Card card)
		{
			HandCards.Add(card);
		}

		/// <summary>
		/// 点数計算
		/// </summary>
		public void CaluculatePoints()
		{
			var aces = HandCards.Where(card => card.Number == 1).ToList();

			if (aces.Any())
			{
				//Aの初期化
				aces.ToList().ForEach(ace => ace.BlackJackNumber = ace.Number);

				var firstAceCard = aces.FirstOrDefault();
				var differenceOfBlackJackNumber = 21 - (HandCards.Sum(card => card.BlackJackNumber) - firstAceCard.BlackJackNumber);

				if (11 <= differenceOfBlackJackNumber) firstAceCard.BlackJackNumber = 11;
			}

			Points = HandCards.Sum(card => card.BlackJackNumber);
		}
	}
}
