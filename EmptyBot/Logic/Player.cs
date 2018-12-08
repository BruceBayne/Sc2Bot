using System.Collections.Generic;
using System.Linq;
using SC2APIProtocol;

namespace EmptyBot.Logic
{
	public abstract class Player
	{
		protected abstract Alliance innerAlliance { get; }

		public void Update(IEnumerable<SC2APIProtocol.Unit> rawDataUnits)
		{
			var ownUnits = rawDataUnits.Where(u => u.Alliance == innerAlliance).ToList();
			UpdateInternal(ownUnits);
		}

		protected abstract void UpdateInternal(IReadOnlyCollection<SC2APIProtocol.Unit> ownUnits);
	}
}