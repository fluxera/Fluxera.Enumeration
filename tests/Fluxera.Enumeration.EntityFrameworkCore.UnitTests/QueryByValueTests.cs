namespace Fluxera.Enumeration.EntityFrameworkCore.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Enumeration.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using NUnit.Framework;

	public class QueryByValueTests
	{
		private ByValueContext context;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			this.context = DbContextFactory.GenerateByValue();

			PersonByValue person = new PersonByValue
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

		[Ignore("Fix this later. Drop .NET 7 in november?")]
		[Test]
		public async Task ShouldFindByValue()
		{
			PersonByValue linqFilterResult = await this.context
				.Set<PersonByValue>()
				.Where(x => x.RelationshipStatus == 2)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByEnum()
		{
			PersonByValue linqFilterResult = await this.context
				.Set<PersonByValue>()
				.Where(x => x.RelationshipStatus == RelationshipStatus.Divorced)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
