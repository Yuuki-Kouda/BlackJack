namespace BlackJack
{
	enum PlayerAction
	{
		None,
		Hit,
		Stand
	}

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
		/// プレイヤードローカード
		/// </summary>
		/// <param name="card"></param>
		public virtual void DrawCard(Card card)
		{
			Hand.AddCard(card);
			Hand.CaluculatePoints();
		}
	}
}

