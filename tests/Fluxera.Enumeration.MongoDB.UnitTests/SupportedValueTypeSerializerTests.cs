namespace Fluxera.Enumeration.MongoDB.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class SupportedValueTypeSerializerTests
	{
		static SupportedValueTypeSerializerTests()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseEnumeration(true);
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		private static readonly string JsonString = @"{ ""ByteEnum"" : 1, ""ShortEnum"" : 1, ""IntEnum"" : 1, ""LongEnum"" : 1 }";
		private static readonly string JsonStringLong = @"{ ""ByteEnum"" : 1, ""ShortEnum"" : 1, ""IntEnum"" : 1, ""LongEnum"" : 444444444444 }";

		[Test]
		public void ShouldSerializeForValue()
		{
			string json = ValueEnumsTestClass.Instance.ToJson();

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldSerializeForValueLong()
		{
			string json = ValueEnumsTestClass.InstanceLong.ToJson();

			json.Should().Be(JsonStringLong);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			ValueEnumsTestClass obj = BsonSerializer.Deserialize<ValueEnumsTestClass>(JsonString);

			obj.ByteEnum.Should().BeSameAs(ByteEnum.One);
			obj.ShortEnum.Should().BeSameAs(ShortEnum.One);
			obj.IntEnum.Should().BeSameAs(IntEnum.One);
			obj.LongEnum.Should().BeSameAs(LongEnum.One);
		}

		[Test]
		public void ShouldDeserializeFromValueLong()
		{
			ValueEnumsTestClass obj = BsonSerializer.Deserialize<ValueEnumsTestClass>(JsonStringLong);

			obj.ByteEnum.Should().BeSameAs(ByteEnum.One);
			obj.ShortEnum.Should().BeSameAs(ShortEnum.One);
			obj.IntEnum.Should().BeSameAs(IntEnum.One);
			obj.LongEnum.Should().BeSameAs(LongEnum.Long);
		}
	}
}
