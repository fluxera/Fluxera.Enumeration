namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationNameConverterTests
	{
		public class TestClass
		{
			[JsonConverter(typeof(EnumerationNameConverter<TestEnum>))]
			public TestEnum Enum { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Enum = TestEnum.Instance,
		};

		private static readonly string JsonString = @"{""Enum"":""Instance""}";

		[Test]
		public void DeserializesNames()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void DeserializesNullByDefault()
		{
			string json = @"{}";

			TestClass? obj = JsonSerializer.Deserialize<TestClass>(json);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Enum"": ""Not Found"" }";

			Action act = () => JsonSerializer.Deserialize<TestClass>(json);

			act.Should()
				.Throw<JsonException>();
		}

		[Test]
		public void DeserializeWhenNull()
		{
			string json = @"{ ""Enum"": null }";

			TestClass? obj = JsonSerializer.Deserialize<TestClass>(json);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void SerializesNames()
		{
			string json = JsonSerializer.Serialize(TestInstance, new JsonSerializerOptions
			{
				WriteIndented = false,
			});

			json.Should().Be(JsonString);
		}
	}
}
