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
		/// プレイヤーコール
		/// </summary>
		public PlayerAction PlayerAction { get; set; }

		/// <summary>
		/// 手札初期化
		/// </summary>
		public void InitializeHand()
		{
			Hand = new Hand();
			PlayerAction = PlayerAction.None;
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

