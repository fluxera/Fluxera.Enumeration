namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class ShortEnum : Enumeration<ShortEnum, short>
	{
		public static readonly ShortEnum One = new ShortEnum(1, "One");

		/// <inheritdoc />
		private ShortEnum(short value, string name)
			: base(value, name)
		{
		}
	}
}
