namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	public class EnumerationValueConverter<TEnum, TValue> : ValueConverter<TEnum?, TValue?>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable, IComparable<TValue>
	{
		public EnumerationValueConverter()
			: base(enumeration => Serialize(enumeration), value => Deserialize(value))
		{
		}

		private static TValue? Serialize(TEnum? enumeration)
		{
			if(enumeration is null)
			{
				return default;
			}

			return enumeration.Value;
		}

		private static TEnum? Deserialize(TValue? value)
		{
			if(value is null)
			{
				return null;
			}

			if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum? result))
			{
				throw new FormatException($"Error converting value '{value}' to enumeration '{typeof(TEnum).Name}'.");
			}

			return result!;
		}
	}
}
