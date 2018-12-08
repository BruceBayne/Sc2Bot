namespace EmptyBot.Logic.Basic
{
	public abstract class BaseBlock : ISc2Unit
	{

		protected SC2APIProtocol.Unit BoundUnit = new SC2APIProtocol.Unit();

		public virtual void Update(SC2APIProtocol.Unit updatedUnit)
		{


			BoundUnit = updatedUnit;
			UpdateInternal();

		}

		protected abstract void UpdateInternal();
	}
}