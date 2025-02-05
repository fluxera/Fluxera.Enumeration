namespace Fluxera.Enumeration.LiteDB.UnitTests.Model
{
	using global::LiteDB;

	public class PersonByValue
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public RelationshipStatus RelationshipStatus { get; set; }
	}
}
