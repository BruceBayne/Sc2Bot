using System;
using EmptyBot.Logic.Basic;
using EmptyBot.Logic.StrategyTypes;
using EmptyBot.Types;
using Sc2Api.CsTypes.Common;
using SC2APIProtocol;
using Action = SC2APIProtocol.Action;

namespace EmptyBot.Logic
{



	public class ControlAbleUnit : Unit
	{
		public HighLevelOrder HighLevelOrder;


		public ControlAbleUnit() : base()
		{

		}


		private int lastOrderTime;


		protected Action CreateRawUnitCommand(uint ability)
		{
			var action = new Action
			{
				ActionRaw = new ActionRaw
				{
					UnitCommand = new ActionRawUnitCommand
					{
						AbilityId = (int)ability,
						UnitTags = { UniqueId }
					},
				}
			};
			return action;

		}

		protected Action CreateRawUnitCommand(Abilities ability)
		{

			return CreateRawUnitCommand((uint)ability);

		}
		public void HoldPosition()
		{
		}



		public void SetupHighOrder(HighLevelOrder highLevelOrder)
		{
			this.HighLevelOrder = highLevelOrder;
		}




		protected Action CurrentAction;



		private void MoveAt(Point2D position)
		{

			var action = CreateRawUnitCommand(Abilities.Move);
			action.ActionRaw.UnitCommand.TargetWorldSpacePos = position;
			CurrentAction = action;


		}



		public bool TryGetAction(out Action action)
		{


			action = null;


			if (ProbablyToFastOrders())
				return false;


			if (HighLevelOrder.Goal == Goal.Idle)
				return false;


			if (HighLevelOrder.Goal == Goal.Scout)
			{
				//MoveAt(latestGameInfo.StartRaw.StartLocations[0]);
			}


			if (CurrentAction != null)
			{
				action = CurrentAction;
				lastOrderTime = Environment.TickCount;
				return true;

			}

			return false;
		}

		private bool ProbablyToFastOrders()
		{
			return Environment.TickCount - lastOrderTime < 500;
		}

	}



	public class Unit : BaseBlock, IEquatable<Unit>
	{
		public Tag UniqueId { get; private set; }


		public override string ToString()
		{
			return BoundUnit.ToString();
		}

		public Unit()
		{

		}


		protected override void UpdateInternal()
		{
			if (UniqueId.Id == 0)
				UniqueId = new Tag(BoundUnit.Tag);
		}



		#region Equtable

		public bool Equals(Unit other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return UniqueId.Equals(other.UniqueId);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Unit)obj);
		}

		public override int GetHashCode()
		{
			return UniqueId.GetHashCode();
		}

		#endregion



	}
}