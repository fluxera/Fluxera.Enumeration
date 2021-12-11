namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	using System;

	public class GuidEnum : Enumeration<GuidEnum, Guid>
	{
		public static readonly GuidEnum One = new GuidEnum(Guid.NewGuid(), "One");

		/// <inheritdoc />
		public GuidEnum(Guid value, string name) 
			: base(value, name)
		{
		}
	}
}