namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;
	using NUnit.Framework;

	[TestFixture]
	public class SupportedValueTypeTests
	{
		private static IEnumerable<object[]> TestData = new List<object[]>
		{
			new object[] { typeof(ByteEnum), 1, ByteEnum.One },
			new object[] { typeof(ShortEnum), 1, ShortEnum.One },
			new object[] { typeof(IntEnum), 1, IntEnum.One },
			new object[] { typeof(LongEnum), 1, LongEnum.One },
			new object[] { typeof(DecimalEnum), 1, DecimalEnum.One },
			new object[] { typeof(FloatEnum), 1, FloatEnum.One },
			new object[] { typeof(DoubleEnum), 1, DoubleEnum.One },
			new object[] { typeof(StringEnum), "1", StringEnum.One },
			new object[] { typeof(GuidEnum), Guid.Empty, GuidEnum.One },
		};

		[Test]
		public void ShouldParse_Generic()
		{
			ByteEnum.ParseValue(1).Should().BeSameAs(ByteEnum.One);

			ShortEnum.ParseValue(1).Should().BeSameAs(ShortEnum.One);
			IntEnum.ParseValue(1).Should().BeSameAs(IntEnum.One);
			LongEnum.ParseValue(1).Should().BeSameAs(LongEnum.One);
			DecimalEnum.ParseValue(1).Should().BeSameAs(DecimalEnum.One);
			FloatEnum.ParseValue(1).Should().BeSameAs(FloatEnum.One);
			DoubleEnum.ParseValue(1).Should().BeSameAs(DoubleEnum.One);
			StringEnum.ParseValue("1").Should().BeSameAs(StringEnum.One);
			GuidEnum.ParseValue(Guid.Empty).Should().BeSameAs(GuidEnum.One);
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldParse_NonGeneric(Type enumType, object enumValue, IEnumeration expected)
		{
			IEnumeration result = Enumeration.ParseValue(enumType, enumValue);
			result.Should().BeSameAs(expected);
		}

		[Test]
		public void ShouldThrowForUnsupportedValueType_Generic()
		{
			Action action = () => UnsupportedEnum.ParseValue(false);
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldThrowForUnsupportedValueType_NonGeneric()
		{
			Action action = () => Enumeration.ParseValue(typeof(UnsupportedEnum), false);
			action.Should().Throw<ArgumentException>();
		}
	}
}
