// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpRequest.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   using JetBrains.Annotations;

   public class TypeHelpRequest
   {
      #region Constructors and Destructors

      public TypeHelpRequest([NotNull] Type type)
      {
         Type = type ?? throw new ArgumentNullException(nameof(type));
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the type the request was created for.</summary>
      public Type Type { get; }

      #endregion

      #region Public Methods and Operators

      public bool IsCustomHeader()
      {
         return Type.GetInterface(typeof(ICustomizedHeader).FullName!) != null;
      }

      public bool IsCustomFooter()
      {
         return Type.GetInterface(typeof(ICustomizedFooter).FullName!) != null;
      }

      #endregion
   }
}