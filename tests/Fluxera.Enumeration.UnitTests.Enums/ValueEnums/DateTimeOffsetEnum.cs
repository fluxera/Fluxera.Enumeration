namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	using System;

	public class DateTimeOffsetEnum : Enumeration<DateTimeOffsetEnum, DateTimeOffset>
	{
		public static readonly DateTimeOffsetEnum One = new DateTimeOffsetEnum(DateTimeOffset.Now, "One");

		/// <inheritdoc />
		public DateTimeOffsetEnum(DateTimeOffset value, string name) 
			: base(value, name)
		{
		}
	}
}