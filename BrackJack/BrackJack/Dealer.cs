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
		/// Dealerがカードを引く処理
		/// </summary>
		/// <param name="card"></param>
		/// <param name="points"></param>
		/// <returns></returns>
		public bool IsDrawDealerOverFlow17(Card card, int points)
		{
			if (points >= 17) return false;
			else 
			{
				DrawCard(card);
				return true;
			}
		}
	}
}
