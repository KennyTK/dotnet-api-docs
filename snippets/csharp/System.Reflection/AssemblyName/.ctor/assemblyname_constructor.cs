﻿// System.Reflection.AssemblyName.AssemblyName()
// System.Reflection.AssemblyName.Name
// System.Reflection.AssemblyName.Version

/*
   The following example demonstrates the constructor 'AssemblyName()' and
   the 'Name' and 'Version' properties of the 'AssemblyName' class. Creates
   a dynamic assembly named 'MyAssembly' with a module named 'MyModule' and
   a type within the module named 'MyType'. The type 'MyType' has a single
   method called 'Main' which is also the entry point to the assembly. The
   creation of the dynamic assembly is carried out by the method called
   'MakeAssembly'. After the assembly is created with the help of 'MakeAssembly'
   the assemblies currently loaded are found and the dynamic assembly that we
   have created is searched for, which is displayed to the console. The dynamic
   assembly is also saved to a file named 'MyAssembly.exe'.

   Note : Run 'MyAssembly.exe' which this example has created for a simple
          'Hello World!" display.
*/
// <Snippet1>
// <Snippet2>
// <Snippet3>

using System;
using System.Reflection;
using System.Threading;
using System.Reflection.Emit;

public class AssemblyName_Constructor
{
   public static void MakeAssembly(AssemblyName myAssemblyName, string fileName)
   {
      // Get the assembly builder from the application domain associated with the current thread.
      AssemblyBuilder myAssemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(myAssemblyName, AssemblyBuilderAccess.RunAndSave);
      // Create a dynamic module in the assembly.
      ModuleBuilder myModuleBuilder = myAssemblyBuilder.DefineDynamicModule("MyModule", fileName);
      // Create a type in the module.
      TypeBuilder myTypeBuilder = myModuleBuilder.DefineType("MyType");
      // Create a method called 'Main'.
      MethodBuilder myMethodBuilder = myTypeBuilder.DefineMethod("Main", MethodAttributes.Public | MethodAttributes.HideBySig |
         MethodAttributes.Static, typeof(void), null);
      // Get the Intermediate Language generator for the method.
      ILGenerator myILGenerator = myMethodBuilder.GetILGenerator();
      // Use the utility method to generate the IL instructions that print a string to the console.
      myILGenerator.EmitWriteLine("Hello World!");
      // Generate the 'ret' IL instruction.
      myILGenerator.Emit(OpCodes.Ret);
      // End the creation of the type.
      myTypeBuilder.CreateType();
      // Set the method with name 'Main' as the entry point in the assembly.
      myAssemblyBuilder.SetEntryPoint(myMethodBuilder);
      myAssemblyBuilder.Save(fileName);
   }

   public static void Main()
   {

      // Create a dynamic assembly with name 'MyAssembly' and build version '1.0.0.2001'.
      AssemblyName myAssemblyName = new AssemblyName();
      myAssemblyName.Name = "MyAssembly";
      myAssemblyName.Version = new Version("1.0.0.2001");
      MakeAssembly(myAssemblyName, "MyAssembly.exe");

      // Get all the assemblies currently loaded in the application domain.
      Assembly[] myAssemblies = Thread.GetDomain().GetAssemblies();

      // Get the dynamic assembly named 'MyAssembly'.
      Assembly myAssembly = null;
      for(int i = 0; i < myAssemblies.Length; i++)
      {
         if(String.Compare(myAssemblies[i].GetName().Name, "MyAssembly") == 0)
            myAssembly = myAssemblies[i];
      }
      if(myAssembly != null)
      {
         Console.WriteLine("\nDisplaying the assembly name\n");
         Console.WriteLine(myAssembly);
      }
   }
}
// </Snippet3>
// </Snippet2>
// </Snippet1>
