﻿namespace Fluxera.Enumeration
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="Type" /> type.
	/// </summary>
	[PublicAPI]
	public static class EnumerationExtensions
	{
		/// <summary>
		///     Checks the given type if it is an <see cref="Enumeration{TEnum, TValue}" />.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>True, if the type is an enumeration, false otherwise.</returns>
		public static bool IsEnumeration(this Type type)
		{
			if(type is null || type.IsAbstract || type.IsGenericTypeDefinition)
			{
				return false;
			}

			do
			{
				if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Enumeration<,>))
				{
					return true;
				}

				type = type.BaseType;
			}
			while(type is not null);

			return false;
		}

		/// <summary>
		///     Gets the type of the generic value parameter from the base type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns>The type of the value.</returns>
		public static Type GetValueType(this Type type)
		{
			do
			{
				if(type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Enumeration<,>))
				{
					Type valueType = type.GetGenericArguments()[1];
					return valueType;
				}

				type = type?.BaseType;
			}
			while(type is not null);

			return null!;
		}
	}
}
