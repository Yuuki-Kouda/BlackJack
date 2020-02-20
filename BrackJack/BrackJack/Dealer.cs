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
		public bool CanDraw 
		{
			get
			{
				if (Hand.Points >= PointsDealerCanDraw) return true;
				else return false;
			}
		}

		//定数
		private readonly int PointsDealerCanDraw = 17;
	}
}
