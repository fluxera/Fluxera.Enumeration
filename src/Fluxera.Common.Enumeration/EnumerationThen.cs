namespace Fluxera.Enumeration
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public readonly struct EnumerationThen<TEnum> where TEnum : Enumeration<TEnum>
	{
		private readonly bool isMatch;
		private readonly Enumeration<TEnum> enumeration;
		private readonly bool stopEvaluating;

		internal EnumerationThen(bool isMatch, bool stopEvaluating, Enumeration<TEnum> enumeration)
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
		public EnumerationWhen<TEnum> Then(Action action)
		{
			if(!this.stopEvaluating && this.isMatch)
			{
				action.Invoke();
			}

			return new EnumerationWhen<TEnum>(this.stopEvaluating || this.isMatch, this.enumeration);
		}
	}
}
