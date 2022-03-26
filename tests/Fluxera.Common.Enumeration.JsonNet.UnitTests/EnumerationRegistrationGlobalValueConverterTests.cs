namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationGlobalValueConverterTests
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

		static EnumerationRegistrationGlobalValueConverterTests()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.UseEnumeration(true);

			JsonConvert.DefaultSettings = () => settings;
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldSerializeForValue()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
