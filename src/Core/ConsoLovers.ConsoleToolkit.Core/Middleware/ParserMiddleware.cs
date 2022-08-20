// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Services;

internal class ParserMiddleware<T> : Middleware<IExecutionContext<T>>
   where T : class
{
   private readonly ICommandLineArgumentParser parser;


   public ParserMiddleware(ICommandLineArgumentParser parser)
   {
      this.parser = parser;
   }

   public override Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
         return Task.FromCanceled(cancellationToken);

      ParseArguments(context);
      return Next(context, cancellationToken);
   }

   private void ParseArguments(IExecutionContext<T> context)
   {
      if (context.Commandline is string stringArgs)
      {
         context.ParsedArguments = parser.ParseArguments(stringArgs);
      }
      else if (context.Commandline is string[] arrayArgs)
      {
         context.ParsedArguments = parser.ParseArguments(arrayArgs);
      }
      else
      {
         throw new InvalidOperationException($"The type {context.Commandline.GetType().Name} is not supported for argument initialization");
      }
   }
}