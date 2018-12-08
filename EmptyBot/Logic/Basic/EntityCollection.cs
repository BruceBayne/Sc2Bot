using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EmptyBot.Logic.Buildings;
using EmptyBot.Logic.StrategyTypes;
using EmptyBot.Types;
using Sc2Api.CsTypes;

namespace EmptyBot.Logic.Basic
{
	public class EntityCollection<TItem> : IEnumerable<TItem> where TItem : ISc2Unit, new()
	{
		private readonly UnitType desiredType;


		Dictionary<int, Type> cache = new Dictionary<int, Type>()
		{
			{(int)Terran.Buildings.CommandCenter,typeof(TerranCommandCenter)},
			{(int)Terran.Buildings.SupplyDepot,typeof(TerranSupplyDepot)},






		};


		public EntityCollection(UnitType desiredType)
		{
			this.desiredType = desiredType;
		}


		private readonly Dictionary<Tag, TItem> items = new Dictionary<Tag, TItem>();

		public void Update(IReadOnlyCollection<SC2APIProtocol.Unit> sc2Units)
		{
			var compatibleCollectionUnits = sc2Units.Where(x => x.CompatibleTo(desiredType));

			items.Clear();


			foreach (var unit in compatibleCollectionUnits)
			{
				var uTag = new Tag(unit.Tag);

				if (!items.ContainsKey(uTag))
					items.Add(uTag, new TItem());

				items[uTag].Update(unit);
			}
		}

		public IEnumerator<TItem> GetEnumerator()
		{
			return items.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<T> LocateAll<T>() where T : TItem
		{
			return items.Values.Where(x => x is T).OfType<T>();
		}
	}
}