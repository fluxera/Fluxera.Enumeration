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

	public class QueryByValueTests
	{
		private LiteDatabaseAsync database;
		private ILiteCollectionAsync<PersonByValue> collection;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			BsonMapper.Global.Entity<PersonByValue>().Id(x => x.Id);
			BsonMapper.Global.UseEnumeration(useValue: true);

			this.database = new LiteDatabaseAsync($"{Guid.NewGuid():N}.db");
			this.collection = this.database.GetCollection<PersonByValue>();

			PersonByValue person = new PersonByValue
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
		public async Task ShouldFindByValue()
		{
			PersonByValue linqFilterResult = await this.collection
				.AsQueryable()
				.Where(x => x.RelationshipStatus == 2)
				.FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}

		[Test]
		public async Task ShouldFindByEnum()
		{
			PersonByValue linqFilterResult = await this.collection
			   .AsQueryable()
			   .Where(x => x.RelationshipStatus == RelationshipStatus.Divorced)
			   .FirstOrDefaultAsync();
			linqFilterResult.Should().NotBeNull();
		}
	}
}
