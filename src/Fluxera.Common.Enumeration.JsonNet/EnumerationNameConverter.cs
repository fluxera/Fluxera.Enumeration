﻿namespace Fluxera.Enumeration.JsonNet
{
	using System;
	using JetBrains.Annotations;
	using Newtonsoft.Json;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class EnumerationNameConverter<TEnum, TValue> : JsonConverter<TEnum>
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
				writer.WriteValue(value.Name);
			}
		}

		/// <inheritdoc />
		public override TEnum ReadJson(JsonReader reader, Type objectType, TEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if(reader.TokenType == JsonToken.Null)
			{
				return null;
			}

			if(reader.TokenType == JsonToken.String)
			{
				string name = (string)reader.Value;
				if(!Enumeration<TEnum, TValue>.TryParseName(name, out TEnum result))
				{
					throw new JsonSerializationException($"Error converting name '{name ?? "null"}' to enumeration '{objectType.Name}'.");
				}

				return result;
			}

			throw new JsonSerializationException($"Unexpected token {reader.TokenType} when parsing an enumeration.");
		}
	}
}
