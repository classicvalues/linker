using System;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Reflection.Activator.TypeOverload {
	public class UnusedDerivedTypesAreNotMarked {
		public static void Main ()
		{
			var tmp = System.Activator.CreateInstance (UndetectableWayOfGettingType ()) as Base;
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
		[KeptMember (".ctor()")]
		abstract class Base
		{
		}

		[Kept]
		[KeptMember (".ctor()")]
		[KeptBaseType (typeof (Base))]
		class Foo : Base {
		}

		class Bar : Base {
		}
	}
}