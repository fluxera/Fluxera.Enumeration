namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System;
	using System.Collections.Generic;
	using Bogus;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;

	public static class PersonFactory
	{
		public static IList<Person> Generate(int count)
		{
			return new Faker<Person>()
				.RuleFor(e => e.Name, (f, e) => f.Person.FullName)
				.RuleFor(e => e.Id, (f, e) => f.Random.Guid().ToString())
				.RuleFor(e => e.Status, (f, e) => f.PickRandomParam(Status.All))
				.RuleFor(e => e.ByteEnum, (f, e) => ByteEnum.One)
				.RuleFor(e => e.ShortEnum, (f, e) => ShortEnum.One)
				.RuleFor(e => e.IntEnum, (f, e) => IntEnum.One)
				.RuleFor(e => e.LongEnum, (f, e) => LongEnum.One)
				.RuleFor(e => e.FloatEnum, (f, e) => FloatEnum.One)
				.RuleFor(e => e.DoubleEnum, (f, e) => DoubleEnum.One)
				.RuleFor(e => e.DecimalEnum, (f, e) => DecimalEnum.One)
				.RuleFor(e => e.StringEnum, (f, e) => StringEnum.One)
				.RuleFor(e => e.GuidEnum, (f, e) => GuidEnum.One)
				.Generate(count);
		}

		public static void Initialize()
		{
			Randomizer.Seed = new Random(62392);
		}
	}
}
