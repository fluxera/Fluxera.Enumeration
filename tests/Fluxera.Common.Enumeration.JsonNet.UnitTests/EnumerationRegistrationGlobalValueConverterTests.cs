namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationGlobalValueConverterTests
	{
		public class TestClass
		{
			public TestEnum Enum { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Enum = TestEnum.Instance,
		};

		private static readonly string JsonString = @"{""Enum"":1}";

		static EnumerationRegistrationGlobalValueConverterTests()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.UseEnumerationValueConverter();

			JsonConvert.DefaultSettings = () => settings;
		}

		[Test]
		public void DeserializesValues()
		{
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void SerializesValues()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
