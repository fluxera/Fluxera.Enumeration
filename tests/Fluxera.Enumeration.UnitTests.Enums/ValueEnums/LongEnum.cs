namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class LongEnum : Enumeration<LongEnum, long>
	{
		public static readonly LongEnum One = new LongEnum(1, "One");

		/// <inheritdoc />
		private LongEnum(long value, string name)
			: base(value, name)
		{
		}
	}
}
