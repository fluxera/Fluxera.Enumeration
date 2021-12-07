namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using System;
	using FluentAssertions;
	using Newtonsoft.Json;
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
			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(JsonString);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void DeserializesNullByDefault()
		{
			string json = @"{}";

			TestClass? obj = JsonConvert.DeserializeObject<TestClass>(json);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Enum"": ""Not Found"" }";

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
		public void SerializesNames()
		{
			string json = JsonConvert.SerializeObject(TestInstance, Formatting.None);

			json.Should().Be(JsonString);
		}
	}
}
