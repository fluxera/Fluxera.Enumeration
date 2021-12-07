namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	[PublicAPI]
	public readonly struct EnumerationWhen<TEnum> where TEnum : Enumeration<TEnum>
	{
		private readonly Enumeration<TEnum> enumeration;
		private readonly bool stopEvaluating;

		internal EnumerationWhen(bool stopEvaluating, Enumeration<TEnum> enumeration)
		{
			this.stopEvaluating = stopEvaluating;
			this.enumeration = enumeration;
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerationWhen">A <see cref="Enumeration{TEnum}" /> value to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum> When(Enumeration<TEnum> enumerationWhen)
		{
			return new EnumerationThen<TEnum>(this.enumeration.Equals(enumerationWhen), this.stopEvaluating, this.enumeration);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum> WhenAny(params Enumeration<TEnum>[] enumerations)
		{
			return new EnumerationThen<TEnum>(enumerations.Contains(this.enumeration), this.stopEvaluating, this.enumeration);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum> WhenAny(IEnumerable<Enumeration<TEnum>> enumerations)
		{
			return new EnumerationThen<TEnum>(enumerations.Contains(this.enumeration), this.stopEvaluating, this.enumeration);
		}

		/// <summary>
		///     Execute this action if no other calls to When/WhenAny have matched.
		/// </summary>
		/// <param name="action">The Action to call.</param>
		public void Default(Action action)
		{
			if(!this.stopEvaluating)
			{
				action.Invoke();
			}
		}
	}
}
