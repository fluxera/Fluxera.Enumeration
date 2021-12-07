namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using System;
	using FluentAssertions;
	using Newtonsoft.Json;
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

			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void DeserializesValue()
		{
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Enum"": 999 }";

			Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

			act.Should()
				.Throw<JsonSerializationException>();
		}

		[Test]
		public void DeserializeThrowsWhenNotValid()
		{
			string json = @"{ ""Enum"": -1 }";

			Action act = () => JsonConvert.DeserializeObject<TestClass>(json);

			act.Should()
				.Throw<JsonSerializationException>();
		}

		[Test]
		public void DeserializeWhenNull()
		{
			string json = @"{ ""Enum"": null }";

			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void SerializesValue()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
