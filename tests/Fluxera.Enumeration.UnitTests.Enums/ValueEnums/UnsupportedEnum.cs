﻿namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	public class UnsupportedEnum : Enumeration<UnsupportedEnum, bool>
	{
		public static readonly UnsupportedEnum One = new UnsupportedEnum(false, "One");

		/// <inheritdoc />
		private UnsupportedEnum(bool value, string name)
			: base(value, name)
		{
		}
	}
}
