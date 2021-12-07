namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	public class EnumerationValueConverter<TEnum> : ValueConverter<TEnum?, int?>
		where TEnum : Enumeration<TEnum>
	{
		public EnumerationValueConverter()
			: base(enumeration => Serialize(enumeration), value => Deserialize(value))
		{
		}

		private static int? Serialize(TEnum? enumeration)
		{
			return enumeration?.Value;
		}

		private static TEnum? Deserialize(int? value)
		{
			if(value is null)
			{
				return null;
			}

			if(!Enumeration<TEnum>.TryFromValue(value.Value, out TEnum? result))
			{
				throw new FormatException($"Error converting value '{value}' to enumeration '{typeof(TEnum).Name}'.");
			}

			return result!;
		}
	}
}
