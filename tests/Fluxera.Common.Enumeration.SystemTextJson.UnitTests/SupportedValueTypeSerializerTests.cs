namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System;
	using System.Text.Json;
	using FluentAssertions;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;
	using NUnit.Framework;

	[TestFixture]
	public class SupportedValueTypeSerializerTests
	{
		private static readonly JsonSerializerOptions options;

		static SupportedValueTypeSerializerTests()
		{
			options = new JsonSerializerOptions
			{
				WriteIndented = false
			};
			options.UseEnumerationValueConverter();
		}

		private static readonly string JsonString = @"{""ByteEnum"":1,""ShortEnum"":1,""IntEnum"":1,""LongEnum"":1,""FloatEnum"":1,""DoubleEnum"":1,""DecimalEnum"":1,""StringEnum"":""1"",""GuidEnum"":""" + Guid.Empty.ToString("D") + @"""" + "}";

		[Test]
		public void ShouldDeserializeFromValue()
		{
			ValueEnumsTestClass? obj = JsonSerializer.Deserialize<ValueEnumsTestClass>(JsonString, options);

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
			string json = JsonSerializer.Serialize(ValueEnumsTestClass.Instance, options);

			json.Should().Be(JsonString);
		}
	}
}
