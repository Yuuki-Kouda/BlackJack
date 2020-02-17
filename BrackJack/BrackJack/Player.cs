namespace BlackJack
{
	/// <summary>
	/// プレイヤー
	/// </summary>
	class Player
	enum PlayerCall
	{
		None,
		Hit,
		Stand
	}
	class Player : Players
	{
		/// <summary>
		/// 手札
		/// プレイヤーコール
		/// </summary>
		public Hand Hand { get; set; } = new Hand();
		public PlayerCall PlayerCall { get; set; } = PlayerCall.None;

		/// <summary>
		/// 手札初期化
		/// プレイヤー初期化
		/// </summary>
		public void InitializeHand()
		public void InitializePlayer()
		{
			Hand = new Hand();
			PlayerCall = PlayerCall.None;
		}

		/// <summary>
		/// プレイヤードローカード
		/// </summary>
		/// <param name="card"></param>
		public override void DrawCard(Card card)
		{
			Hand.AddCard(card);
			Hand.CaluculatePoints();
		}
	}
}

