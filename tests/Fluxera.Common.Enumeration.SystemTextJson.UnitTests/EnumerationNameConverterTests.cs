namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationNameConverterTests
	{
		public class TestClass
		{
			[JsonConverter(typeof(EnumerationNameConverter<Color, int>))]
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":""Red""}";

		[Test]
		public void ShouldDeserializeFromName()
		{
			TestClass? obj = JsonSerializer.Deserialize<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = @"{}";

			TestClass? obj = JsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": ""Not Found"" }";

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
		public void ShouldSerializeForName()
		{
			string json = JsonSerializer.Serialize(TestInstance, new JsonSerializerOptions
			{
				WriteIndented = false,
			});

			json.Should().Be(JsonString);
		}
	}
}
