namespace Fluxera.Enumeration.JsonNet
{
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	[PublicAPI]
	public static class JsonSerializerSettingsExtensions
	{
		public static void UseEnumeration(this JsonSerializerSettings settings, bool useValue = false)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new EnumerationContractResolver(useValue)
			};
		}
	}
}
