namespace Fluxera.Enumeration.UnitTests
{
	using System;
	using System.ComponentModel;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class ParseNameTests
	{
		[Test]
		public void ShouldReturnEnumForDerivedClass_Generic()
		{
			Animal result = Animal.ParseName("Tiger");
			result.Should().NotBeNull().And.BeSameAs(Mammal.Tiger);
		}

		[Test]
		public void ShouldReturnEnumForDerivedClass_NonGeneric()
		{
			Animal result = Animal.ParseName("Tiger");
			result.Should().NotBeNull().And.BeSameAs(Mammal.Tiger);
		}

		[Test]
		public void ShouldReturnEnumForNameFromEnumInstance_Generic()
		{
			string expected = Color.Green.Name;
			Color result = Color.ParseName(expected);
			result.Name.Should().Be(expected);
		}

		[Test]
		public void ShouldReturnEnumForNameFromEnumInstance_NonGeneric()
		{
			string expected = Color.Green.Name;
			IEnumeration result = Enumeration.ParseName(typeof(Color), expected);
			result.Name.Should().Be(expected);
		}

		[Test]
		public void ShouldReturnEnumForValidName_Generic()
		{
			Color result = Color.ParseName("Green");
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldReturnEnumForValidName_NonGeneric()
		{
			IEnumeration result = Enumeration.ParseName(typeof(Color), "Green");
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldReturnEnumNameForEnumInstance_Generic()
		{
			string expected = "Green";
			Color result = Color.ParseName(expected);
			result.Name.Should().Be(expected);
		}

		[Test]
		public void ShouldReturnEnumNameForEnumInstance_NonGeneric()
		{
			string expected = "Green";
			IEnumeration result = Enumeration.ParseName(typeof(Color), expected);
			result.Name.Should().Be(expected);
		}

		[Test]
		public void ShouldThrowForEmptyInvalidName_Generic()
		{
			Action action = () => Color.ParseName("Purple");
			action.Should().Throw<InvalidEnumArgumentException>();
		}

		[Test]
		public void ShouldThrowForEmptyInvalidName_NonGeneric()
		{
			Action action = () => Enumeration.ParseName(typeof(Color), "Purple");
			action.Should().Throw<InvalidEnumArgumentException>();
		}

		[Test]
		public void ShouldThrowForEmptyStringName_Generic()
		{
			Action action = () => Color.ParseName(string.Empty);
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldThrowForEmptyStringName_NonGeneric()
		{
			Action action = () => Enumeration.ParseName(typeof(Color), string.Empty);
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void ShouldThrowForNullStringName_Generic()
		{
			Action action = () => Color.ParseName(null);
			action.Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void ShouldThrowForNullStringName_NonGeneric()
		{
			Action action = () => Enumeration.ParseName(typeof(Color), null);
			action.Should().Throw<ArgumentNullException>();
		}
	}
}
