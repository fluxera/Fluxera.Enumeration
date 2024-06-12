namespace Fluxera.Enumeration.LiteDB.UnitTests.Model
{
	using global::LiteDB;

	public class Person
	{
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public Gender Gender { get; set; }
	}
}
