﻿namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Enumeration.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.Extensions.Logging;

	public class TestDbContext : DbContext
	{
		private readonly bool useValueConverter;

		public TestDbContext(bool useValueConverter)
		{
			this.useValueConverter = useValueConverter;
		}

		public bool IsLoggingSensitiveData { get; set; }

		public ILoggerFactory LoggerFactory { get; set; }

		public DbSet<Person> People { get; set; }

		public IEnumerable<object> SeedData { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(optionsBuilder == null)
			{
				throw new ArgumentNullException(nameof(optionsBuilder));
			}

			optionsBuilder.UseInMemoryDatabase("TestDatabase");

			if(this.SeedData != null)
			{
				optionsBuilder.ReplaceService<IModelCacheKeyFactory, NoModelCacheKeyFactory>();
			}

			optionsBuilder.UseLoggerFactory(this.LoggerFactory);
			if(this.IsLoggingSensitiveData)
			{
				optionsBuilder.EnableSensitiveDataLogging();
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			if(modelBuilder == null)
			{
				throw new ArgumentNullException(nameof(modelBuilder));
			}

			if(this.SeedData != null)
			{
				IEnumerable<Type> types = this.SeedData.Select(x => x.GetType()).Distinct();
				foreach(Type type in types)
				{
					EntityTypeBuilder entityBuilder = modelBuilder.Entity(type);
					object[] data = this.SeedData.Where(x => x.GetType() == type).ToArray();
					entityBuilder.HasData(data);
				}
			}

			modelBuilder.UseEnumeration(this.useValueConverter);

			base.OnModelCreating(modelBuilder);
		}
	}
}
