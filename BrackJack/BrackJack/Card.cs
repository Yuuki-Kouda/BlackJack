namespace BlackJack
{
	/// <summary>
	/// スート
	/// </summary>
	public enum Suit
	{
		Heart,
		Spade,
		Diamond,
		Club
	}

	/// <summary>
	/// トランプカードクラス
	/// </summary>
	class Card
	{
		/// <summary>
		/// 数字
		/// </summary>
		public int Number { get; }
		/// <summary>
		/// 数字（表示用）
		/// </summary>
		public string DisplayNumber { get; }
		/// <summary>
		/// カードの点数
		/// </summary>
		public int BlackJackNumber { get; private set; }
		/// <summary>
		/// トランプのマーク
		/// </summary>
		public Suit Mark { get; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="mark"></param>
		/// <param name="number"></param>
		public Card(Suit mark, int number)
		{
			this.Number = number;
			this.Mark = mark;

			switch (number)
			{
				case 1:
					this.DisplayNumber = "A";
					SetBlackJackNumber(number);
					break;

				case 11:
					this.DisplayNumber = "J";
					SetBlackJackNumber(10);
					break;

				case 12:
					this.DisplayNumber = "Q";
					SetBlackJackNumber(10);
					break;

				case 13:
					this.DisplayNumber = "K";
					SetBlackJackNumber(10);
					break;

				default:
					this.DisplayNumber = number.ToString();
					SetBlackJackNumber(number);
					break;
			}
		}

		/// <summary>
		/// 点数変換
		/// </summary>
		/// <param name="card"></param>
		public void SetBlackJackNumber(int number)
		{
			BlackJackNumber = number;
		}
	}
}
