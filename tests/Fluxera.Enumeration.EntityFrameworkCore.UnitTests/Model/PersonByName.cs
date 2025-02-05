namespace Fluxera.Enumeration.EntityFrameworkCore.UnitTests.Model
{
	using System.ComponentModel.DataAnnotations;

	public class PersonByName
	{
		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public RelationshipStatus RelationshipStatus { get; set; }
	}
}
