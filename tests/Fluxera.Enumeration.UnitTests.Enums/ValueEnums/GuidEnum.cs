namespace Fluxera.Enumeration.UnitTests.Enums.ValueEnums
{
	using System;

	public class GuidEnum : Enumeration<GuidEnum, Guid>
	{
		public static readonly GuidEnum One = new GuidEnum(Guid.Parse("7a524c5a-7724-442d-a906-8219bce4a0fd"), "One");

		/// <inheritdoc />
		public GuidEnum(Guid value, string name) 
			: base(value, name)
		{
		}
	}
}