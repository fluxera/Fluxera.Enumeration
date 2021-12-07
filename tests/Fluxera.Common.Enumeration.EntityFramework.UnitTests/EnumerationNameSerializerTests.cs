//namespace Fluxera.Common.Enumeration.MongoDB.UnitTests
//{
//	using System;
//	using FluentAssertions;
//	using global::MongoDB.Bson;
//	using global::MongoDB.Bson.Serialization;
//	using global::MongoDB.Bson.Serialization.Conventions;
//	using NUnit.Framework;

//	[TestFixture]
//	public class EnumerationNameSerializerTests
//    {
//        public class TestClass
//        {
//            public TestEnum Enum { get; set; }
//		}

//        static readonly TestClass TestInstance = new TestClass
//        {
//			Enum = TestEnum.Instance,
//        };

//        static readonly string JsonString = @"{ ""Enum"" : ""Instance"" }";

//		static EnumerationNameSerializerTests()
//		{
//			ConventionPack pack = new ConventionPack();
//			pack.AddEnumerationNameConvention();
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
//			string json = @"{}";

//			TestClass? obj = BsonSerializer.Deserialize<TestClass>(json);

//			obj.Enum.Should().BeNull();
//		}

//		[Test]
//		public void DeserializeThrowsWhenNotFound()
//		{
//			string json = @"{ ""Enum"": ""Not Found"" }";

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


