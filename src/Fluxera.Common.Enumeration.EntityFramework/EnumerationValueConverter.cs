namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	public class EnumerationValueConverter<TEnum, TValue> : ValueConverter<TEnum?, TValue?>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : struct, IComparable, IComparable<TValue>
	{
		public EnumerationValueConverter()
			: base(enumeration => Serialize(enumeration), value => Deserialize(value))
		{
		}

		private static TValue? Serialize(TEnum? enumeration)
		{
			return enumeration?.Value;
		}

		private static TEnum? Deserialize(TValue? value)
		{
			if(value is null)
			{
				return null;
			}

			if(!Enumeration<TEnum, TValue>.TryParseValue(value.Value, out TEnum? result))
			{
				throw new FormatException($"Error converting value '{value}' to enumeration '{typeof(TEnum).Name}'.");
			}

			return result!;
		}
	}
}
