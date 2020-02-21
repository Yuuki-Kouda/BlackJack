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
		public List<Card> DeckList { get; set; } = new List<Card>();

		//定数
		public readonly int DeckNumberCanStartGame = 24;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Deck()
		{
			InitializeDeckList();
		}

		/// <summary>
		/// デッキの初期化が必要であれば初期化する
		/// </summary>
		public void InitializeDeckListIfNeeded()
		{
			if (DeckList.Count < DeckNumberCanStartGame) 
				InitializeDeckList();
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
