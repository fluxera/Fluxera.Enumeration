namespace Fluxera.Enumeration.JsonNet.UnitTests
{
	using System;
	using System.Text.Json;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class SupportedValueTypeSerializerTests
	{
		private static readonly JsonSerializerOptions options;

		static SupportedValueTypeSerializerTests()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings();
			settings.UseEnumeration(true);

			JsonConvert.DefaultSettings = () => settings;
		}

		private static readonly string JsonString = @"{""ByteEnum"":1,""ShortEnum"":1,""IntEnum"":1,""LongEnum"":1,""FloatEnum"":1.0,""DoubleEnum"":1.0,""DecimalEnum"":1.0,""StringEnum"":""1"",""GuidEnum"":""" + Guid.Empty.ToString("D") + @"""" + "}";

		[Test]
		public void ShouldDeserializeForValue()
		{
			string json = JsonConvert.SerializeObject(ValueEnumsTestClass.Instance, Formatting.None);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			ValueEnumsTestClass obj = JsonConvert.DeserializeObject<ValueEnumsTestClass>(JsonString);

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
