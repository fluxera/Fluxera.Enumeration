namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
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
			options.UseEnumeration(true);
		}

		private static readonly string JsonString = @"{""ByteEnum"":1,""ShortEnum"":1,""IntEnum"":1,""LongEnum"":1}";

		[Test]
		public void ShouldDeserializeForValue()
		{
			string json = JsonSerializer.Serialize(ValueEnumsTestClass.Instance, options);

			json.Should().Be(JsonString);
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{
			ValueEnumsTestClass obj = JsonSerializer.Deserialize<ValueEnumsTestClass>(JsonString, options);

			obj.ByteEnum.Should().BeSameAs(ByteEnum.One);
			obj.ShortEnum.Should().BeSameAs(ShortEnum.One);
			obj.IntEnum.Should().BeSameAs(IntEnum.One);
			obj.LongEnum.Should().BeSameAs(LongEnum.One);
		}
	}
}
