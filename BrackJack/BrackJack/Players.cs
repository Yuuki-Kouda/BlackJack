namespace BlackJack
{
	/// <summary>
	/// プレイヤー
	/// </summary>
	abstract class Players
	{
		/// <summary>
		/// 手札
		/// </summary>
		public Hand Hand { get; set; } = new Hand();

		/// <summary>
		/// ドローカード
		/// </summary>
		abstract public void DrawCard(Card card);
	}
}
