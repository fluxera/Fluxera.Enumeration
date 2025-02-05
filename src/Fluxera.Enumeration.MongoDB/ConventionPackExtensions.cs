namespace Fluxera.Enumeration.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="ConventionPack" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConventionPackExtensions
	{
		/// <summary>
		///     Configures the convention to serialize enumerations.
		/// </summary>
		/// <param name="pack"></param>
		/// <param name="useValue"></param>
		/// <returns></returns>
		public static ConventionPack UseEnumeration(this ConventionPack pack, bool useValue = false)
		{
			pack.Add(new EnumerationConvention(useValue));

			return pack;
		}
	}
}
