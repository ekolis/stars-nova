#region Copyright
// ============================================================================
// Nova. (c) 2010 Timothy Detering
//
// This is free software. You can redistribute it and/or modify it under the
// terms of the GNU General Public License version 2 as published by the Free
// Software Foundation.
// ============================================================================
#endregion

#region Using Statements
using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
#endregion

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if DEBUG
[assembly: AssemblyConfiguration( "Debug" )]
#else
[assembly: AssemblyConfiguration( "Retail" )]
#endif

[assembly: AssemblyCompany( "Nova" )]
[assembly: AssemblyCopyright( "Copyright © Nova 2010" )]
[assembly: AssemblyTrademark( "TODO" )]
[assembly: AssemblyCulture( "" )]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion( "0.1.0.0" )]
#if !PocketPC && !Smartphone
[assembly: AssemblyFileVersion( "0.1.0.0" )]
#endif

//
//  Set the assembly to CLS compliant.
//
[assembly: CLSCompliantAttribute( false )]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible( false )]

[assembly: NeutralResourcesLanguageAttribute( "en-US" )]

//
//  Set permission to execute but no other permissions.
//
#if (!PocketPC && !Smartphone)
[assembly: SecurityPermission(SecurityAction.RequestMinimum, Execution = true )]
//[assembly: PermissionSet(SecurityAction.RequestOptional, Name = "Nothing" )]
#endif

