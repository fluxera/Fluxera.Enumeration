namespace Fluxera.Enumeration
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class EnumerationExtensions
	{
		/// <summary>
		///     Checks the given type if it is an <see cref="Enumeration{TEnum}" />.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>True, if the type is an enumeration, false otherwise.</returns>
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
	}
}
