namespace Fluxera.Enumeration.MongoDB.UnitTests.Model
{
	using global::MongoDB.Bson;

	public class PersonByValue
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public RelationshipStatus RelationshipStatus { get; set; }
	}
}
