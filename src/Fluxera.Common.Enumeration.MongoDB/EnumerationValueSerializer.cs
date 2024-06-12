namespace Fluxera.Enumeration.MongoDB
{
	using System;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.IO;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	/// <summary>
	///     A value-based enumeration serializer implementation.
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	[PublicAPI]
	public sealed class EnumerationValueSerializer<TEnum, TValue> : SerializerBase<TEnum>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable<TValue>, IEquatable<TValue>
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum value)
		{
			if(value is null)
			{
				context.Writer.WriteNull();
				return;
			}

			switch (value.Value)
			{
				case byte byteValue:
					context.Writer.WriteInt32(byteValue);
					break;
				case short shortValue:
					context.Writer.WriteInt32(shortValue);
					break;
				case int intValue:
					context.Writer.WriteInt32(intValue);
					break;
				case long longValue:
					context.Writer.WriteInt64(longValue);
					break;
				default:
					throw new FormatException($"Unsupported enum value type: {value.Value.GetType()}");
			}
		}

		/// <inheritdoc />
		public override TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if(context.Reader.CurrentBsonType == BsonType.Null)
			{
				context.Reader.ReadNull();
				return null;
			}

			if(context.Reader.CurrentBsonType is BsonType.Int32 or BsonType.Int64)
			{
				TValue value = ReadValue(context.Reader);
				if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum result))
				{
					throw new FormatException($"Error converting value '{value}' to enumeration '{args.NominalType.Name}'.");
				}

				return result;
			}

			throw new FormatException($"Unexpected token {context.Reader.CurrentBsonType} when parsing an enumeration.");
		}

		private static TValue ReadValue(IBsonReader reader)
		{
			TValue value;

			if(typeof(TValue) == typeof(byte))
			{
				value = (TValue)Convert.ChangeType(reader.ReadInt32(), typeof(byte));
			}
			else if(typeof(TValue) == typeof(short))
			{
				value = (TValue)Convert.ChangeType(reader.ReadInt32(), typeof(short));
			}
			else if(typeof(TValue) == typeof(int))
			{
				value = (TValue)(object)reader.ReadInt32();
			}
			else if(typeof(TValue) == typeof(long))
			{
				value = (TValue)(object)reader.ReadInt64();
			}
			else
			{
				throw new FormatException($"The value type {typeof(TValue).Name} is not supported.");
			}

			return value;
		}
	}
}
