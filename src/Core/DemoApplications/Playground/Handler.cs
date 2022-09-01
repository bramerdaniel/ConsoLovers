// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Handler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public class Handler : IMappingHandler<ApplicationArgs>
{
   #region IMappingHandler<ApplicationArgs> Members

   public bool TryMap(ApplicationArgs arguments, CommandLineArgument argument)
   {
      return false;
   }

   #endregion
}