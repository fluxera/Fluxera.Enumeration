namespace Fluxera.Enumeration.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	[PublicAPI]
	public class EnumerationValueConverter<TEnum> : JsonConverter<TEnum>
		where TEnum : Enumeration<TEnum>
	{
		/// <inheritdoc />
		public override bool CanWrite => true;

		/// <inheritdoc />
		public override bool CanRead => true;

		/// <inheritdoc />
		public override void WriteJson(JsonWriter writer, TEnum? value, JsonSerializer serializer)
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
		public override TEnum? ReadJson(JsonReader reader, Type objectType, TEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.Null)
			{
				return null;
			}

			if(reader.TokenType == JsonToken.Integer)
			{
				int value = -1;
				if(reader.Value != null)
				{
					if(reader.Value.GetType() != typeof(int))
					{
						value = (int)Convert.ChangeType(reader.Value, typeof(int));
					}
					else
					{
						value = (int)reader.Value;
					}
				}

				if(!Enumeration<TEnum>.TryFromValue(value, out TEnum? result))
				{
					throw new JsonSerializationException($"Error converting value '{reader.Value ?? "null"}' to enumeration '{objectType.Name}'.");
				}

				return result;
			}

			throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing an enumeration.");
		}
	}
}
