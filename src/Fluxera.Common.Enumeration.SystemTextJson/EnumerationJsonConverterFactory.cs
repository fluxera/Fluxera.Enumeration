namespace Fluxera.Enumeration.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class EnumerationJsonConverterFactory : JsonConverterFactory
	{
		private static readonly Type nameConverterType = typeof(EnumerationNameConverter<,>);
		private static readonly Type valueConverterType = typeof(EnumerationValueConverter<,>);
		private readonly bool useValueConverter;

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
		public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			Type valueType = typeToConvert.GetValueType();
			Type converterTypeTemplate = this.useValueConverter ? valueConverterType : nameConverterType;
			Type converterType = converterTypeTemplate.MakeGenericType(typeToConvert, valueType);

			return (JsonConverter)Activator.CreateInstance(converterType);
		}
	}
}
