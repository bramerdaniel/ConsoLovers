// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   using JetBrains.Annotations;

   // TODO move help stuff to own namespace

   /// <summary>Implementation of the help command</summary>
   public class HelpCommand : ICommand<HelpCommandArguments>
   {
      #region Constants and Fields

      private readonly ICommandLineEngine engine;

      private readonly ILocalizationService localizationService;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="HelpCommand"/> class.</summary>
      /// <param name="engine">The <see cref="ICommandLineEngine"/> that should be used.</param>
      /// <param name="console">The console that should be used by the command.</param>
      /// <param name="localizationService">The <see cref="ILocalizationService"/> that is used for translating resources.</param>
      /// <param name="argumentReflector"></param>
      /// <exception cref="ArgumentNullException">engine</exception>
      [InjectionConstructor]
      public HelpCommand([NotNull] ICommandLineEngine engine, [NotNull] ILocalizationService localizationService,
         [NotNull] IConsole console, [NotNull] IArgumentReflector argumentReflector)
      {
         this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
         this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
         Console = console ?? throw new ArgumentNullException(nameof(console));
         ArgumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
      }

      #endregion

      #region ICommand<HelpCommandArguments> Members

      /// <summary>Executes this instance.</summary>
      public virtual void Execute()
      {
         var helpRequest = Arguments.ArgumentDictionary.OrderBy(x => x.Index).Select(x => x.Name).ToArray();
         PrintHelp(helpRequest);
      }

      public HelpCommandArguments Arguments { get; set; }

      #endregion

      #region Properties

      private IConsole Console { get; }

      public IArgumentReflector ArgumentReflector { get; }

      #endregion

      #region Methods

      private void PrintArgumentHelp(ParameterInfo parameterInfo)
      {
         if (parameterInfo.ParameterType.IsPrimitive)
         {
            engine.PrintHelp(parameterInfo.PropertyInfo, localizationService);
         }
         else
         {
            engine.PrintHelp(parameterInfo.ParameterType, localizationService);
         }
      }

      private void PrintCommandHelp(CommandInfo commandInfo, string[] remainingParameters)
      {
         var argumentName = remainingParameters.FirstOrDefault();
         if (!string.IsNullOrEmpty(argumentName))
         {
            if (commandInfo.ArgumentType == null)
            {
               // TODO: Forward to help provider ???
               Console.WriteLine("The command does not take any parameters. So no help could be found");
               return;
            }

            var classInfo = ArgumentReflector.GetTypeInfo(commandInfo.ArgumentType);
            var parameterInfo = classInfo.GetParameterInfo(argumentName);

            if (parameterInfo != null)
            {
               if (parameterInfo is CommandInfo nextCommand)
               {
                  var withoutCurrent = remainingParameters.Length > 1 ? remainingParameters.Skip(1).ToArray() : Array.Empty<string>();
                  PrintCommandHelp(nextCommand, withoutCurrent);
               }
               else
               {
                  engine.PrintHelp(parameterInfo.PropertyInfo, localizationService);
               }

               return;
            }
         }

         if (commandInfo.ArgumentType != null)
         {
            engine.PrintHelp(commandInfo.ArgumentType, localizationService);
         }
         else
         {
            engine.PrintHelp(commandInfo.PropertyInfo.PropertyType, localizationService);
         }
      }

      private void PrintHelp(params string[] helpRequest)
      {
         if (helpRequest == null || helpRequest.Length <= 0)
         {
            engine.PrintHelp(Arguments.ArgumentInfos.ArgumentType, localizationService);
            return;
         }

         var parameterInfo = Arguments.ArgumentInfos.GetParameterInfo(helpRequest[0]);
         if (parameterInfo == null)
         {
            Console.WriteLine("UnknownHelpRequest");
            return;
         }

         if (parameterInfo is CommandInfo commandInfo)
         {
            var remaining = helpRequest.Length > 1 ? helpRequest.Skip(1).ToArray() : Array.Empty<string>();
            PrintCommandHelp(commandInfo, remaining);
         }
         else
         {
            PrintArgumentHelp(parameterInfo);
         }
      }

      #endregion
   }
}