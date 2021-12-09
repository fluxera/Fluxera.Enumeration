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
		public static void UseEnumerationNameConverter(this JsonSerializerOptions options)
		{
			options.Converters.Add(new EnumerationJsonConverterFactory());
		}

		public static void UseEnumerationValueConverter(this JsonSerializerOptions options)
		{
			options.Converters.Add(new EnumerationJsonConverterFactory(true));
		}

		public static void UseEnumerationNameConverter<TEnum>(this JsonSerializerOptions options)
			where TEnum : Enumeration<TEnum>
		{
			options.Converters.Add(new EnumerationNameConverter<TEnum>());
		}

		public static void UseEnumerationValueConverter<TEnum>(this JsonSerializerOptions options)
			where TEnum : Enumeration<TEnum>
		{
			options.Converters.Add(new EnumerationValueConverter<TEnum>());
		}
	}
}