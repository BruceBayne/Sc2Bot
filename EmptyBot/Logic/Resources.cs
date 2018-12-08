using System.Collections.Generic;
using System.Linq;
using EmptyBot.Logic.Basic;
using SC2APIProtocol;

namespace EmptyBot.Logic
{



	public class Minerals : ISc2Unit
	{
		public void Update(SC2APIProtocol.Unit unit)
		{



		}
	}

	public class Vespene : ISc2Unit
	{
		public void Update(SC2APIProtocol.Unit unit)
		{



		}
	}




	public class Resources
	{

		public readonly EntityCollection<Minerals> Minerals = new EntityCollection<Minerals>(UnitType.ResourceMinerals);
		public readonly EntityCollection<Minerals> Vespene = new EntityCollection<Minerals>(UnitType.ResourceMinerals);



		public void Update(IReadOnlyCollection<SC2APIProtocol.Unit> units)
		{

			var compatibleUnits = units.Where(x => x.Alliance == Alliance.Neutral).ToList();

			Minerals.Update(compatibleUnits);
			Vespene.Update(compatibleUnits);

		}
	}
}