namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationValueConverterTests
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

		private static readonly JsonSerializerOptions options;

		static EnumerationRegistrationValueConverterTests()
		{
			options = new JsonSerializerOptions();
			options.UseEnumerationValueConverter();
		}

		[Test]
		public void DeserializesValues()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString, options);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void SerializesValues()
		{
			string json = JsonSerializer.Serialize(TestInstance, options);

			json.Should().Be(JsonString);
		}
	}
}
