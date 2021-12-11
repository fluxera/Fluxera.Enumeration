namespace Fluxera.Enumeration.LiteDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using global::LiteDB;
	using Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class BsonMapperExtensions
	{
		public static BsonMapper UseEnumerationNameConverter(this BsonMapper mapper, Assembly assembly)
		{
			Guard.Against.Null(mapper, nameof(mapper));
			Guard.Against.Null(assembly, nameof(assembly));

			IEnumerable<Type> enumerationTypes = assembly.GetTypes().Where(type => type.IsEnumeration());
			foreach(Type enumerationType in enumerationTypes)
			{
				mapper.RegisterNameType(enumerationType);
			}

			return mapper;
		}

		public static BsonMapper UseEnumerationValueConverter(this BsonMapper mapper, Assembly assembly)
		{
			Guard.Against.Null(mapper, nameof(mapper));
			Guard.Against.Null(assembly, nameof(assembly));

			IEnumerable<Type> enumerationTypes = assembly.GetTypes().Where(type => type.IsEnumeration());
			foreach(Type enumerationType in enumerationTypes)
			{
				mapper.RegisterValueType(enumerationType);
			}

			return mapper;
		}

		private static void RegisterNameType(this BsonMapper mapper, Type enumerationType)
		{
			mapper.RegisterType(enumerationType,
				EnumerationNameConverter.Serialize(enumerationType),
				EnumerationNameConverter.Deserialize(enumerationType));
		}

		private static void RegisterValueType(this BsonMapper mapper, Type enumerationType)
		{
			mapper.RegisterType(enumerationType,
				EnumerationValueConverter.Serialize(enumerationType),
				EnumerationValueConverter.Deserialize(enumerationType));
		}
	}
}
