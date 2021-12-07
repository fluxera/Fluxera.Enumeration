namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationRegistrationNameConverterTests
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

		private static readonly JsonSerializerOptions options;

		static EnumerationRegistrationNameConverterTests()
		{
			options = new JsonSerializerOptions();
			options.UseEnumerationNameConverter();
		}

		[Test]
		public void DeserializesNames()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString, options);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void SerializesNames()
		{
			string json = JsonSerializer.Serialize(TestInstance, options);

			json.Should().Be(JsonString);
		}
	}
}
