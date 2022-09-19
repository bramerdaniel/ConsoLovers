// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultExitCodeHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services
{
   using System;

   /// <summary>Default implementation of the <see cref="IExitCodeHandler"/> interface</summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IExitCodeHandler"/>
   internal sealed class DefaultExitCodeHandler : ExitCodeHandler
   {
      protected override int ComputeExitCodeForOtherExceptions(Exception exception)
      {
         return 1;
      }
   }
}