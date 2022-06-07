namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class SupportedValueTypeSerializerTests
	{
		static SupportedValueTypeSerializerTests()
		{
			BsonMapper.Global.UseEnumeration(true);
		}

		private static readonly string JsonString = @"{""ByteEnum"":1,""ShortEnum"":1,""IntEnum"":1,""LongEnum"":{""$numberLong"":""1""},""FloatEnum"":1.0,""DoubleEnum"":1.0,""DecimalEnum"":{""$numberDecimal"":""1""},""StringEnum"":""1"",""GuidEnum"":{""$guid"":""00000000-0000-0000-0000-000000000000""}}";

		[Test]
		public void ShouldDeserializeForValue()
		{
			BsonDocument doc = BsonMapper.Global.ToDocument(ValueEnumsTestClass.Instance);
			string json = JsonSerializer.Serialize(doc);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(JsonString);
			ValueEnumsTestClass obj = BsonMapper.Global.ToObject<ValueEnumsTestClass>(doc);

			obj.ByteEnum.Should().BeSameAs(ByteEnum.One);
			obj.ShortEnum.Should().BeSameAs(ShortEnum.One);
			obj.IntEnum.Should().BeSameAs(IntEnum.One);
			obj.LongEnum.Should().BeSameAs(LongEnum.One);
			obj.FloatEnum.Should().BeSameAs(FloatEnum.One);
			obj.DoubleEnum.Should().BeSameAs(DoubleEnum.One);
			obj.DecimalEnum.Should().BeSameAs(DecimalEnum.One);
			obj.StringEnum.Should().BeSameAs(StringEnum.One);
			obj.GuidEnum.Should().BeSameAs(GuidEnum.One);
		}
	}
}
