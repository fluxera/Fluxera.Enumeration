﻿namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Numerics;
	using System.Runtime.CompilerServices;
	using JetBrains.Annotations;

	internal static class Guard
	{
		public static T ThrowIfNull<T>(T argument, [InvokerParameterName] [CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			ArgumentNullException.ThrowIfNull(argument, parameterName);

			return argument;
		}

		public static string ThrowIfNullOrEmpty(string argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			argument = ThrowIfNull(argument, parameterName);

			if(string.IsNullOrEmpty(argument))
			{
				throw new ArgumentException("Value cannot be empty.", parameterName);
			}

			return argument;
		}

		public static string ThrowIfNullOrWhiteSpace(string argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			argument = ThrowIfNull(argument, parameterName);

			if(string.IsNullOrWhiteSpace(argument))
			{
				throw new ArgumentException("Value cannot be whitespace-only.", parameterName);
			}

			return argument;
		}

		public static bool ThrowIfFalse(bool argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null, string message = null)
		{
			if(!argument)
			{
				throw new ArgumentException(message ?? "Value cannot be false.", parameterName);
			}

			return true;
		}

		public static IEnumerable<T> ThrowIfNullOrEmpty<T>(IEnumerable<T> argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			argument = ThrowIfNull(argument, parameterName);

			// ReSharper disable PossibleMultipleEnumeration
			if(!argument.Any())
			{
				throw new ArgumentException("Enumerable cannot be empty.", parameterName);
			}

			return argument;
			// ReSharper enable PossibleMultipleEnumeration
		}

		public static T ThrowIfUnsupportedValueType<T>(T argument, [InvokerParameterName] [CallerArgumentExpression(nameof(argument))] string parameterName = null, string message = null)
		{
			argument = ThrowIfNull(argument, parameterName);

			Type valueType = argument.GetType();

			if(valueType != typeof(byte) &&
				valueType != typeof(short) &&
				valueType != typeof(int) &&
				valueType != typeof(long))
			{
				throw new ArgumentException(message ?? $"Value cannot be an unsupported type ({valueType}).", parameterName);
			}

			return argument;
		}

#if NET7_0_OR_GREATER
		public static T ThrowIfNegative<T>(T argument, [InvokerParameterName] [CallerArgumentExpression(nameof(argument))] string parameterName = null)
			where T : INumber<T>
		{
			if(T.IsNegative(argument))
			{
				throw new ArgumentException("Value cannot be negative.", parameterName);
			}

			return argument;
		}
#endif

#if NET6_0
		public static byte ThrowIfNegative(byte argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			if(argument < 0)
			{
				throw new ArgumentException("Value cannot be negative.", parameterName);
			}

			return argument;
		}

		public static short ThrowIfNegative(short argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			if(argument < 0)
			{
				throw new ArgumentException("Value cannot be negative.", parameterName);
			}

			return argument;
		}

		public static int ThrowIfNegative(int argument, [InvokerParameterName] [CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			if(argument < 0)
			{
				throw new ArgumentException("Value cannot be negative.", parameterName);
			}

			return argument;
		}

		public static long ThrowIfNegative(long argument, [InvokerParameterName][CallerArgumentExpression(nameof(argument))] string parameterName = null)
		{
			if(argument < 0)
			{
				throw new ArgumentException("Value cannot be negative.", parameterName);
			}

			return argument;
		}
#endif
	}
}
