using System.Collections.Generic;
using SC2APIProtocol;
using Action = SC2APIProtocol.Action;

namespace EmptyBot.Logic.Ai
{
	public class RobotAi
	{
		private readonly ResponseGameInfo gameInfo;
		private readonly ResponseData dataInfo;

		public Game game;

		public RobotAi(ResponseGameInfo gameInfoResponse, ResponseData data)
		{
			this.gameInfo = gameInfoResponse;
			dataInfo = data;

			game = new Game(data);
		}


		private int counter = 0;

		public IEnumerable<Action> GetActions(ResponseObservation observation, ResponseGameInfo giResponseGameInfo)
		{
			var actions = new List<Action>();






			if (observation?.Observation?.RawData == null)
			{
				return actions;
			}


			game.UpdateGame(giResponseGameInfo, observation.Observation.RawData.Units);
			actions.AddRange(game.CollectOrders());




			return actions;
		}
	}
}