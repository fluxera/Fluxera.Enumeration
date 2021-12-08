namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationNameConverterTests
	{
		public class TestClass
		{
			[JsonConverter(typeof(EnumerationNameConverter<Color>))]
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":""Red""}";

		[Test]
		public void DeserializesNames()
		{
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void DeserializesNullByDefault()
		{
			string json = @"{}";

			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Color"": ""Not Found"" }";

			Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

			act.Should()
				.Throw<JsonSerializationException>();
		}

		[Test]
		public void DeserializeWhenNull()
		{
			string json = @"{ ""Color"": null }";

			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void SerializesNames()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
