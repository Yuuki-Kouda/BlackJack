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
		/// ディーラーの点数は17以上か
		/// </summary>
		public bool IsFinishedDealerDraw 
		{
			get
			{
				if (Hand.Points >= Seventeen) return true;
				else return false;
			}
		}

		//定数
		private readonly int Seventeen = 17;

		/// <summary>
		/// ディーラーがカードを引く処理
		/// </summary>
		/// <param name="card"></param>
		/// <param name="isFinishedDraw"></param>
		public override void DrawCard(Card card)
		{
			Hand.AddCard(card);
			Hand.CaluculatePoints();
		}
	}
}
