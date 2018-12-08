using EmptyBot.Logic.Basic;
using Sc2Api.CsTypes;

namespace EmptyBot.Logic.StrategyTypes
{
	public static class Ext
	{
		public static bool IsRegularUnit(this SC2APIProtocol.Unit unit)
		{
			if (unit.UnitType == (uint)Terran.CommandCenter.Units.Scv)
			{
				return true;
			}

			return false;
		}


		public static bool IsResource(this SC2APIProtocol.Unit unit)
		{
			return false;
		}

		public static bool CompatibleTo(this SC2APIProtocol.Unit unit, UnitType desiredType)
		{
			if (unit.UnitType == (uint)Terran.Buildings.CommandCenter)
			{
				if (desiredType == UnitType.Building)
					return true;
			}


			if (unit.UnitType == (uint)Terran.CommandCenter.Units.Scv)
			{
				if (desiredType == UnitType.Regular)
					return true;
			}


			return false;
		}


		public static bool IsBuilding(this SC2APIProtocol.Unit unit)
		{
			if (unit.UnitType == (uint)Terran.Buildings.CommandCenter)
			{
				return true;
			}

			return false;
		}
	}
}