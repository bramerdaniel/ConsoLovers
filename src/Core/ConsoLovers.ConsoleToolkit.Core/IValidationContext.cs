// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IValidationContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Reflection;

/// <summary>Interface that contains information about the validated argument property</summary>
public interface IValidationContext
{
   #region Public Properties

   /// <summary>Gets the name of the argument that should be validated.</summary>
   string ArgumentName { get; }

   /// <summary>Gets the command line attribute of the argument.</summary>
   CommandLineAttribute CommandLineAttribute { get; }

   /// <summary>Gets the <see cref="PropertyInfo"/> for the property that is validated.</summary>
   PropertyInfo Property { get; }

   #endregion
}