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
			TestClass obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = @"{}";

			TestClass obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldDeserializeNullValue()
		{
			string json = @"{ ""Color"": null }";

			TestClass obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldSerializeForName()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": ""Not Found"" }";

			Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

			act.Should()
				.Throw<JsonSerializationException>();
		}
	}
}
