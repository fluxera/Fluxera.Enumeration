namespace Fluxera.Enumeration.SystemTextJson
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EnumerationNameConverter<TEnum> : JsonConverter<TEnum>
		where TEnum : Enumeration<TEnum>
	{
		public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
		{
			if(value is null)
			{
				writer.WriteNullValue();
			}
			else
			{
				writer.WriteStringValue(value.Name);
			}
		}

		public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if(reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}

			if(reader.TokenType == JsonTokenType.String)
			{
				string? name = reader.GetString();
				if(!Enumeration<TEnum>.TryParseName(name, out TEnum? result))
				{
					throw new JsonException($"Error converting name '{name ?? "null"}' to enumeration '{typeToConvert.Name}'.");
				}

				return result;
			}

			throw new JsonException($"Unexpected token {reader.TokenType} when parsing an enumeration.");
		}
	}
}
