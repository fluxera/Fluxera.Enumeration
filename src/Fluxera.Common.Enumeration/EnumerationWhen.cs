namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	[PublicAPI]
	public readonly struct EnumerationWhen<TEnum, TValue> 
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : struct,IComparable, IComparable<TValue>
	{
		private readonly Enumeration<TEnum, TValue> enumeration;
		private readonly bool stopEvaluating;

		internal EnumerationWhen(bool stopEvaluating, Enumeration<TEnum, TValue> enumeration)
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
		public EnumerationThen<TEnum, TValue> When(Enumeration<TEnum, TValue> enumerationWhen)
		{
			return new EnumerationThen<TEnum, TValue>(this.enumeration.Equals(enumerationWhen), this.stopEvaluating, this.enumeration);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum, TValue> WhenAny(params Enumeration<TEnum, TValue>[] enumerations)
		{
			return new EnumerationThen<TEnum, TValue>(enumerations.Contains(this.enumeration), this.stopEvaluating, this.enumeration);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum, TValue> WhenAny(IEnumerable<Enumeration<TEnum, TValue>> enumerations)
		{
			return new EnumerationThen<TEnum, TValue>(enumerations.Contains(this.enumeration), this.stopEvaluating, this.enumeration);
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

		/// <summary>
		///     Execute this action if no other calls to When/WhenAny have matched.
		/// </summary>
		/// <param name="action">The Action to call.</param>
		public void Default(Action<TEnum> action)
		{
			if(!this.stopEvaluating)
			{
				action.Invoke((TEnum)this.enumeration);
			}
		}
	}
}
