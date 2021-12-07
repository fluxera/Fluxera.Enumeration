namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationEqualsTests
	{
		public static IEnumerable<object[]> EqualsObjectData =>
			new List<object[]>
			{
				new object[] { TestEnum.One, null, false },
				new object[] { TestEnum.One, TestEnum.One, true },
				new object[] { TestEnum.One, TestEnum2.One, false },
				new object[] { TestEnum.One, TestEnum.Two, false },
			};

		public static IEnumerable<object[]> EqualsEnumerationData =>
			new List<object[]>
			{
				new object[] { TestEnum.One, null, false },
				new object[] { TestEnum.One, TestEnum.One, true },
				new object[] { TestEnum.One, TestEnum.Two, false },
			};

		public static IEnumerable<object[]> EqualOperatorData =>
			new List<object[]>
			{
				new object[] { null, null, true },
				new object[] { null, TestEnum.One, false },
				new object[] { TestEnum.One, null, false },
				new object[] { TestEnum.One, TestEnum.One, true },
				new object[] { TestEnum.One, TestEnum.Two, false },
			};

		private class TestEnum2 : Enumeration<TestEnum2>
		{
			public static TestEnum2 One = new TestEnum2(nameof(One), 1);

			protected TestEnum2(string name, int value) : base(name, value)
			{
			}
		}

		[Test]
		[TestCaseSource(nameof(EqualOperatorData))]
		public void EqualOperatorReturnsExpected(TestEnum left, TestEnum right, bool expected)
		{
			bool result = left == right;

			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EqualsEnumerationData))]
		public void EqualsEnumerationReturnsExpected(TestEnum left, object right, bool expected)
		{
			bool result = left.Equals(right);

			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EqualsObjectData))]
		public void EqualsObjectReturnsExpected(TestEnum left, object right, bool expected)
		{
			bool result = left.Equals(right);

			result.Should().Be(expected);
		}

		[Test]
		[TestCaseSource(nameof(EqualOperatorData))]
		public void NotEqualOperatorReturnsExpected(TestEnum left, TestEnum right, bool expected)
		{
			bool result = left != right;

			result.Should().Be(!expected);
		}
	}
}
