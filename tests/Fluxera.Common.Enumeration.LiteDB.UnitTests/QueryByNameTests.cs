namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Enumeration.LiteDB.UnitTests.Model;
	using global::LiteDB;
	using global::LiteDB.Async;
	using global::LiteDB.Queryable;
	using NUnit.Framework;

	public class QueryByNameTests
	{
		private LiteDatabaseAsync database;
		private ILiteCollectionAsync<PersonByName> collection;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			BsonMapper.Global.Entity<PersonByName>().Id(x => x.Id);
			BsonMapper.Global.UseEnumeration();

			this.database = new LiteDatabaseAsync($"{Guid.NewGuid():N}.db");
			this.collection = this.database.GetCollection<PersonByName>();

			PersonByName person = new PersonByName
			{
				Name = "Ross Geller",
				RelationshipStatus = RelationshipStatus.Divorced
			};

			await collection.InsertAsync(person);
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			this.database?.Dispose();
		}

		[Test]
		public async Task ShouldFindByName()
		{
			PersonByName linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.RelationshipStatus == "Divorced")
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByEnum()
		{
			PersonByName linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.RelationshipStatus == RelationshipStatus.Divorced)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
