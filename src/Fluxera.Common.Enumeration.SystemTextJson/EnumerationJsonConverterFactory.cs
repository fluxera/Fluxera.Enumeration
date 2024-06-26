namespace Fluxera.Enumeration.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class EnumerationJsonConverterFactory : JsonConverterFactory
	{
		private static readonly Type NameConverterType = typeof(EnumerationNameConverter<,>);
		private static readonly Type ValueConverterType = typeof(EnumerationValueConverter<,>);

		private readonly bool useValueConverter;

		/// <summary>
		///     Initializes a new instance of the <see cref="EnumerationJsonConverterFactory" /> type.
		/// </summary>
		/// <param name="useValueConverter"></param>
		public EnumerationJsonConverterFactory(bool useValueConverter = false)
		{
			this.useValueConverter = useValueConverter;
		}

		/// <inheritdoc />
		public override bool CanConvert(Type typeToConvert)
		{
			bool isEnumeration = typeToConvert.IsEnumeration();
			return isEnumeration;
		}

		/// <inheritdoc />
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type valueType = typeToConvert.GetEnumerationValueType();
			Type converterTypeTemplate = this.useValueConverter ? ValueConverterType : NameConverterType;
			Type converterType = converterTypeTemplate.MakeGenericType(typeToConvert, valueType);

			return (JsonConverter)Activator.CreateInstance(converterType);
		}
	}
}
