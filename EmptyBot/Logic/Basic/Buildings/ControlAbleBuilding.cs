using System;
using SC2APIProtocol;

namespace EmptyBot.Logic.Basic.Buildings
{
	public class ControlAbleBuilding : ControlAbleUnit
	{


		public bool IsBuildInProgress => this.BoundUnit.CargoSpaceTaken == 0;


		public void SetRally(Point2D rally)
		{


		}


		public void TrainRaw(int unit)
		{



			if (IsBuildInProgress)
				return;



			var t = Game.StaticInfo.Units[unit].AbilityId;
			CurrentAction = CreateRawUnitCommand(t);
		}

		public void Train(Enum unit)
		{
			var val = (int)Convert.ChangeType(unit, unit.GetTypeCode());
			TrainRaw(val);
		}





	}
}