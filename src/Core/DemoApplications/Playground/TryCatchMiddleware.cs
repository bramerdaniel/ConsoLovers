// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TryCatchMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Middleware;

public class TryCatchMiddleware : Middleware<ApplicationArgs>
{
   private readonly IConsole console;

   public TryCatchMiddleware(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public override async Task ExecuteAsync(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      try
      {
         context.ParsedArguments.RemoveFirst("removeMe");

         await Next(context, cancellationToken);


      }
      catch (Exception)
      {
         console.WriteLine("--- Catch --->", ConsoleColor.Red);
      }
   }
}