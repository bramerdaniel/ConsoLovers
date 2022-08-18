// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegistrationException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
   using System;

   /// <summary>Exception thrown while registration</summary>
   public class RegistrationException : Exception
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="RegistrationException"/> class.</summary>
      /// <param name="message">The message that describes the error.</param>
      public RegistrationException(string message)
         : base(message)
      {
      }

      #endregion
   }
}