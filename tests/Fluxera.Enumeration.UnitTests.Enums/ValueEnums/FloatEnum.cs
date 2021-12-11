namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class FloatEnum : Enumeration<FloatEnum, float>
	{
		public static readonly FloatEnum One = new FloatEnum(1, "One");

		/// <inheritdoc />
		public FloatEnum(float value, string name) 
			: base(value, name)
		{
		}
	}
}