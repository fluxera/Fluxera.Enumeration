namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using FluentAssertions;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationGlobalNameConverterTests
	{
		public class TestClass
		{
			public TestEnum Enum { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Enum = TestEnum.Instance,
		};

		private static readonly string JsonString = @"{""Enum"":""Instance""}";

		static EnumerationRegistrationGlobalNameConverterTests()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.UseEnumerationNameConverter();

			JsonConvert.DefaultSettings = () => settings;
		}

		[Test]
		public void DeserializesNames()
		{
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void SerializesNames()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
