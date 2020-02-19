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
		/// ディーラーが開始時ドローを引いている最中、または引いた直後か
		/// </summary>
		public bool IsFinishedStartDealerDraw 
		{ 
			get 
			{
				if (Hand.HandCards.Count > Two) return true;
				else return false;
			}
		}
		/// <summary>
		/// ディーラーの点数は17以上か
		/// </summary>
		public bool IsFinishedDealerTurn 
		{
			get
			{
				if (Hand.Points >= Seventeen) return true;
				else return false;
			}
		}

		//定数
		private readonly int Two = 2;
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
