namespace Fluxera.Enumeration.UnitTests
{
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationImplicitValueConversionTests
	{
		[Test]
		public void ReturnsValueOfGivenEnum()
		{
			TestEnum enumeration = TestEnum.One;

			int result = enumeration;

			result.Should().Be(enumeration.Value);
		}
	}
}
