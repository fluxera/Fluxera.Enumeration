namespace Fluxera.Enumeration.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationTryFromNameTests
	{
		[Test]
		public void ProducesEnumGivenMatchingName()
		{
			TestEnum.TryFromName("One", out TestEnum? result);

			result.Should().BeSameAs(TestEnum.One);
		}

		[Test]
		public void ProducesNullGivenEmptyString()
		{
			TestEnum.TryFromName(string.Empty, out TestEnum? result);

			result.Should().BeNull();
		}

		[Test]
		public void ProducesNullGivenNull()
		{
			TestEnum.TryFromName(null, out TestEnum? result);

			result.Should().BeNull();
		}

		[Test]
		public void ReturnsFalseGivenEmptyString()
		{
			bool result = TestEnum.TryFromName(string.Empty, out TestEnum? _);

			result.Should().BeFalse();
		}

		[Test]
		public void ReturnsFalseGivenNull()
		{
			bool result = TestEnum.TryFromName(null, out TestEnum? _);

			result.Should().BeFalse();
		}

		[Test]
		public void ReturnsTrueGivenMatchingName()
		{
			bool result = TestEnum.TryFromName("One", out TestEnum? _);

			result.Should().BeTrue();
		}
	}
}
