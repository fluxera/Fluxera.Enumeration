namespace Fluxera.Enumeration.UnitTests.Enums
{
	public abstract class Animal : Enumeration<Animal, int>
	{
		/// <inheritdoc />
		protected Animal(int value, string name)
			: base(value, name)
		{
		}
	}
}
