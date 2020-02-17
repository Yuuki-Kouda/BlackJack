namespace BlackJack
{
	enum PlayerAction
	{
		None,
		Hit,
		Stand
	}
	class Player : Players
	{
		/// <summary>
		/// プレイヤーコール
		/// </summary>
		public PlayerAction PlayerAction { get; set; } = PlayerAction.None;

		/// <summary>
		/// プレイヤー初期化
		/// </summary>
		public void InitializePlayer()
		{
			Hand = new Hand();
			PlayerAction = PlayerAction.None;
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

