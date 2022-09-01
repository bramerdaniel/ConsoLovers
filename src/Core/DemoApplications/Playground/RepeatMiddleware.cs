// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RepeatMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

public class RepeatMiddleware : Middleware<ApplicationArgs>
{
   public override async Task ExecuteAsync(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      for (int i = 0; i < 5; i++)
      {
         await Next(context, cancellationToken);
         //context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.Force =
         //   !context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.Force;
      }

      // context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.UserName = "Calvin";
      await Next(context, cancellationToken);
   }
}