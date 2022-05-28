namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A name-based enumeration converter.
	/// </summary>
	[PublicAPI]
	public static class EnumerationNameConverter
	{
		/// <summary>
		///     Serializes the enumeration.
		/// </summary>
		/// <returns></returns>
		public static Func<object, BsonValue> Serialize()
		{
			return obj =>
			{
				IEnumeration enumeration = obj as IEnumeration;
				return enumeration?.Name;
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

				if(bson.IsString)
				{
					string name = bson.AsString;

					if(!Enumeration.TryParseName(enumerationType, name, out IEnumeration result))
					{
						throw new LiteException(0, $"Error converting name '{name ?? "null"}' to enumeration '{enumerationType.Name}'.");
					}

					return result;
				}

				throw new LiteException(0, $"Unexpected token {bson.Type} when parsing an enumeration.");
			};
		}
	}
}
