namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationValueConverterTests
	{
		public class TestClass
		{
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{""Color"":0}";

		static EnumerationValueConverterTests()
		{
			BsonMapper.Global.UseEnumeration(true);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			BsonDocument doc = (BsonDocument?)JsonSerializer.Deserialize(JsonString);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = "{}";

			BsonDocument doc = (BsonDocument?)JsonSerializer.Deserialize(json);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldDeserializeNullValue()
		{
			string json = @"{ ""Color"": null }";

			BsonDocument doc = (BsonDocument?)JsonSerializer.Deserialize(json);
			TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldSerializeForValue()
		{
			BsonDocument doc = BsonMapper.Global.ToDocument(TestInstance);
			string json = JsonSerializer.Serialize(doc);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": 999 }";

			Action act = () =>
			{
				BsonDocument doc = (BsonDocument?)JsonSerializer.Deserialize(json);
				TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);
			};

			act.Should()
				.Throw<LiteException>();
		}

		[Test]
		public void ShouldThrowWhenNotInvalid()
		{
			string json = @"{ ""Color"": -1 }";

			Action act = () =>
			{
				BsonDocument doc = (BsonDocument?)JsonSerializer.Deserialize(json);
				TestClass obj = BsonMapper.Global.ToObject<TestClass>(doc);
			};

			act.Should()
				.Throw<LiteException>();
		}
	}
}
