namespace Fluxera.Enumeration.MongoDB.UnitTests.Model
{
	using global::MongoDB.Bson;

	public class PersonByName
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public RelationshipStatus RelationshipStatus { get; set; }
	}
}
