namespace Fluxera.Enumeration.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class EnumerationContractResolver : DefaultContractResolver
	{
		private static readonly Type NameConverterType = typeof(EnumerationNameConverter<,>);
		private static readonly Type ValueConverterType = typeof(EnumerationValueConverter<,>);

		private readonly bool useValueConverter;

		/// <summary>
		///     Initializes a new instance of the <see cref="EnumerationContractResolver" /> type.
		/// </summary>
		/// <param name="useValueConverter"></param>
		public EnumerationContractResolver(bool useValueConverter = false)
		{
			this.useValueConverter = useValueConverter;
		}

		/// <inheritdoc />
		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			if(objectType.IsEnumeration())
			{
				Type valueType = objectType.GetEnumerationValueType();
				Type converterTypeTemplate = this.useValueConverter ? ValueConverterType : NameConverterType;
				Type converterType = converterTypeTemplate.MakeGenericType(objectType, valueType);

				return (JsonConverter)Activator.CreateInstance(converterType);
			}

			return base.ResolveContractConverter(objectType);
		}
	}
}
