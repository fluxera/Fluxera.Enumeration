namespace Fluxera.Enumeration.UnitTests.Enums
{
	public abstract class Animal : Enumeration<Animal>
	{
		/// <inheritdoc />
		protected Animal(int value, string name)
			: base(value, name)
		{
		}
	}
}
