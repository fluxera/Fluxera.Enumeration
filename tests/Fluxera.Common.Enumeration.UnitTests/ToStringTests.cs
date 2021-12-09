namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using Enums;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class ToStringTests
	{
		private static IEnumerable<Color> TestData => Color.All;

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldReturnTheEnumName(Color color)
		{
			string result = color.ToString();
			result.Should().Be(color.Name);
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldReturnTheEnumName_IEnumeration(IEnumeration color)
		{
			string? result = color.ToString();
			result.Should().Be(color.Name);
		}
	}
}
