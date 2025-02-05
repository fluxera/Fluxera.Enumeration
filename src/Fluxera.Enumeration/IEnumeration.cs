namespace Fluxera.Enumeration
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for using the enumeration is contexts that do not provide type information.
	/// </summary>
	[PublicAPI]
	public interface IEnumeration : IComparable
	{
		/// <summary>
		///     Gets the name of the enum option.
		/// </summary>
		string Name { get; }

		/// <summary>
		///     Gets the value of the enum option.
		/// </summary>
		object Value { get; }
	}
}
