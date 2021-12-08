namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using Enums;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EqualsTests
	{
		private static IEnumerable<object[]> EqualOperatorTestData => new List<object[]>
		{
			new object[] { null, null, true },
			new object[] { null, Color.Red, false },
			new object[] { Color.Red, null, false },
			new object[] { Color.Red, Color.Red, true },
			new object[] { Color.Red, Color.Blue, false },
		};

		public static IEnumerable<object[]> EquatableEqualsTestData => new List<object[]>
		{
			new object[] { Color.Red, null, false },
			new object[] { Color.Red, Color.Red, true },
			new object[] { Color.Red, Color.Blue, false },
		};

		[Test]
		[TestCaseSource(nameof(EqualOperatorTestData))]
		public void EqualOperatorShouldReturnExpectedValue(Color left, Color right, bool expected)
		{
			bool result = left == right;
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EqualOperatorTestData))]
		public void EqualOperatorShouldReturnExpectedValue_IEnumeration(IEnumeration left, IEnumeration right, bool expected)
		{
			bool result = left == right;
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EquatableEqualsTestData))]
		public void EquatableEqualsShouldReturnExpectedValueWithEnumeration(Color left, Color right, bool expected)
		{
			bool result = left.Equals(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EquatableEqualsTestData))]
		public void EquatableEqualsShouldReturnExpectedValueWithEnumeration_IEnumeration(IEnumeration left, IEnumeration right, bool expected)
		{
			bool result = left.Equals(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EqualOperatorTestData))]
		public void NotEqualOperatorShouldReturnExpectedValue(Color left, Color right, bool expected)
		{
			bool result = left != right;
			result.Should().Be(!expected);
		}

		[Test]
		[TestCaseSource(nameof(EqualOperatorTestData))]
		public void NotEqualOperatorShouldReturnExpectedValue_IEnumeration(IEnumeration left, IEnumeration right, bool expected)
		{
			bool result = left != right;
			result.Should().Be(!expected);
		}

		[Test]
		[TestCaseSource(nameof(EquatableEqualsTestData))]
		public void ObjectEqualsShouldReturnExpectedValueWithObject(Color left, object right, bool expected)
		{
			bool result = left.Equals(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EquatableEqualsTestData))]
		public void ObjectEqualsShouldReturnExpectedValueWithObject_IEnumeration(IEnumeration left, object right, bool expected)
		{
			bool result = left.Equals(right);
			result.Should().Be(expected);
		}
	}
}
