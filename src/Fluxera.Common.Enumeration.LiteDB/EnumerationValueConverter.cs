namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using global::LiteDB;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class EnumerationValueConverter
	{
		public static Func<object, BsonValue?> Serialize(Type enumerationType)
		{
			return obj =>
			{
				IEnumeration? enumeration = obj as IEnumeration;
				return new BsonValue(enumeration?.Value);
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

				if(bson.IsNumber || bson.IsString || bson.IsBoolean || bson.IsDecimal)
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
			else
			{
				throw new LiteException(0, $"The value type {typeValue.Name} is not supported.");
			}

			return value;
		}
	}
}
