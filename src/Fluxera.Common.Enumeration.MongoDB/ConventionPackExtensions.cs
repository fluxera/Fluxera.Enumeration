namespace Fluxera.Enumeration.MongoDB
{
	using global::MongoDB.Bson.Serialization.Conventions;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ConventionPackExtensions
	{
		public static ConventionPack UseEnumerationNameConverter(this ConventionPack pack)
		{
			pack.Add(new EnumerationConvention());

			return pack;
		}

		public static ConventionPack UseEnumerationValueConverter(this ConventionPack pack)
		{
			pack.Add(new EnumerationConvention(true));

			return pack;
		}
	}
}
