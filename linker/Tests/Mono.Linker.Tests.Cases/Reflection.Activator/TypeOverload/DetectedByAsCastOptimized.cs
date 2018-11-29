using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Reflection.Activator.TypeOverload {
	[SetupCompileArgument ("/optimize+")]
	public class DetectedByAsCastOptimized {
		public static void Main ()
		{
			var tmp = System.Activator.CreateInstance (UndetectableWayOfGettingType ()) as Foo;
			HereToUseCreatedInstance (tmp);
		}
		
		[Kept]
		static void HereToUseCreatedInstance (object arg)
		{
		}

		[Kept]
		static Type UndetectableWayOfGettingType ()
		{
			return typeof (Foo);
		}

		[Kept]
		[KeptMember(".ctor()")]
		class Foo {
		}
	}
}