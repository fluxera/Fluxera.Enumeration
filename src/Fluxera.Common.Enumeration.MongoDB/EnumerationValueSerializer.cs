namespace Fluxera.Enumeration.MongoDB
{
	using System;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.IO;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EnumerationValueSerializer<TEnum, TValue> : SerializerBase<TEnum>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : struct, IComparable, IComparable<TValue>
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum? value)
		{
			if(value is null)
			{
				context.Writer.WriteNull();
				return;
			}

			switch(value)
			{
				case { Value: byte writeValue }:
					context.Writer.WriteInt32(writeValue);
					break;
				case { Value: short writeValue }:
					context.Writer.WriteInt32(writeValue);
					break;
				case { Value: int writeValue }:
					context.Writer.WriteInt32(writeValue);
					break;
				case { Value: long writeValue }:
					context.Writer.WriteInt64(writeValue);
					break;
				case { Value: decimal writeValue }:
					context.Writer.WriteDecimal128(writeValue);
					break;
				case { Value: float writeValue }:
					context.Writer.WriteDouble(writeValue);
					break;
				case { Value: double writeValue }:
					context.Writer.WriteDouble(writeValue);
					break;
				default:
					context.Writer.WriteString(value.Value.ToString());
					break;
			}
		}

		/// <inheritdoc />
		public override TEnum Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			if(context.Reader.CurrentBsonType == BsonType.Null)
			{
				context.Reader.ReadNull();
				return null!;
			}

			if(context.Reader.CurrentBsonType == BsonType.Int32)
			{
				TValue value = ReadValue(context.Reader);
				if(!Enumeration<TEnum, TValue>.TryParseValue(value, out TEnum? result))
				{
					throw new FormatException($"Error converting value '{value}' to enumeration '{args.NominalType.Name}'.");
				}

				return result!;
			}

			throw new FormatException($"Unexpected token {context.Reader.CurrentBsonType} when parsing an enumeration.");
		}

		private TValue ReadValue(IBsonReader reader)
		{
			TValue value;

			if(typeof(TValue) == typeof(byte))
			{
				value = (TValue)(object)reader.ReadInt32();
			}
			else if(typeof(TValue) == typeof(short))
			{
				value = (TValue)(object)reader.ReadInt32();
			}
			else if(typeof(TValue) == typeof(int))
			{
				value = (TValue)(object)reader.ReadInt32();
			}
			else if(typeof(TValue) == typeof(long))
			{
				value = (TValue)(object)reader.ReadInt64();
			}
			else if(typeof(TValue) == typeof(float))
			{
				value = (TValue)(object)reader.ReadDouble();
			}
			else if(typeof(TValue) == typeof(double))
			{
				value = (TValue)(object)reader.ReadDouble();
			}
			else if(typeof(TValue) == typeof(decimal))
			{
				value = (TValue)(object)reader.ReadDecimal128();
			}
			else if(typeof(TValue) == typeof(string))
			{
				value = (TValue)(object)reader.ReadString();
			}
			else
			{
				throw new FormatException($"The value type {typeof(TValue).Name} is not supported.");
			}

			return value;
		}
	}
}
