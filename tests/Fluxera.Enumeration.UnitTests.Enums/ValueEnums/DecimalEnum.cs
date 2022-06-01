namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class DecimalEnum : Enumeration<DecimalEnum, decimal>
	{
		public static readonly DecimalEnum One = new DecimalEnum(1, "One");

		/// <inheritdoc />
		private DecimalEnum(decimal value, string name)
			: base(value, name)
		{
		}
	}
}
