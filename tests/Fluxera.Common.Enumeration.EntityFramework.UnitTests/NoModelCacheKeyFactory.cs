namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Infrastructure;

	/// <summary>
	///     Creates a unique cache key every time to ensure no caching happens.
	/// </summary>
	public class NoModelCacheKeyFactory : IModelCacheKeyFactory
	{
		public object Create(DbContext context)
		{
			return Guid.NewGuid();
		}
	}
}
