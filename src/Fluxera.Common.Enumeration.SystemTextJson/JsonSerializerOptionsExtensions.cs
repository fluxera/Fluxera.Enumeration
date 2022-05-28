namespace Fluxera.Enumeration.SystemTextJson
{
	using System.Text.Json;
	using JetBrains.Annotations;

	/// <summary>
	///     See: https://github.com/dotnet/docs/blob/main/docs/standard/serialization/system-text-json-converters-how-to.md
	/// </summary>
	[PublicAPI]
	public static class JsonSerializerOptionsExtensions
	{
		/// <summary>
		///     Configures the JSON converter to use.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="useValue"></param>
		public static void UseEnumeration(this JsonSerializerOptions options, bool useValue = false)
		{
			options.Converters.Add(new EnumerationJsonConverterFactory(useValue));
		}
	}
}
