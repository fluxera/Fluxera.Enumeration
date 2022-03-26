namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using global::LiteDB;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class EnumerationValueConverter
	{
		public static Func<object, BsonValue?> Serialize()
		{
			return obj =>
			{
				if(obj is not IEnumeration enumeration)
				{
					return BsonValue.Null;
				}

				BsonValue bsonValue;

				switch(enumeration)
				{
					case { Value: byte writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: short writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: int writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: long writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: decimal writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: float writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: double writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					case { Value: Guid writeValue }:
						bsonValue = new BsonValue(writeValue);
						break;
					default:
						bsonValue = new BsonValue(enumeration.Value.ToString());
						break;
				}

				return bsonValue;
			};
		}

		public static Func<BsonValue, object?> Deserialize(Type enumerationType)
		{
			return bson =>
			{
				if(bson.IsNull)
				{
					return null;
				}

				if(bson.IsNumber || bson.IsString || bson.IsBoolean || bson.IsDecimal || bson.IsGuid)
				{
					Type valueType = enumerationType.GetValueType();
					object value = ReadValue(bson, valueType);

					if(!Enumeration.TryParseValue(enumerationType, value, out IEnumeration? result))
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
			else if(typeValue == typeof(float))
			{
				value = bsonValue.AsDouble;
			}
			else if(typeValue == typeof(double))
			{
				value = bsonValue.AsDouble;
			}
			else if(typeValue == typeof(decimal))
			{
				value = bsonValue.AsDecimal;
			}
			else if(typeValue == typeof(string))
			{
				value = bsonValue.AsString;
			}
			else if(typeValue == typeof(Guid))
			{
				value = bsonValue.AsGuid;
			}
			else
			{
				throw new LiteException(0, $"The value type {typeValue.Name} is not supported.");
			}

			return value;
		}
	}
}
