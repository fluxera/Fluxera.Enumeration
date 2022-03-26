namespace Fluxera.Enumeration.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ConventionPackExtensions
	{
		public static ConventionPack UseEnumeration(this ConventionPack pack, bool useValue = false)
		{
			pack.Add(new EnumerationConvention(useValue));

			return pack;
		}
	}
}
