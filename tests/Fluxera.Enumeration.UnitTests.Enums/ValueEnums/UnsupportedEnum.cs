namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class UnsupportedEnum : Enumeration<UnsupportedEnum, double>
	{
		public static readonly UnsupportedEnum One = new UnsupportedEnum(12.55, "One");

		/// <inheritdoc />
		private UnsupportedEnum(double value, string name)
			: base(value, name)
		{
		}
	}
}
