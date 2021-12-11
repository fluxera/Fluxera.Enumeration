namespace Fluxera.Enumeration.MongoDB.UnitTests
{
	using System;
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
			pack.UseEnumerationValueConverter();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		private static readonly string JsonString = @"{ ""ByteEnum"" : 1, ""ShortEnum"" : 1, ""IntEnum"" : 1, ""LongEnum"" : NumberLong(1), ""FloatEnum"" : 1.0, ""DoubleEnum"" : 1.0, ""DecimalEnum"" : NumberDecimal(""1""), ""StringEnum"" : ""1"", ""GuidEnum"" : """ + Guid.Empty.ToString("D") + @"""" + " }";

		[Test]
		public void ShouldDeserializeFromValue()
		{
			ValueEnumsTestClass? obj = BsonSerializer.Deserialize<ValueEnumsTestClass>(JsonString);

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

		[Test]
		public void ShouldDeserializeForValue()
		{
			string json = ValueEnumsTestClass.Instance.ToJson();

			json.Should().Be(JsonString);
		}
	}
}
