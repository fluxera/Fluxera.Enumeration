namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationValueConverterTests
	{
		public class TestClass
		{
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":0}";

		private static readonly JsonSerializerOptions options;

		static EnumerationRegistrationValueConverterTests()
		{
			options = new JsonSerializerOptions();
			options.UseEnumeration(true);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			TestClass obj = JsonSerializer.Deserialize<TestClass>(JsonString, options);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldSerializeForValue()
		{
			string json = JsonSerializer.Serialize(TestInstance, options);

			json.Should().Be(JsonString);
		}
	}
}
