namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class ValueEnumsTestClass
	{
		public ByteEnum ByteEnum { get; set; }

		public ShortEnum ShortEnum { get; set; }

		public IntEnum IntEnum { get; set; }

		public LongEnum LongEnum { get; set; }

		public FloatEnum FloatEnum { get; set; }

		public DoubleEnum DoubleEnum { get; set; }

		public DecimalEnum DecimalEnum { get; set; }

		public StringEnum StringEnum { get; set; }

		public GuidEnum GuidEnum { get; set; }

		public static readonly ValueEnumsTestClass Instance = new ValueEnumsTestClass
		{
			ByteEnum = ByteEnum.One,
			ShortEnum = ShortEnum.One,
			IntEnum = IntEnum.One,
			LongEnum = LongEnum.One,
			FloatEnum = FloatEnum.One,
			DoubleEnum = DoubleEnum.One,
			DecimalEnum = DecimalEnum.One,
			StringEnum = StringEnum.One,
			GuidEnum = GuidEnum.One,
		};
	}
}
