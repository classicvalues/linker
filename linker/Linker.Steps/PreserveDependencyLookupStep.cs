//
// PreserveDependencyLookupStep.cs
//
// Author:
//   Marek Safar (marek.safar@gmail.com)
//
// Copyright (C) 2018  Microsoft Corporation
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using Mono.Cecil;

namespace Mono.Linker.Steps {
	public class PreserveDependencyLookupStep : BaseStep
	{
		protected override void ProcessAssembly (AssemblyDefinition assembly)
		{
			var module = assembly.MainModule;

			// TODO: This is expensive
			if (!module.HasTypeReference ("System.Runtime.CompilerServices.PreserveDependencyAttribute"))
				return;
				
			foreach (var type in module.Types) {
				foreach (var method in type.GetMethods ()) {
					var md = method.Resolve ();
					if (md?.HasCustomAttributes != true)
						continue;

					foreach (var ca in  md.CustomAttributes) {
						if (!IsPreserveDependencyAttribute (ca.AttributeType))
							continue;

						if (ca.ConstructorArguments.Count != 3)
							continue;

						var assemblyName = ca.ConstructorArguments [2].Value as string;
						if (assemblyName == null)
							continue;

						assembly = Context.Resolve (assemblyName);
					}
				}
			}
		}

		public static bool IsPreserveDependencyAttribute (TypeReference tr)
		{
			return tr.Name == "PreserveDependencyAttribute" && tr.Namespace == "System.Runtime.CompilerServices";
		}
	}
}