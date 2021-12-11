namespace Fluxera.Enumeration.SystemTextJson.UnitTests
{
	using System.Text.Json;
	using NUnit.Framework;

	[TestFixture]
	public class SupportedValueTypeTests
	{
		private static readonly JsonSerializerOptions options;

		static SupportedValueTypeTests()
		{
			options = new JsonSerializerOptions();
			options.UseEnumerationValueConverter();
		}

		[Test]
		public void ShouldDeserializeFromValue()
		{

		}
	}
}
