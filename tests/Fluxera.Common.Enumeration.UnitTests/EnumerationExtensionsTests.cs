namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationExtensionsTests
	{
		public abstract class AbstractEnum : Enumeration<AbstractEnum>
		{
			protected AbstractEnum(string name, int value) : base(name, value)
			{
			}
		}

		public static IEnumerable<object[]> IsEnumerationData =>
			new List<object[]>
			{
				new object[] { typeof(int), false, null },
				new object[] { typeof(AbstractEnum), false, null },
				new object[] { typeof(TestEnum), true, new Type[] { typeof(TestEnum), typeof(int) } },
			};

		[Test]
		[TestCaseSource(nameof(IsEnumerationData))]
		public void IsEnumerationReturnsExpected(Type type, bool expectedResult, Type[] _)
		{
			bool result = type.IsEnumeration();

			result.Should().Be(expectedResult);
		}
	}
}
