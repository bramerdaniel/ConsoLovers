﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Linq;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   /// <summary>Implementation of the help command</summary>
   public class HelpCommand : ICommand<HelpCommandArguments>
   {
      public IConsole Console { get; }

      #region Constants and Fields

      private readonly ICommandLineEngine engine;

      private readonly ResourceManager resourceManager;

      #endregion

      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="HelpCommand" /> class.
      /// </summary>
      /// <param name="engine">The <see cref="ICommandLineEngine" /> that should be used.</param>
      /// <param name="console">The console that should be used by the command.</param>
      /// <param name="resourceManager">The resource manager.</param>
      /// <exception cref="System.ArgumentNullException">engine</exception>
      [InjectionConstructor]
      public HelpCommand([NotNull] ICommandLineEngine engine, [CanBeNull] IConsole console, [CanBeNull] ResourceManager resourceManager)
      {
         this.engine = engine ?? throw new ArgumentNullException(nameof(engine));

         Console = console ?? new ConsoleProxy();
         this.resourceManager = resourceManager;
      }

      #endregion

      #region ICommand Members

      /// <summary>Executes this instance.</summary>
      public virtual void Execute()
      {
         var helpRequest = Arguments.ArgumentDictionary.Values.OrderBy(x => x.Index).Select(x => x.Name).ToArray();
         PrintHelp(helpRequest);
      }

      #endregion

      #region ICommand<HelpCommandArguments> Members

      public HelpCommandArguments Arguments { get; set; }

      #endregion

      #region Methods

      private void PrintArgumentHelp(ParameterInfo parameterInfo)
      {
         if (parameterInfo.ParameterType.IsPrimitive)
         {
            engine.PrintHelp(parameterInfo.PropertyInfo, resourceManager);
         }
         else
         {
            engine.PrintHelp(parameterInfo.ParameterType, resourceManager);
         }
      }

      private void PrintCommandHelp(CommandInfo commandInfo, string argumentName)
      {
         if (!string.IsNullOrEmpty(argumentName))
         {
            if (commandInfo.ArgumentType != null)
            {
               var classInfo = ArgumentClassInfo.FromType(commandInfo.ArgumentType);
               var parameterInfo = classInfo.GetParameterInfo(argumentName);
               if (parameterInfo != null)
               {
                  engine.PrintHelp(parameterInfo.PropertyInfo, resourceManager);
                  return;
               }
            }
            else
            {
               // TODO: Forward to help provider ???
               Console.WriteLine("The command does not take any parameters. So no help could be found");
               return;
            }
         }

         if (commandInfo.ArgumentType != null)
         {
            engine.PrintHelp(commandInfo.ArgumentType, resourceManager);
         }
         else
         {
            engine.PrintHelp(commandInfo.PropertyInfo.PropertyType, resourceManager);
         }
      }

      private void PrintHelp(params string[] helpRequest)
      {
         if (helpRequest == null || helpRequest.Length <= 0)
         {
            engine.PrintHelp(Arguments.ArgumentInfos.ArgumentType, resourceManager);
            return;
         }

         var parameterInfo = Arguments.ArgumentInfos.GetParameterInfo(helpRequest[0]);
         if (parameterInfo == null)
         {
            Console.WriteLine("UnknownHelpRequest");
            return;
         }

         var commandInfo = parameterInfo as CommandInfo;
         if (commandInfo != null)
         {
            PrintCommandHelp(commandInfo, helpRequest.Length > 1 ? helpRequest[1] : null);
         }
         else
         {
            PrintArgumentHelp(parameterInfo);
         }
      }

      #endregion
   }
}