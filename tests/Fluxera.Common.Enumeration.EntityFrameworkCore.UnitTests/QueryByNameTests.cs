namespace Fluxera.Enumeration.EntityFrameworkCore.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Enumeration.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using NUnit.Framework;

	public class QueryByNameTests
	{
		private ByNameContext context;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			this.context = DbContextFactory.GenerateByName();

			PersonByName person = new PersonByName
			{
				Id = Guid.NewGuid().ToString("N"),
				Name = "Ross Geller",
				RelationshipStatus = RelationshipStatus.Divorced
			};

			await this.context.AddAsync(person);
			await this.context.SaveChangesAsync();
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			this.context?.Dispose();
		}

		[Ignore("Fix this later")]
		[Test]
		public async Task ShouldFindByName()
		{
			PersonByName linqFilterResult = await this.context
				.Set<PersonByName>()
				.Where(x => x.RelationshipStatus == "Divorced")
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByEnum()
		{
			PersonByName linqFilterResult = await this.context
				.Set<PersonByName>()
				.Where(x => x.RelationshipStatus == RelationshipStatus.Divorced)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
