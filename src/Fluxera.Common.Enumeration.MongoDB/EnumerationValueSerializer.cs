namespace Fluxera.Enumeration.MongoDB
{
	using System;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	[PublicAPI]
	public class EnumerationValueSerializer<TEnum> : SerializerBase<TEnum>
		where TEnum : Enumeration<TEnum>
	{
		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TEnum? value)
		{
			if(value is null)
			{
				context.Writer.WriteNull();
			}
			else
			{
				context.Writer.WriteInt32(value.Value);
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
				int value = context.Reader.ReadInt32();

				if(!Enumeration<TEnum>.TryFromValue(value, out TEnum? result))
				{
					throw new FormatException($"Error converting value '{value}' to enumeration '{args.NominalType.Name}'.");
				}

				return result!;
			}

			throw new FormatException($"Unexpected token {context.Reader.CurrentBsonType} when parsing an enumeration.");
		}
	}
}
