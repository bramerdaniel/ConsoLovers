// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System.Threading.Tasks;

   public interface IAsyncCommand : ICommandBase
   {
      #region Public Properties

      Task ExecuteAsync();

      #endregion
   }

   public interface IAsyncCommand<T> : IAsyncCommand , ICommandArguments<T>
   {
   }
}