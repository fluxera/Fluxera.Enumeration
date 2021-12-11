namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class StringEnum : Enumeration<StringEnum, string>
	{
		public static readonly StringEnum One = new StringEnum("1", "One");

		/// <inheritdoc />
		public StringEnum(string value, string name) 
			: base(value, name)
		{
		}
	}
}