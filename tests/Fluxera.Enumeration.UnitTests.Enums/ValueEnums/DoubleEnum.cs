namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class DoubleEnum : Enumeration<DoubleEnum, double>
	{
		public static readonly DoubleEnum One = new DoubleEnum(1, "One");

		/// <inheritdoc />
		private DoubleEnum(double value, string name)
			: base(value, name)
		{
		}
	}
}
