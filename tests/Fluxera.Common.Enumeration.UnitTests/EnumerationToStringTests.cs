namespace Fluxera.Enumeration.UnitTests
{
	using System.Collections.Generic;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationToStringTests
	{
		public static IEnumerable<TestEnum> NameData =>
			new List<TestEnum>
			{
				TestEnum.One,
				TestEnum.Two,
				TestEnum.Three,
			};

		[Test]
		[TestCaseSource(nameof(NameData))]
		public void ReturnsFormattedNameAndValue(TestEnum enumeration)
		{
			string result = enumeration.ToString();

			result.Should().Be(enumeration.Name);
		}
	}
}
