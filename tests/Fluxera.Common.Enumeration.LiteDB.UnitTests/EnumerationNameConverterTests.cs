namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationNameConverterTests
	{
		public class TestClass
		{
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":""Red""}";

		static EnumerationNameConverterTests()
		{
			BsonMapper.Global.UseEnumeration();
		}

		[Test]
		public void ShouldDeserializeFromName()
		{
			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(JsonString);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = @"{}";

			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(json);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldDeserializeNullValue()
		{
			string json = @"{ ""Color"": null }";

			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(json);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldSerializeForName()
		{
			BsonDocument doc = BsonMapper.Global.ToDocument(TestInstance);
			string json = JsonSerializer.Serialize(doc);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": ""Not Found"" }";

			Action act = () =>
			{
				BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(json);
				TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);
			};

			act.Should()
				.Throw<LiteException>();
		}
	}
}
