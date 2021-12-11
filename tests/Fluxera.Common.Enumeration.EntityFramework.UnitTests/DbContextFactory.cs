namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System.Linq;

	public static class DbContextFactory
	{
		public static TestDbContext Generate(int seedCount, bool useValueConverter)
		{
			PersonFactory.Initialize();

			TestDbContext context = new TestDbContext(useValueConverter)
			{
				SeedData = PersonFactory.Generate(seedCount).ToArray(),
			};

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
