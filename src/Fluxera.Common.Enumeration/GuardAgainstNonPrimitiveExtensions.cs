namespace Fluxera.Enumeration
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using static Fluxera.Guards.ExceptionHelpers;

	[PublicAPI]
	public static class GuardAgainstNonPrimitiveExtensions
	{
		/// <summary>
		///		Checks if the type of the value is one of the supported ones.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="guard">The guard.</param>
		/// <param name="input">The input value.</param>
		/// <param name="parameterName">The parameter name.</param>
		/// <param name="message">The optional error message.</param>
		/// <returns>The input if the check was successful.</returns>
		public static T UnsupportedValueType<T>(this IGuard guard, T input, [InvokerParameterName] string parameterName, string? message = null)
		{
			Guard.Against.Null(input, nameof(input));

			Type valueType = input.GetType();

			if(valueType != typeof(byte) &&
			   valueType != typeof(short) &&
			   valueType != typeof(int) &&
			   valueType != typeof(long) &&
			   valueType != typeof(float) &&
			   valueType != typeof(double) &&
			   valueType != typeof(decimal) &&
			   valueType != typeof(string) &&
			   valueType != typeof(DateTime) &&
			   valueType != typeof(DateTimeOffset) &&
			   valueType != typeof(TimeSpan) &&
			   valueType != typeof(Guid))
			{
				throw CreateArgumentException(parameterName, message ?? "Value cannot be an unsupported type.");
			}

			return input;
		}
	}
}
