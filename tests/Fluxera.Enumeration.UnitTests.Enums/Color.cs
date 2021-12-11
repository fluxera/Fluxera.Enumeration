namespace Fluxera.Enumeration.UnitTests.Enums
{
	using System.Runtime.CompilerServices;

	public sealed class Color : Enumeration<Color, int>
	{
		public static readonly Color Red = new Color(0, "FF0000");
		public static readonly Color Green = new Color(1, "00FF00");
		public static readonly Color Blue = new Color(2, "0000FF");

		/// <inheritdoc />
		private Color(int value, string hexValue, [CallerMemberName] string name = null!)
			: base(value, name)
		{
			this.HexValue = hexValue;
		}

		public string HexValue { get; }
	}
}
