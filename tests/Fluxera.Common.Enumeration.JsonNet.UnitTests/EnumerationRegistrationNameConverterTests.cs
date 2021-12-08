namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using Newtonsoft.Json;
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

		static EnumerationRegistrationNameConverterTests()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.UseEnumerationNameConverter<Color>();

			JsonConvert.DefaultSettings = () => settings;
		}

		[Test]
		public void DeserializesNames()
		{
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void SerializesNames()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
