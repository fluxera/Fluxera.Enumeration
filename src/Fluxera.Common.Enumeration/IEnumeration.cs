namespace Fluxera.Enumeration
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IEnumeration
	{
		/// <summary>
		///     Gets the name of the enum option.
		/// </summary>
		string Name { get; }

		/// <summary>
		///     Gets the numeric value of the enum option.
		/// </summary>
		int Value { get; }
	}
}
