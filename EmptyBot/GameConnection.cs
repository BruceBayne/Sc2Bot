using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using EmptyBot.Logic.Ai;
using SC2APIProtocol;

namespace EmptyBot
{
	public class GameConnection
	{
		ProtobufProxy proxy = new ProtobufProxy();

		private RobotAi ai;

		string address = "192.168.11.33";


		public GameConnection()
		{
		}


		public async Task Connect(int port)
		{
			for (int i = 0; i < 40; i++)
			{
				try
				{
					await proxy.Connect(address, port);
					return;
				}
				catch (WebSocketException e)
				{
				}

				Thread.Sleep(2000);
			}

			throw new Exception("Unable to make a connection.");
		}

		public async Task CreateGame(string mapName, Race opponentRace, Difficulty opponentDifficulty)
		{
			RequestCreateGame createGame = new RequestCreateGame
			{
				Realtime = false,
				LocalMap = new LocalMap { MapPath = mapName }
			};


			var player1 = new PlayerSetup();
			createGame.PlayerSetup.Add(player1);
			player1.Type = PlayerType.Participant;

			var player2 = new PlayerSetup();
			createGame.PlayerSetup.Add(player2);
			player2.Race = opponentRace;
			player2.Type = PlayerType.Computer;
			player2.Difficulty = opponentDifficulty;

			Request request = new Request { CreateGame = createGame };
			Response response = await proxy.SendRequest(request);
		}


		public async Task<uint> JoinGame(Race race)
		{
			RequestJoinGame joinGame = new RequestJoinGame
			{
				Race = race,
				Options = new InterfaceOptions { Raw = true, Score = true }
			};


			Request request = new Request { JoinGame = joinGame };
			Response response = await proxy.SendRequest(request);
			return response.JoinGame.PlayerId;
		}

		public async Task<uint> JoinGameLadder(Race race, int startPort)
		{
			RequestJoinGame joinGame = new RequestJoinGame
			{
				Race = race,
				SharedPort = startPort + 1,
				ServerPorts = new PortSet { GamePort = startPort + 2, BasePort = startPort + 3 }
			};


			joinGame.ClientPorts.Add(new PortSet());
			joinGame.ClientPorts[0].GamePort = startPort + 4;
			joinGame.ClientPorts[0].BasePort = startPort + 5;

			joinGame.Options = new InterfaceOptions { Raw = true, Score = true };

			Request request = new Request { JoinGame = joinGame };

			Response response = await proxy.SendRequest(request);
			return response.JoinGame.PlayerId;
		}

		public async Task Ping()
		{
			await proxy.Ping();
		}

		public async Task RequestLeaveGame()
		{
			var requestLeaveGame = new Request { LeaveGame = new RequestLeaveGame() };
			await proxy.SendRequest(requestLeaveGame);
		}

		public async Task SendRequest(Request request)
		{
			await proxy.SendRequest(request);
		}

		public async Task<ResponseQuery> SendQuery(RequestQuery query)
		{
			var request = new Request { Query = query };
			var response = await proxy.SendRequest(request);
			return response.Query;
		}

		public async Task Run()
		{
			while (true)
			{
				var observationRequest = new Request { Observation = new RequestObservation() };
				var giRequest = new Request { GameInfo = new RequestGameInfo() };

				var response = await proxy.SendRequest(observationRequest);
				var giResponse = await proxy.SendRequest(giRequest);


				var observation = response.Observation ?? giResponse.Observation;
				var gameInfo = giResponse.GameInfo ?? response.GameInfo;


				if (observation != null && gameInfo != null)
				{
					if (response.Status == Status.Ended || response.Status == Status.Quit)
					{
						//bot.OnEnd(gameInfoResponse.GameInfo, observation, playerId, observation.PlayerResult[(int)playerId - 1].Result);
						break;
					}


					var actions = ai.GetActions(observation, gameInfo);
					var actionRequest = new Request { Action = new RequestAction() };
					actionRequest.Action.Actions.AddRange(actions);
					await proxy.SendRequest(actionRequest);


					var stepRequest = new Request { Step = new RequestStep { Count = 1 } };
					await proxy.SendRequest(stepRequest);
				}
			}
		}

		public async Task RunSinglePlayer(string map, Race myRace, Race opponentRace, Difficulty opponentDifficulty)
		{
			await Connect(5678);


			bool createNewGame = false;


			if (createNewGame)
			{
				await CreateGame(map, opponentRace, opponentDifficulty);
				var playerId = await JoinGame(myRace);
			}

			var dataReq = new Request
			{
				Data = new RequestData
				{
					UnitTypeId = true,
					AbilityId = true,
					BuffId = true,
					EffectId = true,
					UpgradeId = true
				},
			};
			var dataResponse = await proxy.SendRequest(dataReq);
			var gameInfoResponse = await proxy.SendRequest(new Request { GameInfo = new RequestGameInfo() });
			ai = new RobotAi(gameInfoResponse.GameInfo, dataResponse.Data);


			await Run();
		}

		public async Task RunLadder(Race myRace, int startPort)
		{
			await Connect(5678);
			uint playerId = await JoinGameLadder(myRace, startPort);
			await Run();
			await RequestLeaveGame();
		}
	}
}