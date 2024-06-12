namespace Fluxera.Enumeration.EntityFrameworkCore.UnitTests
{
	using Fluxera.Enumeration.EntityFrameworkCore.UnitTests.Model;
	using Microsoft.EntityFrameworkCore;
	using System;

	public class ByNameContext : DbContext
	{
		public DbSet<PersonByName> PeopleByName { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(optionsBuilder == null)
			{
				throw new ArgumentNullException(nameof(optionsBuilder));
			}

			optionsBuilder.UseInMemoryDatabase("TestByNameDatabase");

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if(modelBuilder == null)
			{
				throw new ArgumentNullException(nameof(modelBuilder));
			}

			modelBuilder.Entity<PersonByName>().UseEnumeration();

			base.OnModelCreating(modelBuilder);
		}
	}
}