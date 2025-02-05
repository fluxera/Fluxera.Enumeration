namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using System;
	using System.IO;
	using FluentAssertions;
	using Fluxera.Enumeration.LiteDB.UnitTests.Model;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class IntegrationTests
	{
		static IntegrationTests()
		{
			BsonMapper.Global.UseEnumeration();
		}

		[Test]
		public void ShouldWriteAndReadEnumeration()
		{
			// Open database (or create if doesn't exist)
			string databaseFile = Path.GetTempFileName();
			Console.WriteLine(databaseFile);
			using(LiteDatabase db = new LiteDatabase(databaseFile))
			{
				// Get a collection (or create, if doesn't exist)
				ILiteCollection<Person> collection = db.GetCollection<Person>("people");

				// Create your new person instance
				Person person = new Person
				{
					Gender = Gender.Male
				};

				// Insert new person document (Id will be auto-incremented)
				collection.Insert(person);

				// Update a document inside a collection
				person.Name = "John Doe";
				collection.Update(person);

				// Index document using document Name property
				collection.EnsureIndex(x => x.Name);

				// Index document using document Gender property
				collection.EnsureIndex(x => x.Gender);

				// Query document
				Person result1 = collection.FindOne(x => x.Name.StartsWith("J"));
				result1.Should().NotBeNull();
				result1.Name.Should().Be("John Doe");
				result1.Gender.Should().Be(Gender.Male);

				// Query document
				Person result2 = collection.FindOne(x => x.Gender == Gender.Male);
				result2.Should().NotBeNull();
				result2.Name.Should().Be("John Doe");
				result2.Gender.Should().Be(Gender.Male);
			}

			File.Delete(databaseFile);
		}
	}
}
