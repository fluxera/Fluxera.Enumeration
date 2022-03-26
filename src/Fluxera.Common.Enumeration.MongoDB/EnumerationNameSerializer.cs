namespace Fluxera.Enumeration.MongoDB
{
	using System;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class EnumerationNameSerializer<TEnum, TValue> : SerializerBase<TEnum>
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : IComparable, IComparable<TValue>
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
				context.Writer.WriteString(value.Name);
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

			if(context.Reader.CurrentBsonType == BsonType.String)
			{
				string? name = context.Reader.ReadString();
				if(!Enumeration<TEnum, TValue>.TryParseName(name, out TEnum? result))
				{
					throw new FormatException($"Error converting name '{name ?? "null"}' to enumeration '{args.NominalType.Name}'.");
				}

				return result!;
			}

			throw new FormatException($"Unexpected token {context.Reader.CurrentBsonType} when parsing an enumeration.");
		}
	}
}
