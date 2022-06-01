namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class IntEnum : Enumeration<IntEnum, int>
	{
		public static readonly IntEnum One = new IntEnum(1, "One");

		/// <inheritdoc />
		private IntEnum(int value, string name)
			: base(value, name)
		{
		}
	}
}
