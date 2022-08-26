// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAsyncMenuCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System.Threading;
   using System.Threading.Tasks;

   public interface IAsyncMenuCommand
   {
      #region Public Methods and Operators

      /// <summary>The executes method is called when the command was executed from the menu.</summary>
      /// <param name="context">The context providing information about the calling menu item.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      /// <returns></returns>
      Task ExecuteAsync(IMenuExecutionContext context, CancellationToken cancellationToken);

      #endregion
   }
}