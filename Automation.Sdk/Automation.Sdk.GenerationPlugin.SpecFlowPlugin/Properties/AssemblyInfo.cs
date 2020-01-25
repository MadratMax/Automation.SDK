using System.Reflection;
using System.Runtime.InteropServices;
using Automation.Sdk.GenerationPlugin.SpecFlowPlugin;
using TechTalk.SpecFlow.Infrastructure;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Automation.Sdk.GenerationPlugin.SpecFlowPlugin")]
[assembly: AssemblyDescription("Generator plugin that adds ability to retry tests on failure.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Max Gaydideev")]
[assembly: AssemblyProduct("Automation.SDK")]
[assembly: AssemblyCopyright("Copyright © 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Telling the assembly to provide GeneratorPlugin
[assembly: GeneratorPlugin(typeof(GeneratorPlugin))]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("7d1d0b7f-91e8-4729-89e2-a294077fe809")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("0.0.0.1")]
[assembly: AssemblyFileVersion("0.0.0.1")]