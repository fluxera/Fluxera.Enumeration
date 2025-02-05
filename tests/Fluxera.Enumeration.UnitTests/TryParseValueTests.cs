namespace Fluxera.Enumeration.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class TryParseValueTests
	{
		[Test]
		public void ShouldOutputEnumForValidValue_Generic()
		{
			Color.TryParseValue(2, out Color result);
			result.Should().BeSameAs(Color.Blue);
		}

		[Test]
		public void ShouldOutputEnumForValidValue_NonGeneric()
		{
			Enumeration.TryParseValue(typeof(Color), 2, out IEnumeration result);
			result.Should().BeSameAs(Color.Blue);
		}

		[Test]
		public void ShouldOutputNullForInvalidValue_Generic()
		{
			Color.TryParseValue(-1, out Color result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldOutputNullForInvalidValue_NonGeneric()
		{
			Enumeration.TryParseValue(typeof(Color), -1, out IEnumeration result);
			result.Should().BeNull();
		}

		[Test]
		public void ShouldReturnFalseForInvalidValue_Generic()
		{
			bool result = Color.TryParseValue(-1, out Color _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnFalseForInvalidValue_NonGeneric()
		{
			bool result = Enumeration.TryParseValue(typeof(Color), -1, out IEnumeration _);
			result.Should().BeFalse();
		}

		[Test]
		public void ShouldReturnTrueForValidValue_Generic()
		{
			bool result = Color.TryParseValue(2, out Color _);
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldReturnTrueForValidValue_NonGeneric()
		{
			bool result = Enumeration.TryParseValue(typeof(Color), 2, out IEnumeration _);
			result.Should().BeTrue();
		}
	}
}
