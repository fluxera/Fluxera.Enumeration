namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationValueConverterTests
	{
		public class TestClass
		{
			[JsonConverter(typeof(EnumerationValueConverter<Color, int>))]
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":0}";

		
		[Test]
		public void ShouldDeserializeFromValue()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = "{}";

			TestClass? obj = JsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": 999 }";

			Action act = () => JsonSerializer.Deserialize<TestClass>(json);

			act.Should()
				.Throw<JsonException>();
		}

		[Test]
		public void ShouldThrowWhenNotInvalid()
		{
			string json = @"{ ""Color"": -1 }";

			Action act = () => JsonSerializer.Deserialize<TestClass>(json);

			act.Should()
				.Throw<JsonException>();
		}

		[Test]
		public void ShouldDeserializeNullValue()
		{
			string json = @"{ ""Color"": null }";

			TestClass? obj = JsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldSerializeForValue()
		{
			string json = JsonSerializer.Serialize(TestInstance, new JsonSerializerOptions
			{
				WriteIndented = false
			});

			json.Should().Be(JsonString);
		}
	}
}
