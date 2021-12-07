namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	public class EnumerationNameConverter<TEnum> : ValueConverter<TEnum?, string?>
		where TEnum : Enumeration<TEnum>
	{
		public EnumerationNameConverter()
			: base(enumeration => Serialize(enumeration), name => Deserialize(name))
		{
		}

		private static string? Serialize(TEnum? enumeration)
		{
			return enumeration?.Name;
		}

		private static TEnum? Deserialize(string? name)
		{
			if(name is null)
			{
				return null;
			}

			if(!Enumeration<TEnum>.TryFromName(name, out TEnum? result))
			{
				throw new FormatException($"Error converting name '{name}' to enumeration '{typeof(TEnum).Name}'.");
			}

			return result!;
		}
	}
}
