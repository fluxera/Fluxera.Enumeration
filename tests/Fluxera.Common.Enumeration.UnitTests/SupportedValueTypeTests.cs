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
		[Test]
		public void ShouldParse_Generic()
		{
			IEnumeration result = ByteEnum.ParseValue(1);
			result.Should().BeSameAs(ByteEnum.One);
		}

		[Test]
		[TestCaseSource(nameof(TestData))]
		public void ShouldParse_NonGeneric(Type enumType, object enumValue, IEnumeration expected)
		{
			IEnumeration result = Enumeration.ParseValue(enumType, enumValue);
			result.Should().BeSameAs(expected);
		}

		private static IEnumerable<object[]> TestData = new List<object[]>
		{
			new object[] { typeof(ByteEnum), 1, ByteEnum.One },
		};
	}
}
