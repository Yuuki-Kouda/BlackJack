using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
	class Dealer : AbstractPlayer
	{
		/// <summary>
		/// ディーラーが引き終わったか
		/// </summary>
		public bool IsFinishedDraw 
		{
			get
			{
				if (Hand.Points >= 17) return true;
				else return false;
			}
		}

		/// <summary>
		/// ディーラー初期化
		/// </summary>
		public void InitializeDealer()
		{
			Hand = new Hand();
		}

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
