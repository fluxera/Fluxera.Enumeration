namespace Fluxera.Enumeration
{
	using System;
	using System.Diagnostics;
	using JetBrains.Annotations;

	/// <summary>
	///     A base type for creating object-oriented flag enums.
	/// </summary>
	/// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
	[PublicAPI]
	[Serializable]
	[DebuggerDisplay("{Name})")]
	public abstract class FlagsEnumeration<TEnum> : Enumeration<TEnum>
		where TEnum : FlagsEnumeration<TEnum>
	{
		/// <inheritdoc />
		protected FlagsEnumeration(string name, int value) 
			: base(name, value)
		{
		}

		//public static Enumeration<TEnum> operator |(Enumeration<TEnum> left, Enumeration<TEnum> right)
		//{
		//	return null;
		//}

		//public static Enumeration<TEnum> operator &(Enumeration<TEnum> left, Enumeration<TEnum> right)
		//{
		//	return null;
		//}

		//public static Enumeration<TEnum> operator ^(Enumeration<TEnum> left, Enumeration<TEnum> right)
		//{
		//	return null;
		//}

		//public bool HasFlag(Enumeration<TEnum> enumeration)
		//{
		//	return false;
		//}
	}
}
