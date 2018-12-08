using System;
using SC2APIProtocol;

namespace EmptyBot
{
	class Program
	{
		private static string mapName = @"/Users/jerald/Downloads/Ladder2018Season2/(2)AcidPlantLE.SC2Map";
		private static Race opponentRace = Race.Random;
		private static Difficulty opponentDifficulty = Difficulty.VeryEasy;


		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");


			var gc = new GameConnection();
			//gc.RunLadder(Race.Terran, 32166).Wait();
			gc.RunSinglePlayer(mapName, Race.Terran, Race.Random, Difficulty.VeryEasy).Wait();

		}
	}
}
