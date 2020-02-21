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
				if (Points > BlackJackPoints) 
					return true;
				else 
					return false;
			} 
		}

		//定数
		public readonly int BlackJackPoints = 21;

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
			var aces = HandCards.Where(card => card.Number == 1);
			if (aces.Any())
			{
				//Aの初期化
				aces.ToList().ForEach(ace => ace.BlackJackNumber = ace.Number);

				var firstAceCard = aces.First();
				var differenceOfBlackJackNumber 
					= BlackJackPoints - (HandCards.Sum(card => card.BlackJackNumber) - firstAceCard.BlackJackNumber);
				if (firstAceCard.SpecialAcePoint <= differenceOfBlackJackNumber)
				{
					firstAceCard.BlackJackNumber = firstAceCard.SpecialAcePoint;
				}
			}

			Points = HandCards.Sum(card => card.BlackJackNumber);
		}
	}
}
