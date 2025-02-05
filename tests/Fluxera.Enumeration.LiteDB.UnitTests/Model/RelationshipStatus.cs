namespace Fluxera.Enumeration.LiteDB.UnitTests.Model
{
	using System.Runtime.CompilerServices;

	public sealed class RelationshipStatus : Enumeration<RelationshipStatus>
	{
		public static readonly RelationshipStatus Single = new RelationshipStatus(0);
		public static readonly RelationshipStatus Married = new RelationshipStatus(1);
		public static readonly RelationshipStatus Divorced = new RelationshipStatus(2);

		/// <inheritdoc />
		private RelationshipStatus(int value, [CallerMemberName] string name = null)
			: base(value, name)
		{
		}
	}
}
