﻿namespace BlackJack
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
		public int BlackJackNumber { get; set; }
		/// <summary>
		/// トランプのマーク
		/// </summary>
		public Suit Mark { get; }

		//定数
		public int SpecialAcePoint = 11;

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
					this.BlackJackNumber = number;
					break;

				case 11:
					this.DisplayNumber = "J";
					this.BlackJackNumber = 10;
					break;

				case 12:
					this.DisplayNumber = "Q";
					this.BlackJackNumber = 10;
					break;

				case 13:
					this.DisplayNumber = "K";
					this.BlackJackNumber = 10;
					break;

				default:
					this.DisplayNumber = number.ToString();
					this.BlackJackNumber = number;
					break;
			}
		}
	}
}
