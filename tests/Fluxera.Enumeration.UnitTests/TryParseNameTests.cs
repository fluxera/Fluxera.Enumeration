namespace Fluxera.Enumeration.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class TryParseNameTests
	{
		[Test]
		public void ShouldOutputEnumForValidName_Generic()
		{
			Color.TryParseName("Green", out Color result);
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldOutputEnumForValidName_NonGeneric()
		{
			Enumeration.TryParseName(typeof(Color), "Green", out IEnumeration result);
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldOutputEnumForValidNameIgnoreCase_Generic()
		{
			Color.TryParseName("gREEn", true, out Color result);
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldOutputEnumForValidNameIgnoreCase_NonGeneric()
		{
			Enumeration.TryParseName(typeof(Color), "gREEn", true, out IEnumeration result);
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldOutputNullForEmptyStringName_Generic()
		{
			Color.TryParseName(string.Empty, out Color result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldOutputNullForEmptyStringName_NonGeneric()
		{
			Enumeration.TryParseName(typeof(Color), string.Empty, out IEnumeration result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldOutputNullForNullStringName_Generic()
		{
			Color.TryParseName(null, out Color result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldOutputNullForNullStringName_NonGeneric()
		{
			Enumeration.TryParseName(typeof(Color), null, out IEnumeration result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldOutputNullForWrongCaseName_Generic()
		{
			Color.TryParseName("gREEn", out Color result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldOutputNullForWrongCaseName_NonGeneric()
		{
			Enumeration.TryParseName(typeof(Color), "gREEn", out IEnumeration result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldReturnFalseForEmptyStringName_Generic()
		{
			bool result = Color.TryParseName(string.Empty, out Color _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnFalseForEmptyStringName_NonGeneric()
		{
			bool result = Enumeration.TryParseName(typeof(Color), string.Empty, out IEnumeration _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnFalseForNullStringName_Generic()
		{
			bool result = Color.TryParseName(null, out Color _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnFalseForNullStringName_NonGeneric()
		{
			bool result = Enumeration.TryParseName(typeof(Color), null, out IEnumeration _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnFalseForWringCaseName_Generic()
		{
			bool result = Color.TryParseName("gREEn", out Color _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnFalseForWringCaseName_NonGeneric()
		{
			bool result = Enumeration.TryParseName(typeof(Color), "gREEn", out IEnumeration _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnTrueForValidName_Generic()
		{
			bool result = Color.TryParseName("Green", out Color _);
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValidName_NonGeneric()
		{
			bool result = Enumeration.TryParseName(typeof(Color), "Green", out IEnumeration _);
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValidNameIgnoreCase_Generic()
		{
			bool result = Color.TryParseName("gREEn", true, out Color _);
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValidNameIgnoreCase_NonGeneric()
		{
			bool result = Enumeration.TryParseName(typeof(Color), "gREEn", true, out IEnumeration _);
			result.Should().BeTrue();
		}
	}
}
