namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationFromValueTests
	{
		[Test]
		public void ReturnsDefaultEnumGivenNonMatchingValue()
		{
			int value = -1;
			TestEnum defaultEnum = TestEnum.One;

			TestEnum result = TestEnum.FromValue(value, defaultEnum);

			result.Should().BeSameAs(defaultEnum);
		}

		[Test]
		public void ReturnsEnumGivenDerivedClass()
		{
			TestBaseEnum result = TestDerivedEnum.FromValue(1);

			result.Should().NotBeNull().And.BeSameAs(TestBaseEnum.One);
		}

		[Test]
		public void ReturnsEnumGivenMatchingValue()
		{
			TestEnum result = TestEnum.FromValue(1);

			result.Should().BeSameAs(TestEnum.One);
		}

		[Test]
		public void ThrowsGivenNonMatchingValue()
		{
			int value = -1;

			Action action = () => TestEnum.FromValue(value);

			action.Should()
				.ThrowExactly<ArgumentException>();
		}
	}
}
