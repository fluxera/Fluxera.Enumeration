namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using Enums;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationWhenThenTests
	{
		[Test]
		public void ShouldExecuteDefaultActionWhenNoConditionMet()
		{
			Color enumeration = Color.Blue;

			bool firstActionExecuted = false;
			bool secondActionExecuted = false;
			bool defaultActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => firstActionExecuted = true)
				.When(Color.Green).Then(() => secondActionExecuted = true)
				.Default(() => defaultActionExecuted = true);

			firstActionExecuted.Should().BeFalse();
			secondActionExecuted.Should().BeFalse();
			defaultActionExecuted.Should().BeTrue();
		}

		[Test]
		public void ShouldExecuteDefaultActionWhenNoConditionMet_ParameterAction()
		{
			Color enumeration = Color.Blue;

			bool whenActionExecuted = false;
			bool defaultActionExecuted = false;

			enumeration
				.When(Color.Red).Then(e => whenActionExecuted = true)
				.Default(e => defaultActionExecuted = true);

			whenActionExecuted.Should().BeFalse();
			defaultActionExecuted.Should().BeTrue();
		}

		[Test]
		public void ShouldExecutedFirstActionWhenFirstConditionMet()
		{
			Color enumeration = Color.Red;

			bool firstActionExecuted = false;
			bool secondActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => firstActionExecuted = true)
				.When(Color.Green).Then(() => secondActionExecuted = true);

			firstActionExecuted.Should().BeTrue();
			secondActionExecuted.Should().BeFalse();
		}

		[Test]
		public void ShouldExecutedSecondActionWhenSecondConditionMet()
		{
			Color enumeration = Color.Green;

			bool firstActionExecuted = false;
			bool secondActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => firstActionExecuted = true)
				.When(Color.Green).Then(() => secondActionExecuted = true);

			firstActionExecuted.Should().BeFalse();
			secondActionExecuted.Should().BeTrue();
		}

		[Test]
		public void ShouldExecuteFirstActionWhenConditionMet_ParameterAction()
		{
			Color enumeration = Color.Red;

			bool whenActionExecuted = false;
			bool defaultActionExecuted = false;

			enumeration
				.When(Color.Red).Then(e => whenActionExecuted = true)
				.Default(e => defaultActionExecuted = true);

			whenActionExecuted.Should().BeTrue();
			defaultActionExecuted.Should().BeFalse();
		}

		[Test]
		public void ShouldExecuteLastActionWhenMatchesLastList()
		{
			Color enumeration = Color.Blue;

			bool firstActionExecuted = false;
			bool secondActionExecuted = false;
			bool thirdActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => firstActionExecuted = true)
				.When(Color.Green).Then(() => secondActionExecuted = true)
				.WhenAny(new List<Color> { Color.Red, Color.Green, Color.Blue }).Then(() => thirdActionExecuted = true);

			firstActionExecuted.Should().BeFalse();
			secondActionExecuted.Should().BeFalse();
			thirdActionExecuted.Should().BeTrue();
		}

		[Test]
		public void ShouldExecuteLastActionWhenMatchesLastParams()
		{
			Color enumeration = Color.Blue;

			bool firstActionExecuted = false;
			bool secondActionExecuted = false;
			bool thirdActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => firstActionExecuted = true)
				.When(Color.Green).Then(() => secondActionExecuted = true)
				.WhenAny(Color.Red, Color.Green, Color.Blue).Then(() => thirdActionExecuted = true);

			firstActionExecuted.Should().BeFalse();
			secondActionExecuted.Should().BeFalse();
			thirdActionExecuted.Should().BeTrue();
		}

		[Test]
		public void ShouldNotExecuteDefaultActionWhenConditionMet()
		{
			Color enumeration = Color.Red;

			bool whenActionExecuted = false;
			bool defaultActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => whenActionExecuted = true)
				.Default(() => defaultActionExecuted = true);

			whenActionExecuted.Should().BeTrue();
			defaultActionExecuted.Should().BeFalse();
		}

		[Test]
		public void ShouldNotExecuteSubsequentActionsWhenFirstConditionMet()
		{
			Color enumeration = Color.Red;

			bool firstActionExecuted = false;
			bool secondActionExecuted = false;

			enumeration
				.When(Color.Red).Then(() => firstActionExecuted = true)
				.When(Color.Green).Then(() => secondActionExecuted = true);

			firstActionExecuted.Should().BeTrue();
			secondActionExecuted.Should().BeFalse();
		}
	}
}
