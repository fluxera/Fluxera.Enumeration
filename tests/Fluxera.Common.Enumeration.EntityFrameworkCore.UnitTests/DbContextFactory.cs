namespace Fluxera.Enumeration.EntityFrameworkCore.UnitTests
{
	using System.Linq;
	using Fluxera.Enumeration.EntityFrameworkCore.UnitTests.Model;

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

		public static ByNameContext GenerateByName()
		{
			ByNameContext context = new ByNameContext();

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}

		public static ByValueContext GenerateByValue()
		{
			ByValueContext context = new ByValueContext();

			context.Database.EnsureDeleted();
			context.Database.EnsureCreated();
			return context;
		}
	}
}
