using System.Collections.Generic;
using System.Linq;
using EmptyBot.Logic.Basic;
using EmptyBot.Logic.Basic.Buildings;
using EmptyBot.Logic.Buildings;
using EmptyBot.Logic.StrategyTypes;
using Sc2Api.CsTypes;
using SC2APIProtocol;
using Action = SC2APIProtocol.Action;

namespace EmptyBot.Logic
{
	public sealed class Game
	{
		public static ResponseData StaticInfo { get; private set; }
		private ResponseGameInfo latestGameInfo = new ResponseGameInfo();


		public readonly Resources Resources = new Resources();
		public readonly Self Self = new Self();
		public readonly Enemy Enemy = new Enemy();


		public Game(ResponseData data)
		{
			StaticInfo = data;
		}

		public void UpdateGame(ResponseGameInfo gameInfo, IReadOnlyCollection<SC2APIProtocol.Unit> sc2Units)
		{
			if (latestGameInfo == null)
				return;

			latestGameInfo = gameInfo;

			Resources.Update(sc2Units);
			Enemy.Update(sc2Units);
			Self.Update(sc2Units);




			foreach (var cc in Self.Buildings.LocateAll<ControlAbleBuilding>())
			{
				cc.Train(Terran.CommandCenter.Units.Scv);
			}



			Self.Units.First().SetupHighOrder(new HighLevelOrder()
			{ Goal = Goal.Scout, Duration = Duration.AsLongAsPossible, Area = MapArea.EnemyBase });
		}

		public IEnumerable<Action> CollectOrders()
		{
			return Self.GetOrders();
		}
	}
}