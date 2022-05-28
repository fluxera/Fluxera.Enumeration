namespace Fluxera.Enumeration.JsonNet
{
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	/// <summary>
	///     Extension methods for the <see cref="JsonSerializerSettings" /> type.
	/// </summary>
	[PublicAPI]
	public static class JsonSerializerSettingsExtensions
	{
		/// <summary>
		///     Configures the contract resolver to use when serializing enumerations.
		/// </summary>
		/// <param name="settings"></param>
		/// <param name="useValue"></param>
		public static void UseEnumeration(this JsonSerializerSettings settings, bool useValue = false)
		{
			settings.ContractResolver = new CompositeContractResolver
			{
				new EnumerationContractResolver(useValue)
			};
		}
	}
}
