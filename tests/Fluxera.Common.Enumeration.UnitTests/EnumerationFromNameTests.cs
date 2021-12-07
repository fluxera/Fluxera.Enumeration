namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using System.ComponentModel;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationFromNameTests
	{
		[Test]
		public void ReturnsEnumGivenDerivedClass()
		{
			TestBaseEnum result = TestDerivedEnum.FromName("One");

			result.Should().NotBeNull().And.BeSameAs(TestBaseEnum.One);
		}

		[Test]
		public void ReturnsEnumGivenExplicitPriorUse()
		{
			string expected = TestEnum.One.Name;

			TestEnum result = TestEnum.FromName(expected);

			result.Name.Should().Be(expected);
		}

		[Test]
		public void ReturnsEnumGivenMatchingName()
		{
			TestEnum result = TestEnum.FromName("One");

			result.Should().BeSameAs(TestEnum.One);
		}

		[Test]
		public void ReturnsEnumGivenNoExplicitPriorUse()
		{
			string expected = "One";

			TestEnum result = TestEnum.FromName(expected);

			result.Name.Should().Be(expected);
		}

		[Test]
		public void ThrowsGivenEmptyString()
		{
			Action action = () => TestEnum.FromName(string.Empty);

			action.Should()
				.ThrowExactly<ArgumentException>()
				.Which.ParamName.Should().Be("name");
		}

		[Test]
		public void ThrowsGivenNonMatchingString()
		{
			string name = "Doesn't Exist";

			Action action = () => TestEnum.FromName(name);

			action.Should()
				.ThrowExactly<InvalidEnumArgumentException>();
		}

		[Test]
		public void ThrowsGivenNull()
		{
			Action action = () => TestEnum.FromName(null);

			action.Should()
				.ThrowExactly<ArgumentNullException>()
				.Which.ParamName.Should().Be("name");
		}
	}
}
