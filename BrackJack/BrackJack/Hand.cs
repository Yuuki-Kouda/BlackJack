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
			var firstAceCard = HandCards.FirstOrDefault(card => card.DisplayNumber == "A");

			//Aが見つかった場合
			if (firstAceCard != null)
			{
				var aceCardList = HandCards.Where(card => card.DisplayNumber == "A").ToList();

				foreach(var card in aceCardList) card.BlackJackNumber = 1;

				var differenceOfBlackJackNumber = 21 - (HandCards.Sum(card => card.BlackJackNumber) - 1);

				if (11 <= differenceOfBlackJackNumber) aceCardList[0].BlackJackNumber = 11;
				else aceCardList[0].BlackJackNumber = 1;
			}

			Points = HandCards.Sum(card => card.BlackJackNumber);
		}
	}
}
