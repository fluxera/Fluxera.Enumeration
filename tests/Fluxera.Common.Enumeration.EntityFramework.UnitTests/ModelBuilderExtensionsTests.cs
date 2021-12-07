namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class ModelBuilderExtensionsTests
	{
		[Test]
		public void ApplyTheValueConversions()
		{
			int seedCount = 1;
			using TestingContext context = DbContextFactory.Generate(seedCount);
			List<PersonEntity> people = context.Set<PersonEntity>().ToList();

			people.Should().BeEquivalentTo(context.SeedData);
		}
	}
}
