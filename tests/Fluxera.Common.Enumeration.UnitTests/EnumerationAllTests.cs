namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationAllTests
	{
		[Test]
		public void ReturnsAllBaseAndDerivedEnumerations()
		{
			IReadOnlyCollection<TestBaseEnumWithDerivedValues> result = TestBaseEnumWithDerivedValues.All;

			result.Should().BeEquivalentTo(new TestBaseEnumWithDerivedValues[]
			{
				DerivedTestEnumWithValues1.A,
				DerivedTestEnumWithValues1.B,
				DerivedTestEnumWithValues2.C,
				DerivedTestEnumWithValues2.D
			});
		}

		[Test]
		public void ReturnsAllDefinedEnumerations()
		{
			IReadOnlyCollection<TestEnum> result = TestEnum.All;

			result.Should().BeEquivalentTo(new[]
			{
				TestEnum.One,
				TestEnum.Two,
				TestEnum.Three,
			});
		}
	}
}
