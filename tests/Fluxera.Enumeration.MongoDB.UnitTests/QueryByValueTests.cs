namespace Fluxera.Enumeration.MongoDB.UnitTests
{
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Enumeration.MongoDB.UnitTests.Model;
	using global::MongoDB.Bson.Serialization.Conventions;
	using global::MongoDB.Driver;
	using global::MongoDB.Driver.Linq;
	using NUnit.Framework;

	public class QueryByValueTests
	{
		private IMongoCollection<PersonByValue> collection;

		[OneTimeSetUp]
		public async Task SetUp()
		{
			ConventionPack pack = [];
			pack.UseEnumeration(useValue: true);
			ConventionRegistry.Register("ConventionPack", pack, t => t == typeof(PersonByValue));

			IMongoClient client = new MongoClient(GlobalFixture.ConnectionString);
			IMongoDatabase database = client.GetDatabase(GlobalFixture.Database);
			this.collection = database.GetCollection<PersonByValue>("PeopleByValue");

			PersonByValue person = new PersonByValue
			{
				Name = "Ross Geller",
				RelationshipStatus = RelationshipStatus.Divorced
			};

			await collection.InsertOneAsync(person);
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
