namespace Fluxera.Enumeration.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class EnumerationValueConverter<TEnum, TValue> : JsonConverter<TEnum>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable, IComparable<TValue>
	{
		/// <inheritdoc />
		public override bool CanWrite => true;

		/// <inheritdoc />
		public override bool CanRead => true;

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, TEnum value, JsonSerializer serializer)
		{
			if(value is null)
			{
				writer.WriteNull();
			}
			else
			{
				writer.WriteValue(value.Value);
			}
		}

		/// <inheritdoc />
		public override TEnum ReadJson(JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.Null)
			{
				return null;
			}

			if(reader.TokenType is JsonToken.Integer or JsonToken.String or JsonToken.Float)
			{
				TValue value;

				if(typeof(TValue) == typeof(Guid))
				{
					string strValue = (string)reader.Value;
					value = string.IsNullOrWhiteSpace(strValue)
						? (TValue)(object)Guid.Empty
						: (TValue)(object)Guid.Parse(strValue);
				}
				else
				{
					value = (TValue)Convert.ChangeType(reader.Value, typeof(TValue));
				}

				if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum result))
				{
					throw new JsonSerializationException($"Error converting value '{reader.Value ?? "null"}' to enumeration '{objectType.Name}'.");
				}

				return result;
			}

			throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing an enumeration.");
		}
	}
}
