namespace Fluxera.Enumeration.MongoDB.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class EnumerationNameSerializerTests
	{
		public class TestClass
		{
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{ ""Color"" : ""Red"" }";

		static EnumerationNameSerializerTests()
		{
			ConventionPack pack = new ConventionPack();
			pack.AddEnumerationNameConvention();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		[Test]
		public void DeserializesNames()
		{
			TestClass? obj = BsonSerializer.Deserialize<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void DeserializesNullByDefault()
		{
			string json = @"{}";

			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void DeserializeThrowsWhenNotFound()
		{
			string json = @"{ ""Color"": ""Not Found"" }";

			Action act = () =>
			{
				TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);
			};

			act.Should()
				.Throw<FormatException>();
		}

		[Test]
		public void DeserializeWhenNull()
		{
			string json = @"{ ""Color"": null }";

			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void SerializesNames()
		{
			string json = TestInstance.ToJson();

			json.Should().Be(JsonString);
		}
	}
}
