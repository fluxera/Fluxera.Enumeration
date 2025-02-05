namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class ParseValueTests
	{
		[Test]
		public void ShouldReturnEnumForDerivedClass_Generic()
		{
			Animal result = Animal.ParseValue(2);
			result.Should().NotBeNull().And.BeSameAs(Reptile.Iguana);
		}

		[Test]
		public void ShouldReturnEnumForDerivedClass_NonGeneric()
		{
			IEnumeration result = Enumeration.ParseValue(typeof(Animal), 2);
			result.Should().NotBeNull().And.BeSameAs(Reptile.Iguana);
		}

		[Test]
		public void ShouldReturnReturnDefaultEnumForInvalidValue_Generic()
		{
			Color defaultEnum = Color.Blue;
			Color result = Color.ParseValue(-1, defaultEnum);
			result.Should().BeSameAs(defaultEnum);
		}

		[Test]
		public void ShouldReturnReturnDefaultEnumForInvalidValue_NonGeneric()
		{
			Color defaultEnum = Color.Blue;
			IEnumeration result = Enumeration.ParseValue(typeof(Color), -1, defaultEnum);
			result.Should().BeSameAs(defaultEnum);
		}

		[Test]
		public void ShouldReturnReturnEnumForValidValue_Generic()
		{
			Color result = Color.ParseValue(1);
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldReturnReturnEnumForValidValue_NonGeneric()
		{
			IEnumeration result = Enumeration.ParseValue(typeof(Color), 1);
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldThrowForInvalidValue_Generic()
		{
			Action action = () => Color.ParseValue(-1);
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldThrowForInvalidValue_NonGeneric()
		{
			Action action = () => Enumeration.ParseValue(typeof(Color), -1);
			action.Should().Throw<ArgumentException>();
		}
	}
}
