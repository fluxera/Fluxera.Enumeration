namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	using System;

	public class DateTimeEnum : Enumeration<DateTimeEnum, DateTime>
	{
		public static readonly DateTimeEnum One = new DateTimeEnum(DateTime.Today, "One");

		/// <inheritdoc />
		public DateTimeEnum(DateTime value, string name) 
			: base(value, name)
		{
		}
	}
}