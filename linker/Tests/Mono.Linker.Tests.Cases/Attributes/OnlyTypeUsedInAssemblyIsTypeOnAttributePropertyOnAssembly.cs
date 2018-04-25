﻿using Mono.Linker.Tests.Cases.Attributes.Dependencies;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

[assembly: AttributeDefinedInReference (PropertyType = typeof (TypeDefinedInReference))]

namespace Mono.Linker.Tests.Cases.Attributes {
	/// <summary>
	/// In the case of attributes on assemblies, we expect both assemblies to be removed because we don't keep assembly level attributes
	/// when that is the only type marked in the assembly
	/// </summary>
	[SetupCompileBefore ("LibraryWithType.dll", new [] { typeof(TypeDefinedInReference) })]
	[SetupCompileBefore ("LibraryWithAttribute.dll", new [] { typeof(AttributeDefinedInReference) })]
	[RemovedAssembly ("LibraryWithType.dll")]
	[RemovedAssembly ("LibraryWithAttribute.dll")]
	public class OnlyTypeUsedInAssemblyIsTypeOnAttributePropertyOnAssembly {
		public static void Main ()
		{
		}
	}
}