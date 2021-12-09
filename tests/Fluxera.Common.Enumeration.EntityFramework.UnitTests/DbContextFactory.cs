namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System.Linq;

	public static class DbContextFactory
	{
		public static TestDbContext Generate(int seedCount)
		{
			PersonFactory.Initialize();

			TestDbContext context = new TestDbContext(false)
			{
				SeedData = PersonFactory.Generate(seedCount).ToArray(),
			};

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
