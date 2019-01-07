using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.Activator.TypeOverload.Create {
	public class SimpleCreateAndNoDefaultCtor {
		public static void Main ()
		{
			var tmp = System.Activator.CreateInstance (typeof (Foo));
			HereToUseCreatedInstance (tmp);
		}

		[Kept]
		static void HereToUseCreatedInstance (object arg)
		{
		}

		[Kept]
		class Foo {
			public Foo (int arg)
			{
			}
		}
	}
}