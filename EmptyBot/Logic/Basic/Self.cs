using System.Collections.Generic;
using System.Numerics;
using EmptyBot.Logic.Basic.Buildings;
using EmptyBot.Logic.Buildings;
using Sc2Api.CsTypes.Common;
using SC2APIProtocol;

namespace EmptyBot.Logic.Basic
{
	public class Self : Player
	{

		public readonly EntityCollection<ControlAbleBuilding> Buildings = new EntityCollection<ControlAbleBuilding>(UnitType.Building);
		public readonly EntityCollection<ControlAbleUnit> Units = new EntityCollection<ControlAbleUnit>(UnitType.Regular);


		protected override void UpdateInternal(IReadOnlyCollection<SC2APIProtocol.Unit> ownUnits)
		{
			Units.Update(ownUnits);
			Buildings.Update(ownUnits);

		}


		public IReadOnlyCollection<Action> GetOrders()
		{

			var actions = new List<Action>();

			foreach (var unit in Units)
				if (unit.TryGetAction(out var action))
					actions.Add(action);

			foreach (var building in Buildings)
				if (building.TryGetAction(out var action))
					actions.Add(action);

			return actions;

		}

		protected override Alliance innerAlliance { get; } = Alliance.Self;
	}






	public class Builder
	{
		ResponseData data;

		public void SetGameInfo(ResponseData data)
		{


		}


		public string GetUnitName(uint unitType)
		{


			return data.Units[(int)unitType].Name;
		}

		public static void Construct(uint unitType)
		{
			//Vector3 startingSpot;

			//var resourceCenters = GetUnits(Units.ResourceCenters);
			//if (resourceCenters.Count > 0)
			//	startingSpot = resourceCenters[0].position;
			//else
			//{
			//	Logger.Error("Unable to construct: {0}. No resource center was found.", GetUnitName(unitType));
			//	return;
			//}

			//const int radius = 12;

			////trying to find a valid construction spot
			//var mineralFields = GetUnits(Units.MineralFields, onlyVisible: true, alliance: Alliance.Neutral);
			//Vector3 constructionSpot;
			//while (true)
			//{
			//	constructionSpot = new Vector3(startingSpot.X + random.Next(-radius, radius + 1), startingSpot.Y + random.Next(-radius, radius + 1), 0);

			//	//avoid building in the mineral line
			//	if (IsInRange(constructionSpot, mineralFields, 5)) continue;

			//	//check if the building fits
			//	if (!CanPlace(unitType, constructionSpot)) continue;

			//	//ok, we found a spot
			//	break;
			//}

			//var worker = GetAvailableWorker(constructionSpot);
			//if (worker == null)
			//{
			//	Logger.Error("Unable to find worker to construct: {0}", GetUnitName(unitType));
			//	return;
			//}

			//var abilityID = Abilities.GetID(unitType);
			//var constructAction = CreateRawUnitCommand(abilityID);
			//constructAction.ActionRaw.UnitCommand.UnitTags.Add(worker.tag);
			//constructAction.ActionRaw.UnitCommand.TargetWorldSpacePos = new Point2D();
			//constructAction.ActionRaw.UnitCommand.TargetWorldSpacePos.X = constructionSpot.X;
			//constructAction.ActionRaw.UnitCommand.TargetWorldSpacePos.Y = constructionSpot.Y;
			//AddAction(constructAction);

			//Logger.Info("Constructing: {0} @ {1} / {2}", GetUnitName(unitType), constructionSpot.X, constructionSpot.Y);
		}


	}








}