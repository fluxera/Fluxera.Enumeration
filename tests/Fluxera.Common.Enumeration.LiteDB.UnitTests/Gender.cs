namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	using System.Runtime.CompilerServices;

	public sealed class Gender : Enumeration<Gender, int>
	{
		public static readonly Gender Male = new Gender(0);
		public static readonly Gender Female = new Gender(1);
		public static readonly Gender Divers = new Gender(2);

		private Gender(int value, [CallerMemberName] string name = null!)
			: base(value, name)
		{
		}
	}
}
