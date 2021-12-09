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
	///     A base class for implementing object-oriented enumerations.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
	[PublicAPI]
	[Serializable]
	[DebuggerDisplay("{Name}")]
	public abstract class Enumeration<TEnum> : IEnumeration, IComparable<TEnum>, IEquatable<TEnum>
		where TEnum : Enumeration<TEnum>
	{
		private static Lazy<TEnum[]> enumOptions = new Lazy<TEnum[]>(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);
		private static Lazy<IDictionary<int, TEnum>> parseValue = new Lazy<IDictionary<int, TEnum>>(GetParseValueDict);
		private static Lazy<IDictionary<string, TEnum>> parseName = new Lazy<IDictionary<string, TEnum>>(GetParseNameDict);
		private static Lazy<IDictionary<string, TEnum>> parseNameIgnoreCase = new Lazy<IDictionary<string, TEnum>>(GetParseNameIgnoreCaseDict);

		protected Enumeration(int value, string name)
		{
			Guard.Against.Negative(value, nameof(value));
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			this.Value = value;
			this.Name = name;
		}

		/// <summary>
		///     Gets an array of all enum options of <see cref="TEnum" />.
		/// </summary>
		public static TEnum[] All => parseName.Value.Values.ToArray();

		/// <inheritdoc />
		public int CompareTo(TEnum? other)
		{
			// https://stackoverflow.com/a/23787253
			// http://msdn.microsoft.com/en-us/library/43hc6wht.aspx
			// http://msdn.microsoft.com/en-us/library/system.icomparable.compareto.aspx

			// By definition, any object compares greater than null, and two null references compare equal to each other.
			if(other is null)
			{
				return 1;
			}

			return this.Value.CompareTo(other.Value);
		}

		/// <summary>
		///     Gets the value of the enum option.
		/// </summary>
		public int Value { get; }

		/// <summary>
		///     Gets the name of the enum option.
		/// </summary>
		public string Name { get; }

		/// <inheritdoc />
		public int CompareTo(object? obj)
		{
			// https://stackoverflow.com/a/23787253
			// http://msdn.microsoft.com/en-us/library/43hc6wht.aspx
			// http://msdn.microsoft.com/en-us/library/system.icomparable.compareto.aspx

			// By definition, any object compares greater than null, and two null references compare equal to each other.
			if(obj is null)
			{
				return 1;
			}

			// The parameter, obj, must be the same type as the class or value type that implements
			// this interface; otherwise, an ArgumentException is thrown.
			if(obj.GetType() != typeof(TEnum))
			{
				throw new ArgumentException("Cannot compare different types.", nameof(obj));
			}

			return this.CompareTo((TEnum)obj);
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

		/// <summary>
		///     Gets an option associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the option to get.</param>
		/// <returns>The associated enum option.</returns>
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
		///     Gets an option associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the value to get.</param>
		/// <param name="defaultValue">The value to return when the desired option is not found.</param>
		/// <returns>
		///     The associated enum option. If the desired option is not found the <paramref name="defaultValue" /> is
		///     returned.
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
		///     Tries to get an option associated with the specified value.
		/// </summary>
		/// <param name="value">The value of the option to get.</param>
		/// <param name="result">
		///     Will contain the associated enum option after this method returns, if the value is found;
		///     otherwise, <c>null</c>.
		/// </param>
		/// <returns><c>true</c> if the name is found, <c>false</c> otherwise.</returns>
		public static bool TryParseValue(int value, out TEnum? result)
		{
			if(value < 0)
			{
				result = default;
				return false;
			}

			return parseValue.Value.TryGetValue(value, out result);
		}

		/// <summary>
		///     Gets the option associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the option to get.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore the case of the name during the comparison; otherwise, <c>false</c>.</param>
		/// <returns>The associated enum option.</returns>
		/// <exception cref="InvalidEnumArgumentException">Thrown when no option was found for the name.</exception>
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
		///     Tries to get the option associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the option to get.</param>
		/// <param name="result">
		///     Will contain the associated enum option after this method returns, if the name is found;
		///     otherwise, <c>null</c>.
		/// </param>
		/// <returns><c>true</c> if the name is found, <c>false</c> otherwise.</returns>
		public static bool TryParseName(string? name, out TEnum? result)
		{
			return TryParseName(name, false, out result);
		}

		/// <summary>
		///     Tries to get the option associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the option to get.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore the case of the name during the comparison; otherwise, <c>false</c>.</param>
		/// <param name="result">
		///     Will contain the associated enum option after this method returns, if the name is found;
		///     otherwise, <c>null</c>.
		/// </param>
		/// <returns><c>true</c> if the name is found, <c>false</c> otherwise.</returns>
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

		public static explicit operator int(Enumeration<TEnum> enumeration)
		{
			return enumeration.Value;
		}

		public static explicit operator string(Enumeration<TEnum> enumeration)
		{
			return enumeration.Name;
		}

		public static explicit operator Enumeration<TEnum>(int value)
		{
			return ParseValue(value);
		}

		public static explicit operator Enumeration<TEnum>(string name)
		{
			return name is not null ? ParseName(name) : null;
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="TEnum" /> parameters.
		///     Execute the action that is configured in the <see cref="EnumerationThen{TEnum}" />.
		/// </summary>
		/// <param name="enumeration">A <see cref="TEnum" /> value to compare to this instance.</param>
		/// <returns>An object to configure an action to execute.</returns>
		public EnumerationThen<TEnum> When(Enumeration<TEnum> enumeration)
		{
			return new EnumerationThen<TEnum>(this.Equals(enumeration), false, this);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="TEnum" /> parameters.
		///     Execute the action that is configured in the <see cref="EnumerationThen{TEnum}" />.
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="TEnum" /> values to compare to this instance.</param>
		/// <returns>An object to configure an action to execute.</returns>
		public EnumerationThen<TEnum> WhenAny(params Enumeration<TEnum>[] enumerations)
		{
			return new EnumerationThen<TEnum>(enumerations.Contains(this), false, this);
		}

		/// <summary>
		///     When this instance is one of the specified <see cref="TEnum" /> parameters.
		///     Execute the action that is configured in the <see cref="EnumerationThen{TEnum}" />.
		/// </summary>
		/// <param name="enumerations">A collection of <see cref="Enumeration{TEnum}" /> values to compare to this instance.</param>
		/// <returns>An object to configure an action to execute.</returns>
		public EnumerationThen<TEnum> WhenAny(IEnumerable<Enumeration<TEnum>> enumerations)
		{
			return new EnumerationThen<TEnum>(enumerations.Contains(this), false, this);
		}

		private static TEnum[] GetAllOptions()
		{
			Type enumType = typeof(TEnum);
			return Assembly.GetAssembly(enumType)
				.GetTypes()
				.Where(t => enumType.IsAssignableFrom(t))
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

		/// <exception cref="KeyNotFoundException"></exception>
		private static IDictionary<int, TEnum> GetParseValueDict()
		{
			// Multiple enum options with same value are not allowed.
			IDictionary<int, TEnum> dictionary = new Dictionary<int, TEnum>();
			foreach(TEnum item in enumOptions.Value)
			{
				dictionary.Add(item.Value, item);
			}

			return dictionary;
		}
	}

	/// <summary>
	///     Helper class to have access to the Parse and TryParse methods from a context where
	///     the generic type of the enumeration is not known.
	/// </summary>
	[PublicAPI]
	public static class Enumeration
	{
		private static IDictionary<Type, IEnumeration[]> globalEnumOptions = new Dictionary<Type, IEnumeration[]>();
		private static IDictionary<Type, IDictionary<int, IEnumeration>> globalParseValue = new Dictionary<Type, IDictionary<int, IEnumeration>>();
		private static IDictionary<Type, IDictionary<string, IEnumeration>> globalParseName = new Dictionary<Type, IDictionary<string, IEnumeration>>();
		private static IDictionary<Type, IDictionary<string, IEnumeration>> globalParseNameIgnoreCase = new Dictionary<Type, IDictionary<string, IEnumeration>>();

		/// <summary>
		///     Gets an option associated with the specified value.
		/// </summary>
		/// <param name="enumerationType">The enum type to parse for.</param>
		/// <param name="value">The value of the option to get.</param>
		/// <returns>The associated enum option.</returns>
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

		/// <summary>
		///     Gets an option associated with the specified value.
		/// </summary>
		/// <param name="enumerationType">The enum type to parse for.</param>
		/// <param name="value">The value of the value to get.</param>
		/// <param name="defaultValue">The value to return when the desired option is not found.</param>
		/// <returns>
		///     The associated enum option. If the desired option is not found the <paramref name="defaultValue" /> is
		///     returned.
		/// </returns>
		public static IEnumeration ParseValue(Type enumerationType, int value, IEnumeration defaultValue)
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

		/// <summary>
		///     Tries to get an option associated with the specified value.
		/// </summary>
		/// <param name="enumerationType">The enum type to parse for.</param>
		/// <param name="value">The value of the option to get.</param>
		/// <param name="result">
		///     Will contain the associated enum option after this method returns, if the value is found;
		///     otherwise, <c>null</c>.
		/// </param>
		/// <returns><c>true</c> if the name is found, <c>false</c> otherwise.</returns>
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

		/// <summary>
		///     Gets the option associated with the specified name.
		/// </summary>
		/// <param name="enumerationType">The enum type to parse for.</param>
		/// <param name="name">The name of the option to get.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore the case of the name during the comparison; otherwise, <c>false</c>.</param>
		/// <returns>The associated enum option.</returns>
		/// <exception cref="InvalidEnumArgumentException">Thrown when no option was found for the name.</exception>
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

		/// <summary>
		///     Tries to get the option associated with the specified name.
		/// </summary>
		/// <param name="enumerationType">The enum type to parse for.</param>
		/// <param name="name">The name of the option to get.</param>
		/// <param name="result">
		///     Will contain the associated enum option after this method returns, if the name is found;
		///     otherwise, <c>null</c>.
		/// </param>
		/// <returns><c>true</c> if the name is found, <c>false</c> otherwise.</returns>
		public static bool TryParseName(Type enumerationType, string? name, out IEnumeration? result)
		{
			return TryParseName(enumerationType, name, false, out result);
		}

		/// <summary>
		///     Tries to get the option associated with the specified name.
		/// </summary>
		/// <param name="enumerationType">The enum type to parse for.</param>
		/// <param name="name">The name of the option to get.</param>
		/// <param name="ignoreCase"><c>true</c> to ignore the case of the name during the comparison; otherwise, <c>false</c>.</param>
		/// <param name="result">
		///     Will contain the associated enum option after this method returns, if the name is found;
		///     otherwise, <c>null</c>.
		/// </param>
		/// <returns><c>true</c> if the name is found, <c>false</c> otherwise.</returns>
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

		/// <summary>
		///     Gets an array of all enum options of <paramref name="enumerationType" />.
		/// </summary>
		/// <param name="enumerationType"></param>
		/// <returns></returns>
		public static IEnumeration[] All(Type enumerationType)
		{
			return GetAllOptions(enumerationType);
		}

		private static IEnumeration[] GetAllOptions(Type enumerationType)
		{
			IEnumeration[] enumOptions;

			// Populate the global options dictionary.
			if(!globalEnumOptions.ContainsKey(enumerationType))
			{
				Type baseType = enumerationType;
				enumOptions = Assembly.GetAssembly(baseType)
					.GetTypes()
					.Where(t => baseType.IsAssignableFrom(t))
					.SelectMany(t => t.GetEnumFields())
					.OrderBy(t => t.Value)
					.ToArray();

				globalEnumOptions.Add(enumerationType, enumOptions);
			}
			else
			{
				enumOptions = globalEnumOptions[enumerationType];
			}

			return enumOptions;
		}

		private static IDictionary<int, IEnumeration> GetParseValueDict(Type enumerationType)
		{
			IEnumeration[] enumOptions = GetAllOptions(enumerationType);

			// Populate the global from value dictionary.
			if(!globalParseValue.ContainsKey(enumerationType))
			{
				globalParseValue.Add(enumerationType, enumOptions.ToDictionary(item => item.Value));
			}

			IDictionary<int, IEnumeration> parseValueDict = globalParseValue[enumerationType];
			return parseValueDict;
		}

		private static IDictionary<string, IEnumeration> GetParseNameDict(Type enumerationType, bool ignoreCase)
		{
			IEnumeration[] enumOptions = GetAllOptions(enumerationType);

			// Populate the global from name dictionaries.
			IDictionary<string, IEnumeration> parseNameDict;
			if(ignoreCase)
			{
				if(!globalParseNameIgnoreCase.ContainsKey(enumerationType))
				{
					globalParseNameIgnoreCase.Add(enumerationType, enumOptions.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));
				}

				parseNameDict = globalParseNameIgnoreCase[enumerationType];
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
	}
}
