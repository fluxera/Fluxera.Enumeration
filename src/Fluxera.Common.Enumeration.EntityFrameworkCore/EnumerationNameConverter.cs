namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	[PublicAPI]
	public sealed class EnumerationNameConverter<TEnum, TValue> : ValueConverter<TEnum?, string?>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable, IComparable<TValue>
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

			if(!Enumeration<TEnum, TValue>.TryParseName(name, out TEnum? result))
			{
				throw new FormatException($"Error converting name '{name}' to enumeration '{typeof(TEnum).Name}'.");
			}

			return result!;
		}
	}
}
