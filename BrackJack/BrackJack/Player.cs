namespace BlackJack
{
	class Player
	{
		/// <summary>
		/// 手札
		/// </summary>
		public Hand Hand { get; set; }

		/// <summary>
		/// 手札初期化
		/// </summary>
		public void InitializeHand()
		{
			Hand = new Hand();
		}

		/// <summary>
		/// ドローカード
		/// </summary>
		/// <param name="card"></param>
		public void DrawCard(Deck deck)
		{			
			Hand.AddCard(deck.DrawCard());
			Hand.CaluculatePoints();
		}
	}
}

