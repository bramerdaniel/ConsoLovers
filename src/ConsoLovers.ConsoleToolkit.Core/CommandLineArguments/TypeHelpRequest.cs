// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   using JetBrains.Annotations;

   public class TypeHelpRequest
   {
      /// <summary>Gets the type the request was created for.</summary>
      public Type Type { get; }

      public TypeHelpRequest([NotNull] Type type)
      {
         Type = type ?? throw new ArgumentNullException(nameof(type));
      }
   }
}