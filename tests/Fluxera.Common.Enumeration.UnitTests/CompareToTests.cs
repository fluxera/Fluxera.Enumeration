namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using Enums;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class CompareToTests
	{
		private static IEnumerable<object[]> CompareToTestData => new List<object[]>
		{
			new object[] { Color.Green, Color.Red, 1 },
			new object[] { Color.Green, Color.Green, 0 },
			new object[] { Color.Green, Color.Blue, -1 },
			new object[] { Color.Green, null, 1 },
		};

		private static IEnumerable<object[]> ComparisonOperatorsTestData => new List<object[]>
		{
			new object[] { Color.Green, Color.Red, false, false, true },
			new object[] { Color.Green, Color.Green, false, true, false },
			new object[] { Color.Green, Color.Blue, true, false, false },
			new object[] { Color.Green, null, false, false, true },
		};

		[Test]
		[TestCaseSource(nameof(CompareToTestData))]
		public void CompareToShouldReturnExpectedValueWithEnumeration(Color left, Color right, int expected)
		{
			int result = left.CompareTo(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(CompareToTestData))]
		public void CompareToShouldReturnExpectedValueWithEnumeration_IEnumeration(IEnumeration left, IEnumeration right, int expected)
		{
			int result = left.CompareTo(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(CompareToTestData))]
		public void CompareToShouldReturnExpectedValueWithObject(Color left, object right, int expected)
		{
			int result = left.CompareTo(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(CompareToTestData))]
		public void CompareToShouldReturnExpectedValueWithObject_IEnumeration(IEnumeration left, object right, int expected)
		{
			int result = left.CompareTo(right);
			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void EqualToShouldReturnExpectedValue(Color left, Color right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left == right;
			result.Should().Be(equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void EqualToShouldReturnExpectedValue_IEnumeration(IEnumeration left, IEnumeration right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left == right;
			result.Should().Be(equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void GreaterThanOrEqualShouldReturnExpectedValue(Color left, Color right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left >= right;
			result.Should().Be(greaterThan || equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void GreaterThanShouldReturnExpectedValue(Color left, Color right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left > right;
			result.Should().Be(greaterThan);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void LessThanOrEqualShouldReturnExpectedValue(Color left, Color right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left <= right;
			result.Should().Be(lessThan || equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void LessThanShouldReturnExpectedValue(Color left, Color right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left < right;
			result.Should().Be(lessThan);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void NotEqualToShouldReturnExpectedValue(Color left, Color right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left != right;
			result.Should().Be(!equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsTestData))]
		public void NotEqualToShouldReturnExpectedValue_IEnumeration(IEnumeration left, IEnumeration right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left != right;
			result.Should().Be(!equalTo);
		}
	}
}
