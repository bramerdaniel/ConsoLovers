// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Linq;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   public class HelpCommand : ICommand<HelpCommandArguments>
   {
      #region Constants and Fields

      private readonly ICommandLineEngine engine;

      private readonly ResourceManager resourceManager;

      #endregion

      #region Constructors and Destructors

      [InjectionConstructor]
      public HelpCommand([NotNull] ICommandLineEngine engine, [CanBeNull] ResourceManager resourceManager)
      {
         if (engine == null)
            throw new ArgumentNullException(nameof(engine));

         this.engine = engine;
         this.resourceManager = resourceManager;
      }

      #endregion

      #region ICommand Members

      public virtual void Execute()
      {
         var helpRequest = Arguments.ArgumentDictionary.Values.FirstOrDefault()?.Name;
         PrintHelp(helpRequest);
      }

      #endregion

      #region ICommand<HelpArguments> Members

      public HelpCommandArguments Arguments { get; set; }

      #endregion

      #region Public Methods and Operators

      public void PrintHelp(string helpRequest)
      {
         if (helpRequest == null)
         {
            engine.PrintHelp(Arguments.ArgumentInfos.ArgumentType, resourceManager);
            return;
         }

         var parameterInfo = Arguments.ArgumentInfos.GetParameterInfo(helpRequest);
         if (parameterInfo == null)
         {
            Console.WriteLine("UnknownHelpRequest");
            return;
         }

         var commandInfo = parameterInfo as CommandInfo;
         if (commandInfo != null)
         {
            if (commandInfo.ArgumentType != null)
            {
               engine.PrintHelp(commandInfo.ArgumentType, resourceManager);
            }
            else
            {
               engine.PrintHelp(commandInfo.PropertyInfo.PropertyType, resourceManager);
            }

            return;
         }

         if (parameterInfo.ParameterType.IsPrimitive)
         {
            engine.PrintHelp(parameterInfo.PropertyInfo, resourceManager);
         }
         else
         {
            engine.PrintHelp(parameterInfo.ParameterType, resourceManager);
         }
      }

      #endregion
   }
}