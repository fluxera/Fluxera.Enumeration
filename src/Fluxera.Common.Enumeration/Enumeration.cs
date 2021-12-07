namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Net;
	using System.Reflection;
	using System.Runtime.CompilerServices;
	using System.Threading;
	using Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     A base type for creating object-oriented enums.
	/// </summary>
	/// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
	[PublicAPI]
	[Serializable]
	public abstract class Enumeration<TEnum> : IEnumeration //, IComparable, IComparable<TEnum>, IEquatable<TEnum>
		where TEnum : Enumeration<TEnum>
	{
		private static Lazy<TEnum[]> enumOptions =
			new Lazy<TEnum[]>(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

		private static Lazy<IDictionary<string, TEnum>> fromName =
			new Lazy<IDictionary<string, TEnum>>(() => enumOptions.Value.ToDictionary(item => item.Name));

		private static Lazy<IDictionary<string, TEnum>> fromNameIgnoreCase =
			new Lazy<IDictionary<string, TEnum>>(() => enumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

		private static Lazy<IDictionary<int, TEnum>> fromValue =
			new Lazy<IDictionary<int, TEnum>>(() =>
			{
				// Multiple enums with same value are allowed but store only one per value.
				IDictionary<int, TEnum> dictionary = new Dictionary<int, TEnum>();
				foreach(TEnum item in enumOptions.Value)
				{
					if(!dictionary.ContainsKey(item.Value))
					{
						dictionary.Add(item.Value, item);
					}
				}

				return dictionary;
			});

		protected Enumeration(string name, int value)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.Negative(value, nameof(value));

			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		///     Gets a collection of all instances of <see cref="Enumeration{TEnum}" />.
		/// </summary>
		/// <remarks>
		///     Retrieves all instances of <see cref="Enumeration{TEnum}" /> referenced by public static read-only fields in
		///     the current class or its base classes.
		/// </remarks>
		public static IReadOnlyCollection<TEnum> All => fromName.Value.Values.ToList().AsReadOnly();

		/// <summary>
		///     Gets the name of the enum option.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     Gets the numeric value of the enum option.
		/// </summary>
		public int Value { get; }

		/// <summary>
		///     Gets the item associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the item to get.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
		/// <returns></returns>
		/// <exception cref="InvalidEnumArgumentException"></exception>
		public static TEnum FromName(string name, bool ignoreCase = false)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			IDictionary<string, TEnum> dictionary = ignoreCase
				? fromNameIgnoreCase.Value
				: fromName.Value;

			if(!dictionary.TryGetValue(name, out TEnum result))
			{
				throw new InvalidEnumArgumentException($"No {typeof(TEnum).Name} with name '{name}' found.");
			}

			return result;
		}

		/// <summary>
		///     Gets the item associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the item to get.</param>
		/// <param name="result">
		///     When this method returns, contains the item associated with the specified name, if the key is found;
		///     otherwise, <c>null</c>. This parameter is passed uninitialized.
		/// </param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFromName(string? name, out TEnum? result)
		{
			return TryFromName(name, false, out result);
		}

		/// <summary>
		///     Gets the item associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the item to get.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore case during the comparison; otherwise, <c>false</c>.</param>
		/// <param name="result">
		///     When this method returns, contains the item associated with the specified name, if the name is found;
		///     otherwise, <c>null</c>. This parameter is passed uninitialized.
		/// </param>
		/// <returns></returns>
		public static bool TryFromName(string? name, bool ignoreCase, out TEnum? result)
		{
			if(string.IsNullOrWhiteSpace(name))
			{
				result = default;
				return false;
			}

			return ignoreCase
				? fromNameIgnoreCase.Value.TryGetValue(name, out result)
				: fromName.Value.TryGetValue(name, out result);
		}

		/// <summary>
		///     Gets an item associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the item to get.</param>
		/// <returns></returns>
		public static TEnum FromValue(int value)
		{
			Guard.Against.Negative(value, nameof(value));

			if(!fromValue.Value.TryGetValue(value, out TEnum? result))
			{
				throw new InvalidEnumArgumentException($"No {typeof(TEnum).Name} with value '{value}' found.");
			}

			return result;
		}

		/// <summary>
		///     Gets an item associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the item to get.</param>
		/// <param name="defaultValue">The value to return when item not found.</param>
		/// <returns>
		///     The first item found that is associated with the specified value.
		///     If the specified value is not found, returns <paramref name="defaultValue" />.
		/// </returns>
		public static TEnum FromValue(int value, TEnum defaultValue)
		{
			Guard.Against.Null(defaultValue, nameof(defaultValue));

			if(value < 0)
			{
				return defaultValue;
			}

			if(!fromValue.Value.TryGetValue(value, out TEnum? result))
			{
				return defaultValue;
			}

			return result;
		}

		/// <summary>
		///     Gets an item associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the item to get.</param>
		/// <param name="result">
		///     When this method returns, contains the item associated with the specified value, if the value is found;
		///     otherwise, <c>null</c>. This parameter is passed uninitialized.
		/// </param>
		/// <returns></returns>
		public static bool TryFromValue(int value, out TEnum? result)
		{
			if(value < 0)
			{
				result = default;
				return false;
			}

			return fromValue.Value.TryGetValue(value, out result);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Name;
		}

		/// <inheritdoc />
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			return obj is Enumeration<TEnum> other && this.Equals(other);
		}

		public virtual bool Equals(Enumeration<TEnum>? other)
		{
			// Check if same instance.
			if(ReferenceEquals(this, other))
			{
				return true;
			}

			// It's not same instance so check if it's not null and is the same value.
			if(other is null)
			{
				return false;
			}

			return this.Value.Equals(other.Value);
		}

		public static bool operator ==(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
		{
			// Handle null on left side.
			if(left is null)
			{
				return right is null; // null == null = true
			}

			// Equals handles null on right side.
			return left.Equals(right);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
		{
			return !(left == right);
		}

		/// <summary>
		///     Compares this instance to a specified <see cref="Enumeration{TEnum}" /> and returns an indication of their relative
		///     values.
		/// </summary>
		/// <param name="other">An <see cref="Enumeration{TEnum}" /> value to compare to this instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="other" />.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public virtual int CompareTo(Enumeration<TEnum> other)
		{
			return this.Value.CompareTo(other.Value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo(right) < 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator <=(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo(right) <= 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo(right) > 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo(right) >= 0;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int(Enumeration<TEnum> enumeration)
		{
			return enumeration.Value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Enumeration<TEnum>(int value)
		{
			return FromValue(value);
		}

		// TODO: Move to a special Flags Enum
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

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumeration">A <see cref="Enumeration{TEnum}" /> value to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum> When(Enumeration<TEnum> enumeration)
		{
			return new EnumerationThen<TEnum>(this.Equals(enumeration), false, this);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum> WhenAny(params Enumeration<TEnum>[] enumerations)
		{
			return new EnumerationThen<TEnum>(enumerations.Contains(this), false, this);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="Enumeration{TEnum}" /> parameters.
		///     Execute the action in the subsequent call to Then().
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>A executor object to execute a supplied action.</returns>
		public EnumerationThen<TEnum> WhenAny(IEnumerable<Enumeration<TEnum>> enumerations)
		{
			return new EnumerationThen<TEnum>(enumerations.Contains(this), false, this);
		}

		private static TEnum[] GetAllOptions()
		{
			Type baseType = typeof(TEnum);
			return Assembly.GetAssembly(baseType)
				.GetTypes()
				.Where(t => baseType.IsAssignableFrom(t))
				.SelectMany(t => t.GetEnumFields<TEnum>())
				.OrderBy(t => t.Value)
				.ToArray();
		}
	}

	[PublicAPI]
	public static class Enumeration
	{
		private static IDictionary<Type, IEnumeration[]> globalEnumOptions = new Dictionary<Type, IEnumeration[]>();
		private static IDictionary<Type, IDictionary<string, IEnumeration>> globalFromName = new Dictionary<Type, IDictionary<string, IEnumeration>>();
		private static IDictionary<Type, IDictionary<string, IEnumeration>> globalFromNameIgnoreCase = new Dictionary<Type, IDictionary<string, IEnumeration>>();
		private static IDictionary<Type, IDictionary<int, IEnumeration>> globalFromValue = new Dictionary<Type, IDictionary<int, IEnumeration>>();

		public static bool TryFromName(Type enumerationType, string? name, out IEnumeration? result)
		{
			return TryFromName(enumerationType, name, false, out result);
		}

		public static bool TryFromName(Type enumerationType, string? name, bool ignoreCase, out IEnumeration? result)
		{
			if(string.IsNullOrWhiteSpace(name))
			{
				result = default;
				return false;
			}

			// Populate the global options dictionary.
			if(!globalEnumOptions.ContainsKey(enumerationType))
			{
				globalEnumOptions.Add(enumerationType, GetAllOptions(enumerationType));
			}

			IEnumeration[] enumOptions = globalEnumOptions[enumerationType];

			// Populate the global from name dictionaries.
			IDictionary<string, IEnumeration> fromName;
			if(ignoreCase)
			{
				if(!globalFromNameIgnoreCase.ContainsKey(enumerationType))
				{
					globalFromNameIgnoreCase.Add(enumerationType, enumOptions.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));
				}

				fromName = globalFromName[enumerationType];
			}
			else
			{
				if(!globalFromName.ContainsKey(enumerationType))
				{
					globalFromName.Add(enumerationType, enumOptions.ToDictionary(item => item.Name));
				}

				fromName = globalFromName[enumerationType];
			}

			return fromName.TryGetValue(name, out result);
		}

		public static bool TryFromValue(Type enumerationType, int value, out IEnumeration? result)
		{
			if(value < 0)
			{
				result = default;
				return false;
			}

			// Populate the global options dictionary.
			if(!globalEnumOptions.ContainsKey(enumerationType))
			{
				globalEnumOptions.Add(enumerationType, GetAllOptions(enumerationType));
			}

			IEnumeration[] enumOptions = globalEnumOptions[enumerationType];

			// Populate the global from value dictionary.
			if(!globalFromValue.ContainsKey(enumerationType))
			{
				globalFromValue.Add(enumerationType, enumOptions.ToDictionary(item => item.Value));
			}

			IDictionary<int, IEnumeration> fromValue = globalFromValue[enumerationType];

			return fromValue.TryGetValue(value, out result);
		}

		private static IEnumeration[] GetAllOptions(Type enumerationType)
		{
			Type baseType = enumerationType;
			return Assembly.GetAssembly(baseType)
				.GetTypes()
				.Where(t => baseType.IsAssignableFrom(t))
				.SelectMany(t => t.GetEnumFields())
				.OrderBy(t => t.Value)
				.ToArray();
		}
	}
}
