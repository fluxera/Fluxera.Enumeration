namespace Fluxera.Enumeration.EntityFramework.UnitTests
{
	using System.ComponentModel.DataAnnotations;
	using Fluxera.Enumeration.UnitTests.Enums.ValueEnums;

	public class Person
	{
		[Key]
		public string Id { get; set; }

		public string Name { get; set; }

		public Status Status { get; set; }

		public ByteEnum ByteEnum { get; set; }

		public ShortEnum ShortEnum { get; set; }

		public IntEnum IntEnum { get; set; }

		public LongEnum LongEnum { get; set; }

		public FloatEnum FloatEnum { get; set; }

		public DoubleEnum DoubleEnum { get; set; }

		public DecimalEnum DecimalEnum { get; set; }

		public StringEnum StringEnum { get; set; }

		public GuidEnum GuidEnum { get; set; }
	}
}
