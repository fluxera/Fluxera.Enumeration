namespace Fluxera.Enumeration.LiteDB.UnitTests
{
	public sealed class TestEnum : Enumeration<TestEnum>
	{
		public static readonly TestEnum Instance = new TestEnum(nameof(Instance), 1);

		private TestEnum(string name, int value) : base(name, value)
		{
		}
	}
}
