namespace Fluxera.Enumeration.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationExplicitConversionTests
	{
		[Test]
		public void ReturnsEnumFromGivenNullableValue()
		{
			int? value = 1;

			TestEnum result = (TestEnum)value;

			result.Should().BeSameAs(TestEnum.One);
		}

		[Test]
		public void ReturnsEnumFromGivenNullableValueAsNull()
		{
			int? value = null;

			TestEnum? result = (TestEnum)value;

			result.Should().BeNull();
		}

		[Test]
		public void ReturnsEnumFromGivenValue()
		{
			int value = 1;

			TestEnum result = (TestEnum)value;

			result.Should().BeSameAs(TestEnum.One);
		}
	}
}
