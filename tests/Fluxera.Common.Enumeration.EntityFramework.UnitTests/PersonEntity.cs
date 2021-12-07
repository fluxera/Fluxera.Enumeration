namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System.ComponentModel.DataAnnotations;

	public class PersonEntity
	{
		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public Status /*?*/ Status { get; set; }
	}
}
