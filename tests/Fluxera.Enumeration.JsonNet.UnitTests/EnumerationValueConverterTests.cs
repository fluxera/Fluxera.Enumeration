namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using Newtonsoft.Json;
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
			TestClass obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = "{}";

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
		public void ShouldSerializeForValue()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": 999 }";

			Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

			act.Should()
				.Throw<JsonSerializationException>();
		}

		[Test]
		public void ShouldThrowWhenNotInvalid()
		{
			string json = @"{ ""Color"": -1 }";

			Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

			act.Should()
				.Throw<JsonSerializationException>();
		}
	}
}
