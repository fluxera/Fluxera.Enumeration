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
	public class EnumerationValueSerializerTests
	{
		public class TestClass
		{
			public Color Color { get; set; }
		}

		private static readonly TestClass TestInstance = new TestClass
		{
			Color = Color.Red,
		};

		private static readonly string JsonString = @"{ ""Color"" : 0 }";

		static EnumerationValueSerializerTests()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseEnumerationValueConverter();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			TestClass? obj = BsonSerializer.Deserialize<TestClass>(JsonString);

			obj.Color.Should().BeSameAs(Color.Red);
		}

		[Test]
		public void ShouldDeserializeNullProperty()
		{
			string json = "{}";

			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldThrowWhenNotFound()
		{
			string json = @"{ ""Color"": 999 }";

			Action act = () =>
			{
				TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);
			};

			act.Should()
				.Throw<FormatException>();
		}

		[Test]
		public void ShouldThrowWhenNotInvalid()
		{
			string json = @"{ ""Color"": -1 }";

			Action act = () =>
			{
				TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);
			};

			act.Should()
				.Throw<FormatException>();
		}

		[Test]
		public void ShouldDeserializeNullValue()
		{
			string json = @"{ ""Color"": null }";

			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

			obj.Color.Should().BeNull();
		}

		[Test]
		public void ShouldSerializeForValue()
		{
			string json = TestInstance.ToJson();

			json.Should().Be(JsonString);
		}
	}
}
