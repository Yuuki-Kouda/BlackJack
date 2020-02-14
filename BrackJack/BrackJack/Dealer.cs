using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
	class Dealer : Player
	{
		/// <summary>
		/// Dealerターン時にカードを引く処理
		/// </summary>
		/// <param name="card"></param>
		/// <param name="isFinishedDraw"></param>
		public void DrawCard(Card card, ref bool isFinishedDraw)
		{
			if (Hand.Points < 17) Hand.AddCard(card);
			else isFinishedDraw = true;
		}
	}
}
