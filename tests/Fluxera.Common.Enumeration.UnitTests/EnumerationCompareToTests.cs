namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationCompareToTests
	{
		public static IEnumerable<object[]> CompareToData =>
			new List<object[]>
			{
				new object[] { TestEnum.Two, TestEnum.One, 1 },
				new object[] { TestEnum.Two, TestEnum.Two, 0 },
				new object[] { TestEnum.Two, TestEnum.Three, -1 },
			};

		public static IEnumerable<object[]> ComparisonOperatorsData =>
			new List<object[]>
			{
				new object[] { TestEnum.Two, TestEnum.One, false, false, true },
				new object[] { TestEnum.Two, TestEnum.Two, false, true, false },
				new object[] { TestEnum.Two, TestEnum.Three, true, false, false },
			};

		[Test]
		[TestCaseSource(nameof(CompareToData))]
		public void CompareToReturnsExpected(TestEnum left, TestEnum right, int expected)
		{
			int result = left.CompareTo(right);

			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsData))]
		public void GreaterThanOrEqualReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left >= right;

			result.Should().Be(greaterThan || equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsData))]
		public void GreaterThanReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left > right;

			result.Should().Be(greaterThan);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsData))]
		public void LessThanOrEqualReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left <= right;

			result.Should().Be(lessThan || equalTo);
		}

		[Test]
		[TestCaseSource(nameof(ComparisonOperatorsData))]
		public void LessThanReturnsExpected(TestEnum left, TestEnum right, bool lessThan, bool equalTo, bool greaterThan)
		{
			bool result = left < right;

			result.Should().Be(lessThan);
		}
	}
}
