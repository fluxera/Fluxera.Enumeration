namespace Fluxera.Enumeration
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     A base type for creating object-oriented enums.
	/// </summary>
	/// <typeparam name="TEnum">The type that is inheriting from this class.</typeparam>
	[PublicAPI]
	[Serializable]
	[DebuggerDisplay("{Name}")]
	public abstract class Enumeration<TEnum> : IEnumeration, IComparable<TEnum>, IEquatable<TEnum>
		where TEnum : Enumeration<TEnum>
	{
		private static Lazy<TEnum[]> enumOptions = new Lazy<TEnum[]>(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);
		private static Lazy<IDictionary<string, TEnum>> parseName = new Lazy<IDictionary<string, TEnum>>(GetParseNameDict);
		private static Lazy<IDictionary<string, TEnum>> parseNameIgnoreCase = new Lazy<IDictionary<string, TEnum>>(GetParseNameIgnoreCaseDict);
		private static Lazy<IDictionary<int, TEnum>> parseValue = new Lazy<IDictionary<int, TEnum>>(GetParseValueDict);

		protected Enumeration(string name, int value)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.Negative(value, nameof(value));

			this.Name = name;
			this.Value = value;
		}

		/// <summary>
		///     Gets a read-only collection of all options of <see cref="Enumeration{TEnum}" />.
		/// </summary>
		/// <remarks>
		///     Gets all options of <see cref="Enumeration{TEnum}" /> that are implemented as
		///		public static read-only fields in the current class or in base classes.
		/// </remarks>
		public static IReadOnlyCollection<TEnum> All => parseName.Value.Values.ToList().AsReadOnly();

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
		public static TEnum ParseName(string name, bool ignoreCase = false)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			IDictionary<string, TEnum> dictionary = ignoreCase
				? parseNameIgnoreCase.Value
				: parseName.Value;

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
		public static bool TryParseName(string? name, out TEnum? result)
		{
			return TryParseName(name, false, out result);
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
		public static bool TryParseName(string? name, bool ignoreCase, out TEnum? result)
		{
			if(string.IsNullOrWhiteSpace(name))
			{
				result = default;
				return false;
			}

			return ignoreCase
				? parseNameIgnoreCase.Value.TryGetValue(name, out result)
				: parseName.Value.TryGetValue(name, out result);
		}

		/// <summary>
		///     Gets an item associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the item to get.</param>
		/// <returns></returns>
		public static TEnum ParseValue(int value)
		{
			Guard.Against.Negative(value, nameof(value));

			if(!parseValue.Value.TryGetValue(value, out TEnum? result))
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
		public static TEnum ParseValue(int value, TEnum defaultValue)
		{
			Guard.Against.Null(defaultValue, nameof(defaultValue));

			if(value < 0)
			{
				return defaultValue;
			}

			if(!parseValue.Value.TryGetValue(value, out TEnum? result))
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
		public static bool TryParseValue(int value, out TEnum? result)
		{
			if(value < 0)
			{
				result = default;
				return false;
			}

			return parseValue.Value.TryGetValue(value, out result);
		}

		/// <inheritdoc />
		public sealed override string ToString()
		{
			return this.Name;
		}

		/// <inheritdoc />
		public sealed override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <inheritdoc />
		public sealed override bool Equals(object? obj)
		{
			return obj is Enumeration<TEnum> other && this.Equals((TEnum)other);
		}

		/// <inheritdoc />
		public bool Equals(TEnum? other)
		{
			// Check if it's the same instance.
			if(ReferenceEquals(this, other))
			{
				return true;
			}

			// It's not the same instance check if it is not null and has the same value.
			if(other is null)
			{
				return false;
			}

			return this.Value.Equals(other.Value);
		}

		/// <inheritdoc />
		public int CompareTo(TEnum other)
		{
			return this.Value.CompareTo(other.Value);
		}

		/// <inheritdoc />
		public int CompareTo(object obj)
		{
			return this.CompareTo((TEnum)obj);
		}

		public static bool operator ==(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
		{
			// Handle null on the left side.
			if(left is null)
			{
				// null == null = true
				return right is null;
			}

			// Equals handles null on the right side.
			return left.Equals(right);
		}

		public static bool operator !=(Enumeration<TEnum>? left, Enumeration<TEnum>? right)
		{
			return !(left == right);
		}

		public static bool operator <(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo(right) < 0;
		}

		public static bool operator <=(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo((TEnum)right) <= 0;
		}

		public static bool operator >(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo((TEnum)right) > 0;
		}

		public static bool operator >=(Enumeration<TEnum> left, Enumeration<TEnum> right)
		{
			return left.CompareTo((TEnum)right) >= 0;
		}

		public static implicit operator int(Enumeration<TEnum> enumeration)
		{
			return enumeration.Value;
		}

		public static explicit operator Enumeration<TEnum>(int value)
		{
			return ParseValue(value);
		}

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

		private static IDictionary<string, TEnum> GetParseNameDict()
		{
			return enumOptions.Value.ToDictionary(item => item.Name);
		}

		private static IDictionary<string, TEnum> GetParseNameIgnoreCaseDict()
		{
			return enumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase);
		}

		private static IDictionary<int, TEnum> GetParseValueDict()
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
		}
	}

	/// <summary>
	///		Helper class to have access to the Parse and TryParse methods from a context where
	///		the generic type of the enumeration is not known.
	/// </summary>
	[PublicAPI]
	public static class Enumeration
	{
		private static IDictionary<Type, IEnumeration[]> globalEnumOptions = new Dictionary<Type, IEnumeration[]>();
		private static IDictionary<Type, IDictionary<string, IEnumeration>> globalParseName = new Dictionary<Type, IDictionary<string, IEnumeration>>();
		private static IDictionary<Type, IDictionary<string, IEnumeration>> globalParseNameIgnoreCase = new Dictionary<Type, IDictionary<string, IEnumeration>>();
		private static IDictionary<Type, IDictionary<int, IEnumeration>> globalParseValue = new Dictionary<Type, IDictionary<int, IEnumeration>>();

		public static IEnumeration ParseName(Type enumerationType, string name, bool ignoreCase = false)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			IDictionary<string, IEnumeration> parseName = GetParseNameDict(enumerationType, ignoreCase);
			if(!parseName.TryGetValue(name, out IEnumeration result))
			{
				throw new InvalidEnumArgumentException($"No {enumerationType.Name} with name '{name}' found.");
			}

			return result;
		}

		public static bool TryParseName(Type enumerationType, string? name, out IEnumeration? result)
		{
			return TryParseName(enumerationType, name, false, out result);
		}

		public static bool TryParseName(Type enumerationType, string? name, bool ignoreCase, out IEnumeration? result)
		{
			if(string.IsNullOrWhiteSpace(name))
			{
				result = default;
				return false;
			}

			IDictionary<string, IEnumeration> parseName = GetParseNameDict(enumerationType, ignoreCase);
			return parseName.TryGetValue(name, out result);
		}

		public static IEnumeration ParseValue(Type enumerationType, int value)
		{
			Guard.Against.Negative(value, nameof(value));

			IDictionary<int, IEnumeration> parseValue = GetParseValueDict(enumerationType);
			if(!parseValue.TryGetValue(value, out IEnumeration? result))
			{
				throw new InvalidEnumArgumentException($"No {enumerationType.Name} with value '{value}' found.");
			}

			return result;
		}

		public static IEnumeration? ParseValue(Type enumerationType, int value, IEnumeration defaultValue)
		{
			Guard.Against.Null(defaultValue, nameof(defaultValue));

			if(value < 0)
			{
				return defaultValue;
			}

			IDictionary<int, IEnumeration> parseValue = GetParseValueDict(enumerationType);
			if(!parseValue.TryGetValue(value, out IEnumeration? result))
			{
				return defaultValue;
			}

			return result;
		}

		public static bool TryParseValue(Type enumerationType, int value, out IEnumeration? result)
		{
			if(value < 0)
			{
				result = default;
				return false;
			}

			IDictionary<int, IEnumeration> parseValue = GetParseValueDict(enumerationType);
			return parseValue.TryGetValue(value, out result);
		}

		public static IEnumeration[] GetAllOptionsFor(Type enumerationType)
		{
			// Populate the global options dictionary.
			if(!globalEnumOptions.ContainsKey(enumerationType))
			{
				globalEnumOptions.Add(enumerationType, GetAllOptions(enumerationType));
			}

			IEnumeration[] enumOptions = globalEnumOptions[enumerationType];
			return enumOptions;
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

		private static IDictionary<string, IEnumeration> GetParseNameDict(Type enumerationType, bool ignoreCase)
		{
			IEnumeration[] enumOptions = GetAllOptionsFor(enumerationType);

			// Populate the global from name dictionaries.
			IDictionary<string, IEnumeration> parseNameDict;
			if(ignoreCase)
			{
				if(!globalParseNameIgnoreCase.ContainsKey(enumerationType))
				{
					globalParseNameIgnoreCase.Add(enumerationType, enumOptions.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));
				}

				parseNameDict = globalParseName[enumerationType];
			}
			else
			{
				if(!globalParseName.ContainsKey(enumerationType))
				{
					globalParseName.Add(enumerationType, enumOptions.ToDictionary(item => item.Name));
				}

				parseNameDict = globalParseName[enumerationType];
			}

			return parseNameDict;
		}

		private static IDictionary<int, IEnumeration> GetParseValueDict(Type enumerationType)
		{
			IEnumeration[] enumOptions = GetAllOptionsFor(enumerationType);

			// Populate the global from value dictionary.
			if(!globalParseValue.ContainsKey(enumerationType))
			{
				globalParseValue.Add(enumerationType, enumOptions.ToDictionary(item => item.Value));
			}

			IDictionary<int, IEnumeration> parseValueDict = globalParseValue[enumerationType];
			return parseValueDict;
		}
	}
}
