namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationNameConverterTests
	{
		public class TestClass
		{
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":""Red""}";

		private static readonly JsonSerializerOptions options;

		static EnumerationRegistrationNameConverterTests()
		{
			options = new JsonSerializerOptions();
			options.UseEnumerationNameConverter();
		}

		[Test]
		public void ShouldDeserializeFromName()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString, options);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldSerializeForName()
		{
			string json = JsonSerializer.Serialize(TestInstance, options);

			json.Should().Be(JsonString);
		}
	}
}
