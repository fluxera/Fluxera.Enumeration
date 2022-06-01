namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class ByteEnum : Enumeration<ByteEnum, byte>
	{
		public static readonly ByteEnum One = new ByteEnum(1, "One");

		/// <inheritdoc />
		private ByteEnum(byte value, string name)
			: base(value, name)
		{
		}
	}
}
