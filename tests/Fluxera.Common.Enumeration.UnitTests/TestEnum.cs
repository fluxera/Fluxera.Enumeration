namespace Fluxera.Enumeration.UnitTests
{
	using System.Runtime.CompilerServices;

	public class TestEnum : Enumeration<TestEnum>
	{
		public static readonly TestEnum One = new TestEnum(1);
		public static readonly TestEnum Two = new TestEnum(2);
		public static readonly TestEnum Three = new TestEnum(3);

		protected TestEnum(int value, [CallerMemberName] string name = null)
			: base(name, value)
		{
		}
	}

	public abstract class TestBaseEnum : Enumeration<TestBaseEnum>
	{
		public static TestBaseEnum One;

		internal TestBaseEnum(string name, int value) : base(name, value)
		{
		}
	}

	public sealed class TestDerivedEnum : TestBaseEnum
	{
		static TestDerivedEnum()
		{
			One = new TestDerivedEnum(nameof(One), 1);
		}

		private TestDerivedEnum(string name, int value) : base(name, value)
		{
		}

		public new static TestBaseEnum FromValue(int value)
		{
			return TestBaseEnum.FromValue(value);
		}

		public new static TestBaseEnum FromName(string name, bool ignoreCase = false)
		{
			return TestBaseEnum.FromName(name, ignoreCase);
		}
	}

	public class TestBaseEnumWithDerivedValues : Enumeration<TestBaseEnumWithDerivedValues>
	{
		protected TestBaseEnumWithDerivedValues(string name, int value) : base(name, value)
		{
		}
	}

	public class DerivedTestEnumWithValues1 : TestBaseEnumWithDerivedValues
	{
		public static readonly DerivedTestEnumWithValues1 A = new DerivedTestEnumWithValues1(nameof(A), 1);
		public static readonly DerivedTestEnumWithValues1 B = new DerivedTestEnumWithValues1(nameof(B), 1);

		private DerivedTestEnumWithValues1(string name, int value) : base(name, value)
		{
		}
	}

	public class DerivedTestEnumWithValues2 : TestBaseEnumWithDerivedValues
	{
		public static readonly DerivedTestEnumWithValues2 C = new DerivedTestEnumWithValues2(nameof(C), 1);
		public static readonly DerivedTestEnumWithValues2 D = new DerivedTestEnumWithValues2(nameof(D), 1);

		private DerivedTestEnumWithValues2(string name, int value) : base(name, value)
		{
		}
	}
}
