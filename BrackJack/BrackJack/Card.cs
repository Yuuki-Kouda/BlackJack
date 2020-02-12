using System;
using System.Collections.Generic;
using System.Text;

namespace BrackJack
{
	/// <summary>
	/// スート
	/// </summary>
	public enum Suit
	{
		Hart,
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
		public int Number { get; set; }
		/// <summary>
		/// 数字（表示用）
		/// </summary>
		public string DisplayNumber { get; }
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
					break;

				case 11:
					this.DisplayNumber = "J";
					break;

				case 12:
					this.DisplayNumber = "Q";
					break;

				case 13:
					this.DisplayNumber = "K";
					break;

				default:
					this.DisplayNumber = number.ToString();
					break;
			}
		}
	}
}
