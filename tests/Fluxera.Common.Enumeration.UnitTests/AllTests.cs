namespace Fluxera.Enumeration.UnitTests
{
	using Enums;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class AllTests
	{
		[Test]
		public void ShouldReturnAllEnumOptions_MultipleTypeWithBaseAndDerived_Generic()
		{
			Animal[] result = Animal.All;

			result.Should().BeEquivalentTo(new Animal[]
			{
				Mammal.Tiger,
				Mammal.Elephant,
				Reptile.Iguana,
				Reptile.Python,
			});
		}

		[Test]
		public void ShouldReturnAllEnumOptions_MultipleTypeWithBaseAndDerived_NonGeneric()
		{
			IEnumeration[] result = Enumeration.All(typeof(Animal));

			result.Should().BeEquivalentTo(new Animal[]
			{
				Mammal.Tiger,
				Mammal.Elephant,
				Reptile.Iguana,
				Reptile.Python,
			});
		}

		[Test]
		public void ShouldReturnAllEnumOptions_SingleType_Generic()
		{
			Color[] result = Color.All;

			result.Should().BeEquivalentTo(new Color[]
			{
				Color.Red,
				Color.Blue,
				Color.Green,
			});
		}

		[Test]
		public void ShouldReturnAllEnumOptions_SingleType_NonGeneric()
		{
			IEnumeration[] result = Enumeration.All(typeof(Color));

			result.Should().BeEquivalentTo(new Color[]
			{
				Color.Red,
				Color.Blue,
				Color.Green,
			});
		}

		[Test]
		public void ShouldReturnAllEnumOptions_SingleTypeWithDerived_Generic()
		{
			MessageType[] result = MessageType.All;

			result.Should().BeEquivalentTo(new MessageType[]
			{
				MessageType.Email,
				MessageType.Postal,
				MessageType.TextMessage,
			});
		}

		[Test]
		public void ShouldReturnAllEnumOptions_SingleTypeWithDerived_NonGeneric()
		{
			IEnumeration[] result = Enumeration.All(typeof(MessageType));

			result.Should().BeEquivalentTo(new MessageType[]
			{
				MessageType.Email,
				MessageType.Postal,
				MessageType.TextMessage,
			});
		}
	}
}
