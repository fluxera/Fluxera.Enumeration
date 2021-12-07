namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using global::LiteDB;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class EnumerationValueConverter
	{
		public static readonly Func<object, BsonValue?> Serialize = obj =>
		{
			IEnumeration? enumeration = obj as IEnumeration;
			return enumeration?.Value;
		};

		public static Func<BsonValue, object?> Deserialize(Type enumerationType)
		{
			return bson =>
			{
				if(bson.IsNull)
				{
					return null;
				}

				if(bson.IsNumber)
				{
					int value = bson.AsInt32;

					if(!Enumeration.TryFromValue(enumerationType, value, out IEnumeration? result))
					{
						throw new LiteException(0, $"Error converting value '{value}' to enumeration '{enumerationType.Name}'.");
					}

					return result;
				}

				throw new LiteException(0, $"Unexpected token {bson.Type} when parsing an enumeration.");
			};
		}
	}
}
