namespace Fluxera.Enumeration.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class EnumerationValueConverter<TEnum, TValue> : JsonConverter<TEnum>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		/// <inheritdoc />
		public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
		{
			if(value is null)
			{
				writer.WriteNullValue();
				return;
			}

			switch(value.Value)
			{
				case byte byteValue:
					writer.WriteNumberValue(byteValue);
					break;
				case short shortValue:
					writer.WriteNumberValue(shortValue);
					break;
				case int intValue:
					writer.WriteNumberValue(intValue);
					break;
				case long longValue:
					writer.WriteNumberValue(longValue);
					break;
				default:
					throw new JsonException($"Unsupported enum value type: {value.Value.GetType()}");
			}
		}

		/// <inheritdoc />
		public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			if(reader.TokenType is JsonTokenType.Number or JsonTokenType.String)
			{
				TValue value = ReadValue(ref reader);
				if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum result))
				{
					throw new JsonException($"Error converting value '{value}' to enumeration '{typeToConvert.Name}'.");
				}

				return result;
			}

			throw new JsonException($"Unexpected token {reader.TokenType} when parsing an enumeration.");
		}

		private static TValue ReadValue(ref Utf8JsonReader reader)
		{
			TValue value;

			if(typeof(TValue) == typeof(byte))
			{
				value = (TValue)(object)reader.GetByte();
			}
			else if(typeof(TValue) == typeof(short))
			{
				value = (TValue)(object)reader.GetInt16();
			}
			else if(typeof(TValue) == typeof(int))
			{
				value = (TValue)(object)reader.GetInt32();
			}
			else if(typeof(TValue) == typeof(long))
			{
				value = (TValue)(object)reader.GetInt64();
			}
			else
			{
				throw new JsonException($"The value type {typeof(TValue).Name} is not supported.");
			}

			return value;
		}
	}
}
