namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;
	using NUnit.Framework;

	[TestFixture]
	public class TypeConverterTests
	{
		public static IEnumerable<object[]> TestData()
		{
			yield return [typeof(ByteEnum), ByteEnum.One.Name, ByteEnum.One.Value, ByteEnum.One];
			yield return [typeof(IntEnum), IntEnum.One.Name, IntEnum.One.Value, IntEnum.One];
			yield return [typeof(LongEnum), LongEnum.One.Name, LongEnum.One.Value, LongEnum.One];
			yield return [typeof(ShortEnum), ShortEnum.One.Name, ShortEnum.One.Value, ShortEnum.One];
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldConvertFromString(Type enumType, string name, object value, object expectedEnum)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(enumType);

			object result = converter.ConvertFromString(name);
			result.Should().BeOfType(enumType);
			result.Should().NotBeNull().And.BeSameAs(expectedEnum);
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldConvertFromValue(Type enumType, string name, object value, object expectedEnum)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(enumType);

			object result = converter.ConvertFrom(value);
			result.Should().BeOfType(enumType);
			result.Should().NotBeNull().And.BeSameAs(expectedEnum);
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldConvertToString(Type enumType, string expectedName, object value, object inputEnum)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(enumType);

			string name = converter.ConvertToString(inputEnum);
			name.Should().NotBeNullOrWhiteSpace().And.Be(expectedName);
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldConvertToValue(Type enumType, string name, object expectedValue, object inputEnum)
		{
			TypeConverter converter = TypeDescriptor.GetConverter(enumType);

			object value = converter.ConvertTo(inputEnum, expectedValue.GetType());
			value.Should().NotBeNull().And.Be(expectedValue);
		}
	}
}
