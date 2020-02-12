﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BrackJack
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
		/// コンストラクタ
		/// </summary>
		public Deck()
		{
			InitializeDeckList();
		}

		/// <summary>
		/// トランプ生成
		/// </summary>
		/// <param name="mark"></param>
		/// <param name="number"></param>
		/// <returns></returns>
		private Card CreateCard(Suit mark, int number)
		{
			Card card = new Card(mark, number);
			return card;
		}

		/// <summary>
		/// 山札初期化
		/// </summary>
		private void InitializeDeckList()
		{
			List<Card> cardList = new List<Card>();

			for (int i = 1; i <= 13; i++)
			{
				cardList.Add(CreateCard(Suit.Hart, i));
				cardList.Add(CreateCard(Suit.Spade, i));
				cardList.Add(CreateCard(Suit.Diamond, i));
				cardList.Add(CreateCard(Suit.Club, i));
			}

			//シャッフル
			DeckList = cardList.OrderBy(i => Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// ドロー
		/// </summary>
		/// <returns></returns>
		public Card DrawnCard()
		{
			var drawnCard = DeckList[0]; 
			DeckList.RemoveAt(0);

			return drawnCard;
		}
	}
}