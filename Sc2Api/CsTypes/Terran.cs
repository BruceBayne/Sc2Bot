namespace Sc2Api.CsTypes
{
	public static class Terran
	{
		public enum Buildings : int
		{
			CommandCenter = 18,
			SupplyDepot = 19,

		}


		public class CommandCenter
		{
			public Buildings Id => Buildings.CommandCenter;

			public enum Units : int
			{
				Scv = 45,
			}
		}


		public static class Barack
		{
			public enum Units : int
			{
				Marine = 48,
				Reaper = 49,
				Ghost = 50,
				Marauder = 51,
			}
		}
	}
}