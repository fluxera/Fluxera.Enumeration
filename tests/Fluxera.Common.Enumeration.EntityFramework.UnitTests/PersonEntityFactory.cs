namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Bogus;

	public static class PersonEntityFactory
	{
		public static IList<PersonEntity> Generate(int count)
		{
			return new Faker<PersonEntity>()
				.RuleFor(e => e.Name, (f, e) => f.Person.FullName)
				.RuleFor(e => e.Id, (f, e) => f.Random.Guid().ToString())
				.RuleFor(e => e.Status, (f, e) => f.PickRandomParam(GetAll()))
				.Generate(count);
		}

		public static void Initialize()
		{
			Randomizer.Seed = new Random(62392);
		}

		private static Status[] GetAll()
		{
			List<Status> statusList = Status.All.ToList();
			//statusList.Add(null!);
			return statusList.ToArray();
		}
	}
}
