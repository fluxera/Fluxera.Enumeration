namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	using System;

	public class TimeSpanEnum : Enumeration<TimeSpanEnum, TimeSpan>
	{
		public static readonly TimeSpanEnum One = new TimeSpanEnum(TimeSpan.MaxValue, "One");

		/// <inheritdoc />
		public TimeSpanEnum(TimeSpan value, string name) 
			: base(value, name)
		{
		}
	}
}