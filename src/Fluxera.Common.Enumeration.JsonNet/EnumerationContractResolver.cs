﻿namespace Fluxera.Enumeration.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	[PublicAPI]
	public sealed class EnumerationContractResolver : DefaultContractResolver
	{
		private static readonly Type nameConverterType = typeof(EnumerationNameConverter<,>);
		private static readonly Type valueConverterType = typeof(EnumerationValueConverter<,>);
		private readonly bool useValueConverter;

		public EnumerationContractResolver(bool useValueConverter = false)
		{
			this.useValueConverter = useValueConverter;
		}

		/// <inheritdoc />
		protected override JsonConverter? ResolveContractConverter(Type objectType)
		{
			if(objectType.IsEnumeration())
			{
				Type valueType = objectType.GetValueType();
				Type converterTypeTemplate = this.useValueConverter ? valueConverterType : nameConverterType;
				Type converterType = converterTypeTemplate.MakeGenericType(objectType, valueType);

				return (JsonConverter)Activator.CreateInstance(converterType);
			}

			return base.ResolveContractConverter(objectType);
		}
	}
}
