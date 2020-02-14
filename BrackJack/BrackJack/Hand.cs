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
		/// バースト有無
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
			ConvertAcesBrackJackNumber();
			CaluculatePoints();
		}

		/// <summary>
		/// 点数の変換
		/// </summary>
		public void ConvertAcesBrackJackNumber()
		{
			var points = 0;
			var hasAce = false;
			int aceIndex = new int();
			var i = 0;

			foreach (var card in HandCards)
			{
				//Aの要素番号の特定
				if (!hasAce && card.DisplayNumber == "A")
				{
					aceIndex = i;
					hasAce = true;
				}
				else points += card.BlackJackNumber;

				i++;
			}

			if (hasAce)
			{
				//Aの点数を11にする
				if (11 <= 21 - points) HandCards[aceIndex]
										.SetBlackJackNumberToOneOrEleven(true);
				//Aの点数を1にする
				else HandCards[aceIndex]
						.SetBlackJackNumberToOneOrEleven(false);
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
