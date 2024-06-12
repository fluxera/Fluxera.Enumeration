namespace Fluxera.Enumeration.EntityFrameworkCore.UnitTests
{
	using System;
	using Fluxera.Enumeration.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;

	public class ByValueContext : DbContext
	{
		public DbSet<PersonByValue> PeopleByValue { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(optionsBuilder == null)
			{
				throw new ArgumentNullException(nameof(optionsBuilder));
			}

			optionsBuilder.UseInMemoryDatabase("TestByValueDatabase");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if(modelBuilder == null)
			{
				throw new ArgumentNullException(nameof(modelBuilder));
			}

			modelBuilder.Entity<PersonByValue>().UseEnumeration(useValue: true);

			base.OnModelCreating(modelBuilder);
		}
	}
}