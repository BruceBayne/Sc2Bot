using EmptyBot.Logic.Basic.Buildings;
using Sc2Api.CsTypes;

namespace EmptyBot.Logic.Buildings
{
	public sealed class TerranCommandCenter : ControlAbleBuilding
	{

		public void Train(Terran.CommandCenter.Units unit)
		{
			base.Train(unit);
		}
	}


	public sealed class TerranSupplyDepot : ControlAbleBuilding
	{


	}

}