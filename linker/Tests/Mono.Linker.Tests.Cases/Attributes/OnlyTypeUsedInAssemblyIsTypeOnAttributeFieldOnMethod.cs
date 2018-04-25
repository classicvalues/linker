﻿using Mono.Linker.Tests.Cases.Attributes.Dependencies;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.Attributes {
	[SetupCompileBefore ("LibraryWithType.dll", new [] { typeof(TypeDefinedInReference) })]
	[SetupCompileBefore ("LibraryWithAttribute.dll", new [] { typeof(AttributeDefinedInReference) })]
	[KeptTypeInAssembly ("LibraryWithType.dll", typeof (TypeDefinedInReference))]
	[RemovedMemberInAssembly ("LibraryWithType.dll", typeof (TypeDefinedInReference), "Unused()")]
	[KeptMemberInAssembly ("LibraryWithAttribute.dll", typeof (AttributeDefinedInReference), ".ctor()")]
	[KeptMemberInAssembly ("LibraryWithAttribute.dll", typeof (AttributeDefinedInReference), "FieldType")]
	public class OnlyTypeUsedInAssemblyIsTypeOnAttributeFieldOnMethod
	{
		public static void Main ()
		{
			var foo = new Foo ();
			foo.Method ();
		}

		[Kept]
		[KeptMember (".ctor()")]
		class Foo {
			[Kept]
			[KeptAttributeAttribute (typeof (AttributeDefinedInReference))]
			[AttributeDefinedInReference (FieldType = typeof (TypeDefinedInReference))]
			public void Method ()
			{
			}
		}
	}
}