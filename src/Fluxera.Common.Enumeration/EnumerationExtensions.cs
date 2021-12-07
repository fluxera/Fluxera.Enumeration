namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class EnumerationExtensions
	{
		public static bool IsEnumeration(this Type? type)
		{
			if(type is null || type.IsAbstract || type.IsGenericTypeDefinition)
			{
				return false;
			}

			do
			{
				if(type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Enumeration<>)))
				{
					return true;
				}

				type = type.BaseType;
			}
			while(type is not null);

			return false;
		}

		public static bool TryGetValues(this Type? type, out IEnumerable<object>? enumerations)
		{
			while(type != null)
			{
				if(type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Enumeration<>)))
				{
					PropertyInfo? listPropertyInfo = type.GetProperty("All", BindingFlags.Public | BindingFlags.Static);
					enumerations = (IEnumerable<object>)(listPropertyInfo?.GetValue(type, null) ?? Enumerable.Empty<object>());
					return true;
				}

				type = type.BaseType;
			}

			enumerations = null;
			return false;
		}
	}
}
