namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System.Linq;

	public static class DbContextFactory
	{
		public static TestingContext Generate(int seedCount)
		{
			PersonEntityFactory.Initialize();

			TestingContext context = new TestingContext(false)
			{
				SeedData = PersonEntityFactory.Generate(seedCount).ToArray(),
			};

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
