namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationWhenThenTests
	{
		public static IEnumerable<TestEnum> NameData =>
			new List<TestEnum>
			{
				TestEnum.One,
				TestEnum.Two,
				TestEnum.Three,
			};

		[Test]
		public void DefaultConditionDoesNotRunWhenConditionMet()
		{
			TestEnum one = TestEnum.One;

			bool firstActionRun = false;
			bool defaultActionRun = false;

			one
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.Default(() => defaultActionRun = true);

			firstActionRun.Should().BeTrue();
			defaultActionRun.Should().BeFalse();
		}

		[Test]
		public void DefaultConditionRunsWhenNoConditionMet()
		{
			TestEnum three = TestEnum.Three;

			bool firstActionRun = false;
			bool secondActionRun = false;
			bool defaultActionRun = false;

			three
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.When(TestEnum.Two).Then(() => secondActionRun = true)
				.Default(() => defaultActionRun = true);

			firstActionRun.Should().BeFalse();
			secondActionRun.Should().BeFalse();
			defaultActionRun.Should().BeTrue();
		}

		[Test]
		public void WhenFirstConditionMetFirstActionRuns()
		{
			TestEnum one = TestEnum.One;

			bool firstActionRun = false;
			bool secondActionRun = false;

			one
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.When(TestEnum.Two).Then(() => secondActionRun = true);

			firstActionRun.Should().BeTrue();
			secondActionRun.Should().BeFalse();
		}

		[Test]
		public void WhenFirstConditionMetSubsequentActionsNotRun()
		{
			TestEnum one = TestEnum.One;

			bool firstActionRun = false;
			bool secondActionRun = false;

			one
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.When(TestEnum.One).Then(() => secondActionRun = true);

			firstActionRun.Should().BeTrue();
			secondActionRun.Should().BeFalse();
		}

		[Test]
		public void WhenMatchesLastListActionRuns()
		{
			TestEnum three = TestEnum.Three;

			bool firstActionRun = false;
			bool secondActionRun = false;
			bool thirdActionRun = false;

			three
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.When(TestEnum.Two).Then(() => secondActionRun = true)
				.WhenAny(new List<TestEnum> { TestEnum.One, TestEnum.Two, TestEnum.Three }).Then(() => thirdActionRun = true);

			firstActionRun.Should().BeFalse();
			secondActionRun.Should().BeFalse();
			thirdActionRun.Should().BeTrue();
		}

		[Test]
		public void WhenMatchesLastParameterActionRuns()
		{
			TestEnum three = TestEnum.Three;

			bool firstActionRun = false;
			bool secondActionRun = false;
			bool thirdActionRun = false;

			three
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.When(TestEnum.Two).Then(() => secondActionRun = true)
				.WhenAny(TestEnum.One, TestEnum.Two, TestEnum.Three).Then(() => thirdActionRun = true);

			firstActionRun.Should().BeFalse();
			secondActionRun.Should().BeFalse();
			thirdActionRun.Should().BeTrue();
		}

		[Test]
		public void WhenSecondConditionMetSecondActionRuns()
		{
			TestEnum two = TestEnum.Two;

			bool firstActionRun = false;
			bool secondActionRun = false;

			two
				.When(TestEnum.One).Then(() => firstActionRun = true)
				.When(TestEnum.Two).Then(() => secondActionRun = true);

			firstActionRun.Should().BeFalse();
			secondActionRun.Should().BeTrue();
		}
	}
}
