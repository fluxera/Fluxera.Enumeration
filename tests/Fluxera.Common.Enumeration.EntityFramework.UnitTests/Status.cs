namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	public class Status : Enumeration<Status, int>
	{
		public static readonly Status Active = new Status(1, nameof(Active));
		public static readonly Status Deleted = new Status(2, nameof(Deleted));
		public static readonly Status Discontinued = new Status(3, nameof(Discontinued));
		public static readonly Status Inactive = new Status(4, nameof(Inactive));
		public static readonly Status Unknown = new Status(0, nameof(Unknown));

		private Status(int value, string name)
			: base(value, name)
		{
		}
	}
}
