namespace Fluxera.Enumeration
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public readonly struct EnumerationThen<TEnum, TValue> 
		where TEnum : Enumeration<TEnum, TValue>
		where TValue : struct, IComparable, IComparable<TValue>
	{
		private readonly bool isMatch;
		private readonly Enumeration<TEnum, TValue> enumeration;
		private readonly bool stopEvaluating;

		internal EnumerationThen(bool isMatch, bool stopEvaluating, Enumeration<TEnum, TValue> enumeration)
		{
			this.isMatch = isMatch;
			this.enumeration = enumeration;
			this.stopEvaluating = stopEvaluating;
		}

		/// <summary>
		///     Calls <paramref name="action" /> Action when the preceding When call matches.
		/// </summary>
		/// <param name="action">Action method to call.</param>
		/// <returns>A chainable instance of CaseWhen for more when calls.</returns>
		public EnumerationWhen<TEnum, TValue> Then(Action action)
		{
			if(!this.stopEvaluating && this.isMatch)
			{
				action.Invoke();
			}

			return new EnumerationWhen<TEnum, TValue>(this.stopEvaluating || this.isMatch, this.enumeration);
		}

		/// <summary>
		///     Calls <paramref name="action" /> Action when the preceding When call matches.
		/// </summary>
		/// <param name="action">Action method to call.</param>
		/// <returns>A chainable instance of CaseWhen for more when calls.</returns>
		public EnumerationWhen<TEnum, TValue> Then(Action<TEnum> action)
		{
			if(!this.stopEvaluating && this.isMatch)
			{
				action.Invoke((TEnum)this.enumeration);
			}

			return new EnumerationWhen<TEnum, TValue>(this.stopEvaluating || this.isMatch, this.enumeration);
		}
	}
}
