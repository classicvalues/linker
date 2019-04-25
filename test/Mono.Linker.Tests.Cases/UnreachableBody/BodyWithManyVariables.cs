using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.UnreachableBody {
	public class BodyWithManyVariables {
		public static void Main()
		{
			UsedToMarkMethod (null);
		}

		[Kept]
		static void UsedToMarkMethod (Foo f)
		{
			f.Method ();
		}

		[Kept]
		class Foo {
			[Kept]
			[ExpectBodyModified]
			[ExpectLocalsModified]
			public void Method ()
			{
				var b1 = new Bar1 ();
				var b2 = new Bar2 ();
				var b3 = new Bar3 ();
				var b4 = new Bar4 ();
				var b5 = new Bar5 ();
				var b6 = new Bar6 ();
				b1.Method ();
				b2.Method ();
				b3.Method ();
				b4.Method ();
				b5.Method ();
				b6.Method ();
			}
		}

		class Bar1 {
			public void Method ()
			{
			}
		}

		class Bar2 {
			public void Method ()
			{
			}
		}
		
		class Bar3 {
			public void Method ()
			{
			}
		}

		class Bar4 {
			public void Method ()
			{
			}
		}
		
		class Bar5 {
			public void Method ()
			{
			}
		}

		class Bar6 {
			public void Method ()
			{
			}
		}
	}
}