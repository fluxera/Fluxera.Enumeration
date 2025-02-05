namespace Fluxera.Enumeration.EntityFrameworkCore
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class EnumerationValueConverter<TEnum, TValue> : ValueConverter<TEnum, TValue>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="EnumerationValueConverter{TEnum,TValue}" /> type.
		/// </summary>
		public EnumerationValueConverter()
			: base(enumeration => Serialize(enumeration), value => Deserialize(value))
		{
		}

		private static TValue Serialize(TEnum enumeration)
		{
			if(enumeration is null)
			{
				return default;
			}

			switch(enumeration.Value)
			{
				case byte:
				case short:
				case int:
				case long:
					return enumeration.Value;
				default:
					throw new FormatException($"Unsupported enum value type: {enumeration.Value.GetType()}");
			}
		}

		private static TEnum Deserialize(TValue value)
		{
			if(value is null)
			{
				return null;
			}

			if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum result))
			{
				throw new FormatException($"Error converting value '{value}' to enumeration '{typeof(TEnum).Name}'.");
			}

			return result;
		}
	}
}
