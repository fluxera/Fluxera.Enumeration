namespace Fluxera.Enumeration.MongoDB.UnitTests
{
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Enumeration.MongoDB.UnitTests.Model;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Linq;
	using NUnit.Framework;

	public class QueryByNameTests
	{
		private IMongoCollection<PersonByName> collection;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			ConventionPack pack = [];
			pack.UseEnumeration();
			ConventionRegistry.Register("ConventionPack", pack, t => t == typeof(PersonByName));

			IMongoClient client = new MongoClient(GlobalFixture.ConnectionString);
			IMongoDatabase database = client.GetDatabase(GlobalFixture.Database);
			this.collection = database.GetCollection<PersonByName>("PeopleByName");

			PersonByName person = new PersonByName
			{
				Name = "Ross Geller",
				RelationshipStatus = RelationshipStatus.Divorced
			};

			await collection.InsertOneAsync(person);
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
