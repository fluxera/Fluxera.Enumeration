namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using System;
	using System.Reflection;
	using FluentAssertions;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationNameConverterTests
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

		static EnumerationNameConverterTests()
		{
			BsonMapper.Global.UseEnumerationName(Assembly.GetExecutingAssembly());
		}

		[Test]
		public void DeserializesNames()
		{
			BsonDocument? doc = (BsonDocument?)JsonSerializer.Deserialize(JsonString);
			TestClass? obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Enum.Should().BeSameAs(TestEnum.Instance);
		}

		[Test]
		public void DeserializesNullByDefault()
		{
			string json = @"{}";

			BsonDocument? doc = (BsonDocument?)JsonSerializer.Deserialize(json);
			TestClass? obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Enum"": ""Not Found"" }";

			Action act = () =>
			{
				BsonDocument? doc = (BsonDocument?)JsonSerializer.Deserialize(json);
				TestClass? obj = BsonMapper.Global.ToObject<TestClass>(doc);
			};

			act.Should()
				.Throw<LiteException>();
		}

		[Test]
		public void DeserializeWhenNull()
		{
			string json = @"{ ""Enum"": null }";

			BsonDocument? doc = (BsonDocument?)JsonSerializer.Deserialize(json);
			TestClass? obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Enum.Should().BeNull();
		}

		[Test]
		public void SerializesNames()
		{
			BsonDocument? doc = BsonMapper.Global.ToDocument(TestInstance);
			string json = JsonSerializer.Serialize(doc);

			json.Should().Be(JsonString);
		}
	}
}
