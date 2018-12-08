using System.Collections.Generic;
using EmptyBot.Logic.Basic;
using EmptyBot.Logic.Basic.Buildings;
using Sc2Api.CsTypes;

namespace EmptyBot.Logic.Buildings
{
	public sealed class TerranBarack : ControlAbleBuilding
	{

		public void Train(Terran.Barack.Units unit)
		{
			base.Train(unit);
		}
	}
}