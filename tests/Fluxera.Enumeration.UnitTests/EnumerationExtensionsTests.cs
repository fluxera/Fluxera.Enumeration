namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationExtensionsTests
	{
		public static IEnumerable<object[]> IsEnumerationTestData => new List<object[]>
		{
			new object[] { typeof(int), false },
			new object[] { typeof(Animal), false },
			new object[] { typeof(Reptile), true },
			new object[] { typeof(Mammal), true },
			new object[] { typeof(Color), true },
			new object[] { typeof(MessageType), true },
		};

		[Test]
		[TestCaseSource(nameof(IsEnumerationTestData))]
		public void IsEnumerationReturnsExpected(Type type, bool expectedResult)
		{
			bool result = type.IsEnumeration();
			result.Should().Be(expectedResult);
		}
	}
}
