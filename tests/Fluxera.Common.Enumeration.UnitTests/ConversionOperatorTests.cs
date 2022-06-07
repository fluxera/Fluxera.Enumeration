namespace Fluxera.Enumeration.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class ConversionOperatorTests
	{
		[Test]
		public void ShouldCastToEnumFromInt()
		{
			int value = 1;
			Color result = (Color)value;
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldCastToEnumFromInt_IEnumeration()
		{
			int value = 1;
			IEnumeration result = (Color)value;
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldCastToEnumFromNullableInt()
		{
			int? value = 1;
			Color result = (Color)value;
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldCastToEnumFromNullableInt_IEnumeration()
		{
			int? value = 1;
			IEnumeration result = (Color)value;
			result.Should().BeSameAs(Color.Green);
		}

		[Test]
		public void ShouldCastToEnumFromString()
		{
			string name = "Blue";
			Color result = (Color)name;
			result.Should().BeSameAs(Color.Blue);
		}

		[Test]
		public void ShouldCastToEnumFromString_IEnumeration()
		{
			string name = "Blue";
			IEnumeration result = (Color)name;
			result.Should().BeSameAs(Color.Blue);
		}

		[Test]
		public void ShouldCastToIntFromEnum()
		{
			Color color = Color.Green;
			int result = (int)color;
			result.Should().Be(color.Value);
		}

		[Test]
		public void ShouldCastToStringFromEnum()
		{
			Color color = Color.Green;
			string result = (string)color;
			result.Should().Be(color.Name);
		}

		[Test]
		public void ShouldReturnNullWhenCastingFromNullInt()
		{
			int? value = null;
			Color result = (Color)value;
			result.Should().BeNull();
		}

		[Test]
		public void ShouldReturnNullWhenCastingFromNullInt_IEnumeration()
		{
			int? value = null;
			IEnumeration result = (Color)value;
			result.Should().BeNull();
		}

		[Test]
		public void ShouldReturnNullWhenCastingFromNullString()
		{
			string name = null;
			Color result = (Color)name;
			result.Should().BeNull();
		}

		[Test]
		public void ShouldReturnNullWhenCastingFromNullString_IEnumeration()
		{
			string name = null;
			IEnumeration result = (Color)name;
			result.Should().BeNull();
		}
	}
}
