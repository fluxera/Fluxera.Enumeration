namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using FluentAssertions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationValueConverterTests
	{
		public class TestClass
		{
			[JsonConverter(typeof(EnumerationValueConverter<TestEnum>))]
			public TestEnum Enum { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Enum = TestEnum.Instance,
		};

		private static readonly string JsonString = @"{""Enum"":1}";

		[Test]
		public void DeserializesNullByDefault()
		{
			string json = "{}";

			TestClass? obj = JsonSerializer.Deserialize<TestClass>(json);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void DeserializesValue()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Enum"": 999 }";

			Action act = () => JsonSerializer.Deserialize<TestClass>(json);

			act.Should()
				.Throw<JsonException>();
		}

		[Test]
		public void DeserializeThrowsWhenNotValid()
		{
			string json = @"{ ""Enum"": -1 }";

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
		public void SerializesValue()
		{
			string json = JsonSerializer.Serialize(TestInstance, new JsonSerializerOptions
			{
				WriteIndented = false
			});

			json.Should().Be(JsonString);
		}
	}
}
