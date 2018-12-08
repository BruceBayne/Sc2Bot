using System.Collections.Generic;
using EmptyBot.Logic.Basic.Buildings;
using EmptyBot.Logic.Buildings;
using SC2APIProtocol;

namespace EmptyBot.Logic.Basic
{
	public class Enemy : Player
	{
		public EntityCollection<NonControllableBuilding> Buildings = new EntityCollection<NonControllableBuilding>(UnitType.Building);
		public readonly EntityCollection<Unit> Units = new EntityCollection<Unit>(UnitType.Regular);


		protected override void UpdateInternal(IReadOnlyCollection<SC2APIProtocol.Unit> ownUnits)
		{
			Buildings.Update(ownUnits);
			Units.Update(ownUnits);
		}

		protected override Alliance innerAlliance { get; } = Alliance.Enemy;
	}
}