namespace Fluxera.Enumeration.JsonNet
{
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	[PublicAPI]
	public static class JsonSerializerSettingsExtensions
	{
		public static void UseEnumerationNameConverter(this JsonSerializerSettings settings)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new EnumerationContractResolver()
			};
		}

		public static void UseEnumerationValueConverter(this JsonSerializerSettings settings)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new EnumerationContractResolver(true)
			};
		}
	}
}
