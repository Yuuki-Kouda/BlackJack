using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack
{
	/// <summary>
	/// 山札クラス
	/// </summary>
	class Deck
	{
		/// <summary>
		/// 山札
		/// </summary>
		List<Card> DeckList { get; set; }
		/// <summary>
		/// 山札にカードがあるかどうか
		/// </summary>
		public bool HasDeckRunOut
		{
			get
			{
				if (DeckList.Count == 0) return true;
				else return false;
			}
		}

		//定数
		public readonly int MinimumNumberOfCard = 24;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Deck()
		{
			InitializeDeckList();
		}

		/// <summary>
		/// デッキのシャッフルが必要であればシャッフルする
		/// </summary>
		public void AttemptInitializeDeckList()
		{
			if (DeckList.Count < MinimumNumberOfCard) InitializeDeckList();
		}

		/// <summary>
		/// 山札初期化
		/// </summary>
		public void InitializeDeckList()
		{
			List<Card> cardList = new List<Card>();

			for (int i = 1; i <= 13; i++)
			{
				Card heartCard = new Card(Suit.Heart, i);
				Card spadeCard = new Card(Suit.Spade, i);
				Card diamondCard = new Card(Suit.Diamond, i);
				Card clubCard = new Card(Suit.Club, i);

				cardList.Add(heartCard);
				cardList.Add(spadeCard);
				cardList.Add(diamondCard);
				cardList.Add(clubCard);
			}
			//シャッフル
			DeckList = cardList.OrderBy(i => Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// ドロー
		/// </summary>
		/// <returns></returns>
		public Card DrawCard()
		{
			var card = DeckList.FirstOrDefault();
			DeckList.Remove(card);

			return card;
		}
	}
}
