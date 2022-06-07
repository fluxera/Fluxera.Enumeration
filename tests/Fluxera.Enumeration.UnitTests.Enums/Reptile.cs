namespace Fluxera.Enumeration.UnitTests.Enums
{
	using System.Runtime.CompilerServices;

	public sealed class Reptile : Animal
	{
		public static readonly Reptile Iguana = new Reptile(2);
		public static readonly Reptile Python = new Reptile(3);

		/// <inheritdoc />
		private Reptile(int value, [CallerMemberName] string name = null)
			: base(value, name)
		{
		}
	}
}
