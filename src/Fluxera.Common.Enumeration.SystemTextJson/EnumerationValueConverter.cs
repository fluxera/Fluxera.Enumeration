namespace Fluxera.Enumeration.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EnumerationValueConverter<TEnum, TValue> : JsonConverter<TEnum>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable, IComparable<TValue>
	{
		public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
		{
			if(value is null)
			{
				writer.WriteNullValue();
				return;
			}

			switch(value)
			{
				case { Value: byte writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				case { Value: short writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				case { Value: int writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				case { Value: long writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				case { Value: decimal writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				case { Value: float writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				case { Value: double writeValue }:
					writer.WriteNumberValue(writeValue);
					break;
				default:
					writer.WriteStringValue(value.Value.ToString());
					break;
			}
		}

		public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			if(reader.TokenType is JsonTokenType.Number or JsonTokenType.String)
			{
				TValue value = ReadValue(ref reader);
				if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum? result))
				{
					throw new JsonException($"Error converting value '{value}' to enumeration '{typeToConvert.Name}'.");
				}

				return result;
			}

			throw new JsonException($"Unexpected token {reader.TokenType} when parsing an enumeration.");
		}

		private TValue ReadValue(ref Utf8JsonReader reader)
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
			else if(typeof(TValue) == typeof(float))
			{
				value = (TValue)(object)reader.GetSingle();
			}
			else if(typeof(TValue) == typeof(double))
			{
				value = (TValue)(object)reader.GetDouble();
			}
			else if(typeof(TValue) == typeof(decimal))
			{
				value = (TValue)(object)reader.GetDecimal();
			}
			else if(typeof(TValue) == typeof(string))
			{
				value = (TValue)(object)reader.GetString();
			}
			else if(typeof(TValue) == typeof(Guid))
			{
				value = (TValue)(object)reader.GetGuid();
			}
			else
			{
				throw new JsonException($"The value type {typeof(TValue).Name} is not supported.");
			}

			return value;
		}
	}
}
