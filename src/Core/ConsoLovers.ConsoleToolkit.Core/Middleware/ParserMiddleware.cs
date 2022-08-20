// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Services;

internal class ParserMiddleware<T> : Middleware<IInitializationContext<T>>
   where T : class
{
   private readonly ICommandLineArgumentParser parser;


   public ParserMiddleware(ICommandLineArgumentParser parser)
   {
      this.parser = parser;
   }

   public override void Execute(IInitializationContext<T> context)
   {
      ParseArguments(context);
      Next(context);
   }

   private void ParseArguments(IInitializationContext<T> context)
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