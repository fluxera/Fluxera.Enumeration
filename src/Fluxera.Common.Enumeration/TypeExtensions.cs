namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	internal static class TypeExtensions
	{
		public static IEnumerable<TEnum> GetEnumFields<TEnum, TValue>(this Type type)
			where TEnum : Enumeration<TEnum, TValue>
			where TValue : IComparable, IComparable<TValue>
		{
			return type
				.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
				.Where(fieldInfo => type.IsAssignableFrom(fieldInfo.FieldType))
				.Select(fieldInfo => (TEnum)fieldInfo.GetValue(null));
		}

		public static IEnumerable<IEnumeration> GetEnumFields(this Type type)
		{
			return type
				.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
				.Where(fieldInfo => type.IsAssignableFrom(fieldInfo.FieldType))
				.Select(fieldInfo => (IEnumeration)fieldInfo.GetValue(null));
		}
	}
}
