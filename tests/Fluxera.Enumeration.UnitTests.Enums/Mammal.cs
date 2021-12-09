namespace Fluxera.Enumeration.UnitTests.Enums
{
	using System.Runtime.CompilerServices;

	public sealed class Mammal : Animal
	{
		public static readonly Mammal Tiger = new Mammal(0);
		public static readonly Mammal Elephant = new Mammal(1);

		/// <inheritdoc />
		private Mammal(int value, [CallerMemberName] string name = null!)
			: base(value, name)
		{
		}
	}
}
