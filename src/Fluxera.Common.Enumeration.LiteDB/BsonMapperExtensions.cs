﻿namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="BsonMapper" /> type.
	/// </summary>
	[PublicAPI]
	public static class BsonMapperExtensions
	{
		/// <summary>
		///     Configures the serialization of enumerations.
		/// </summary>
		/// <param name="mapper"></param>
		/// <param name="useValue"></param>
		/// <returns></returns>
		public static BsonMapper UseEnumeration(this BsonMapper mapper, bool useValue = false)
		{
			Guard.ThrowIfNull(mapper);

			IEnumerable<Type> enumerationTypes = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(x => x.GetTypes())
				.Where(x => x.IsEnumeration());

			foreach(Type enumerationType in enumerationTypes)
			{
				if(useValue)
				{
					mapper.RegisterValueType(enumerationType);
				}
				else
				{
					mapper.RegisterNameType(enumerationType);
				}
			}

			return mapper;
		}

		private static void RegisterNameType(this BsonMapper mapper, Type enumerationType)
		{
			mapper.RegisterType(enumerationType,
				EnumerationNameConverter.Serialize(),
				EnumerationNameConverter.Deserialize(enumerationType));
		}

		private static void RegisterValueType(this BsonMapper mapper, Type enumerationType)
		{
			mapper.RegisterType(enumerationType,
				EnumerationValueConverter.Serialize(),
				EnumerationValueConverter.Deserialize(enumerationType));
		}
	}
}
