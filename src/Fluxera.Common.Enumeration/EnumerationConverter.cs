namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Concurrent;
	using System.ComponentModel;
	using System.Globalization;

	internal sealed class EnumerationConverter : TypeConverter
	{
		private static readonly ConcurrentDictionary<Type, TypeConverter> ActualConverters = new ConcurrentDictionary<Type, TypeConverter>();

		private readonly TypeConverter innerConverter;

		public EnumerationConverter(Type enumerationType)
		{
			this.innerConverter = ActualConverters.GetOrAdd(enumerationType, CreateActualConverter);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return this.innerConverter.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return this.innerConverter.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return this.innerConverter.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			return this.innerConverter.ConvertTo(context, culture, value, destinationType);
		}

		private static TypeConverter CreateActualConverter(Type enumerationType)
		{
			if(!enumerationType.IsEnumeration())
			{
				throw new InvalidOperationException($"The type '{enumerationType}' is not a enumeration.");
			}

			Type valueType = enumerationType.GetValueType();
			Type actualConverterType = typeof(EnumerationConverter<,>).MakeGenericType(enumerationType, valueType);
			return (TypeConverter)Activator.CreateInstance(actualConverterType);
		}
	}

	internal sealed class EnumerationConverter<TEnum, TValue> : TypeConverter
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : notnull, IComparable, IComparable<TValue>
	{
		/// <inheritdoc />
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string)
				|| sourceType == typeof(TValue)
				|| base.CanConvertFrom(context, sourceType);
		}

		/// <inheritdoc />
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string)
				|| destinationType == typeof(TValue)
				|| base.CanConvertTo(context, destinationType);
		}

		/// <inheritdoc />
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if(value is string name)
			{
				if(!Enumeration.TryParseName(typeof(TEnum), name, true, out IEnumeration result))
				{
					result = Enumeration.ParseValue(typeof(TEnum), value);
				}

				return result;
			}

			if(value is TValue)
			{
				return Enumeration.ParseValue(typeof(TEnum), value);
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <inheritdoc />
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Guard.ThrowIfNull(value);

			Enumeration<TEnum, TValue> enumeration = (Enumeration<TEnum, TValue>)value;

			if(destinationType == typeof(string))
			{
				return enumeration.Name;
			}

			if(destinationType == typeof(TValue))
			{
				return enumeration.Value;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
