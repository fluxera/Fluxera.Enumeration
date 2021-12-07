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
		public static BsonMapper UseEnumerationName<TEnum>(this BsonMapper mapper)
			where TEnum : Enumeration<TEnum>
		{
			mapper.RegisterNameType(typeof(TEnum));
			return mapper;
		}

		public static BsonMapper UseEnumerationValue<TEnum>(this BsonMapper mapper)
			where TEnum : Enumeration<TEnum>
		{
			mapper.RegisterValueType(typeof(TEnum));
			return mapper;
		}

		public static BsonMapper UseEnumerationName(this BsonMapper mapper, Assembly assembly)
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

		public static BsonMapper UseEnumerationValue(this BsonMapper mapper, Assembly assembly)
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
				EnumerationNameConverter.Serialize,
				EnumerationNameConverter.Deserialize(enumerationType));
		}

		private static void RegisterValueType(this BsonMapper mapper, Type enumerationType)
		{
			mapper.RegisterType(enumerationType,
				EnumerationValueConverter.Serialize,
				EnumerationValueConverter.Deserialize(enumerationType));
		}
	}
}
