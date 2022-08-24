// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

internal class ParserMiddleware<T> : Middleware<T>
   where T : class
{
   #region Constants and Fields

   private readonly ICommandLineArgumentParser parser;

   #endregion

   #region Constructors and Destructors

   public ParserMiddleware(ICommandLineArgumentParser parser)
   {
      this.parser = parser;
   }

   #endregion

   #region Public Properties

   public override int ExecutionOrder => KnownLocations.ParserMiddleware;

   #endregion

   #region Public Methods and Operators

   public override Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
         return Task.FromCanceled(cancellationToken);

      ParseArguments(context);
      return Next(context, cancellationToken);
   }

   #endregion

   #region Methods

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

   #endregion
}