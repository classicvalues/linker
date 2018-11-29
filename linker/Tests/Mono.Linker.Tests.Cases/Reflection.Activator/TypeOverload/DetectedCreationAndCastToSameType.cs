using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.Activator.TypeOverload {
	public class DetectedCreationAndCastToSameType {
		public static void Main ()
		{
			var tmp = System.Activator.CreateInstance (typeof (Foo)) as Foo;
			HereToUseCreatedInstance (tmp);
		}
		
		[Kept]
		static void HereToUseCreatedInstance (object arg)
		{
		}

		[Kept]
		[KeptMember (".ctor()")]
		class Foo {
		}
	}
}