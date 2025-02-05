namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A value-based enumeration converter.
	/// </summary>
	[PublicAPI]
	public static class EnumerationValueConverter
	{
		/// <summary>
		///     Serializes the enumeration.
		/// </summary>
		/// <returns></returns>
		public static Func<object, BsonValue> Serialize()
		{
			return obj =>
			{
				if(obj is not IEnumeration enumeration)
				{
					return BsonValue.Null;
				}

				switch(enumeration.Value)
				{
					case byte byteValue:
						return new BsonValue(byteValue);
					case short shortValue:
						return new BsonValue(shortValue);
					case int intValue:
						return new BsonValue(intValue);
					case long longValue:
						return new BsonValue(longValue);
					default:
						throw new LiteException(0, $"Unsupported enum value type: {enumeration.Value.GetType()}");
				}
			};
		}

		/// <summary>
		///     Deserializes the enumeration.
		/// </summary>
		/// <param name="enumerationType"></param>
		/// <returns></returns>
		/// <exception cref="LiteException"></exception>
		public static Func<BsonValue, object> Deserialize(Type enumerationType)
		{
			return bson =>
			{
				if(bson.IsNull)
				{
					return null;
				}

				if(bson.IsNumber)
				{
					Type valueType = enumerationType.GetEnumerationValueType();
					object value = ReadValue(bson, valueType);

					if(!Enumeration.TryParseValue(enumerationType, value, out IEnumeration result))
					{
						throw new LiteException(0, $"Error converting value '{value}' to enumeration '{enumerationType.Name}'.");
					}

					return result;
				}

				throw new LiteException(0, $"Unexpected token {bson.Type} when parsing an enumeration.");
			};
		}

		private static object ReadValue(BsonValue bsonValue, Type typeValue)
		{
			object value;

			if(typeValue == typeof(byte))
			{
				value = bsonValue.AsInt32;
			}
			else if(typeValue == typeof(short))
			{
				value = bsonValue.AsInt32;
			}
			else if(typeValue == typeof(int))
			{
				value = bsonValue.AsInt32;
			}
			else if(typeValue == typeof(long))
			{
				value = bsonValue.AsInt64;
			}
			else
			{
				throw new LiteException(0, $"The value type {typeValue.Name} is not supported.");
			}

			return value;
		}
	}
}
