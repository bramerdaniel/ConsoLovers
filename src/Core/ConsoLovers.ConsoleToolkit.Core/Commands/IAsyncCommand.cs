// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   /// <summary>Interface for an async command/></summary>
   /// <seealso cref="ICommandBase" />
   public interface IAsyncCommand : ICommandBase
   {
      #region Public Properties

      Task ExecuteAsync(CancellationToken cancellationToken);

      #endregion
   }

   /// <summary>Interface for an async command with arguments/></summary>
   public interface IAsyncCommand<T> : IAsyncCommand, ICommandArguments<T>
   {
   }
}