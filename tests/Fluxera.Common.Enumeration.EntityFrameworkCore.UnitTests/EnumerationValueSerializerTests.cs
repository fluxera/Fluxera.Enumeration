//namespace Fluxera.Common.Enumeration.MongoDB.UnitTests
//{
//	using System;
//	using FluentAssertions;
//	using global::MongoDB.Bson;
//	using global::MongoDB.Bson.Serialization;
//	using global::MongoDB.Bson.Serialization.Conventions;
//	using NUnit.Framework;

//	[TestFixture]
//	public class EnumerationValueSerializerTests
//    {
//        public class TestClass
//        {
//			public TestEnum Enum { get; set; }
//		}

//        static readonly TestClass TestInstance = new TestClass
//        {
//			Enum = TestEnum.Instance,
//        };

//        static readonly string JsonString = @"{ ""Enum"" : 1 }";

//		static EnumerationValueSerializerTests()
//		{
//			ConventionPack pack = new ConventionPack();
//			pack.AddEnumerationValueConvention();
//			ConventionRegistry.Register("ConventionPack", pack, t => true);
//		}

//		[Test]
//		public void SerializesNames()
//		{
//			string json = TestInstance.ToJson();

//			json.Should().Be(JsonString);
//		}

//		[Test]
//		public void DeserializesNames()
//		{
//			TestClass? obj = BsonSerializer.Deserialize<TestClass>(JsonString);

//			obj.Enum.Should().BeSameAs(TestEnum.Instance);
//		}

//		[Test]
//		public void DeserializesNullByDefault()
//		{
//			string json = "{}";

//			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

//			obj.Enum.Should().BeNull();
//		}

//		[Test]
//		public void DeserializeThrowsWhenNotFound()
//		{
//			string json = @"{ ""Enum"": 999 }";

//			Action act = () =>
//			{
//				TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);
//			};

//			act.Should()
//				.Throw<FormatException>();
//		}

//		[Test]
//		public void DeserializeThrowsWhenNotValid()
//		{
//			string json = @"{ ""Enum"": -1 }";

//			Action act = () =>
//			{
//				TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);
//			};

//			act.Should()
//				.Throw<FormatException>();
//		}

//		[Test]
//		public void DeserializeWhenNull()
//		{
//			string json = @"{ ""Enum"": null }";

//			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

//			obj.Enum.Should().BeNull();
//		}
//	}
//}



