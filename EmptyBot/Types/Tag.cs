using System;

namespace EmptyBot.Types
{
	public struct Tag : IEquatable<Tag>
	{
		public ulong Id;

		public Tag(ulong id)
		{
			Id = id;
		}


		public static implicit operator ulong(Tag d)
		{
			return d.Id;
		}

		public bool Equals(Tag other)
		{
			return Id == other.Id;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Tag other && Equals(other);
		}

		public override int GetHashCode()
		{
			return (int)Id;
		}
	}
}