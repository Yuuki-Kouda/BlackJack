namespace BrackJack
{
	enum Players
	{
		Player,
		Dealer
	}

	/// <summary>
	/// プレイヤー
	/// </summary>
	class Player
	{
		/// <summary>
		/// 手札
		/// </summary>
		public Hand Hand { get; set; }
		/// <summary>
		/// バースト有無
		/// </summary>
		public bool IsBust { get; set; } = false;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Player()
		{
			Hand hand = new Hand();
			this.Hand = hand;
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
