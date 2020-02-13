namespace BlackJack
{
	/// <summary>
	/// プレイヤー
	/// </summary>
	class Player
	{
		/// <summary>
		/// 手札
		/// </summary>
		public Hand Hand { get; set; } = new Hand();
		/// <summary>
		/// バースト有無
		/// </summary>
		public bool IsBust { get; set; } = false;

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
		public void DrawCard(Card card)
		{
			IsBust = Hand.AddCard(card);
			return;
		}
	}
}
