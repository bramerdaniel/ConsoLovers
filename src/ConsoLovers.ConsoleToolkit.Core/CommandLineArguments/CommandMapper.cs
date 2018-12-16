// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary><see cref="IArgumentMapper{T}"/> implementation that can also map commands</summary>
   /// <typeparam name="T">The type of the argument class</typeparam>
   /// <seealso cref="MapperBase"/>
   /// <seealso cref="IArgumentMapper{T}"/>
   public class CommandMapper<T> : MapperBase, IArgumentMapper<T>
      where T : class
   {
      #region Constants and Fields

      private readonly IObjectFactory factory;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="CommandMapper{T}"/> class.</summary>
      /// <param name="factory">The factory the command mapper should use.</param>
      /// <exception cref="System.ArgumentNullException">factory</exception>
      public CommandMapper([NotNull] IObjectFactory factory)
      {
         this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
      }

      #endregion

      #region Public Events

      /// <summary>Occurs when command line argument could be mapped to a specific property of the specified class of type.</summary>
      public event EventHandler<MapperEventArgs> MappedCommandLineArgument;

      /// <summary>Occurs when a command line argument of the given arguments dictionary could not be mapped to a arguments member</summary>
      public event EventHandler<MapperEventArgs> UnmappedCommandLineArgument;

      #endregion

      #region IArgumentMapper<T> Members

      /// <summary>Maps the give argument dictionary to the given instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      public T Map(IDictionary<string, CommandLineArgument> arguments, T instance)
      {
         var argumentInfo = ArgumentClassInfo.FromType<T>();
         var helpRequest = GetHelpRequest(arguments, argumentInfo);
         if (helpRequest != null)
         {
            MapHelpOnly(instance, argumentInfo, arguments, helpRequest);
            return instance;
         }

         if (argumentInfo.HasCommands)
            MapArgumentsToCommand(instance, argumentInfo, arguments);

         if (arguments.Any())
         {
            // TODO forward events of mapped args
            var defaultMapper = new ArgumentMapper<T>(factory);
            defaultMapper.Map(arguments, instance);
         }

         foreach (var argument in arguments.Values.Where(x => !x.Mapped))
            UnmappedCommandLineArgument?.Invoke(this, new MapperEventArgs(argument, null, instance));

         return instance;
      }

      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      public T Map(IDictionary<string, CommandLineArgument> arguments)
      {
         var instance = factory.CreateInstance<T>();
         return Map(arguments, instance);
      }

      #endregion

      #region Methods

      private static CommandLineArgument GetHelpRequest(IDictionary<string, CommandLineArgument> arguments, ArgumentClassInfo argumentInfo)
      {
         if (argumentInfo.HelpCommand == null)
            return null;

         foreach (var identifier in argumentInfo.HelpCommand.Attribute.GetIdentifiers())
         {
            if (arguments.TryGetValue(identifier, out var commandLineArgument))
            {
               arguments.Remove(identifier);
               return commandLineArgument;
            }
         }

         return null;
      }

      private static void DecreaseIndices(IDictionary<string, CommandLineArgument> arguments)
      {
         foreach (var argument in arguments.Values)
            argument.Index--;
      }

      private static CommandInfo GetCommandByNameOrDefault(ArgumentClassInfo argumentInfo, string commandName, IDictionary<string, CommandLineArgument> arguments)
      {
         if (commandName == null)
            return argumentInfo.DefaultCommand;

         foreach (var commandInfo in argumentInfo.CommandInfos)
         {
            if (IsEqual(commandInfo.ParameterName, commandName))
            {
               arguments.Remove(commandName);
               DecreaseIndices(arguments);
               return commandInfo;
            }

            foreach (var alias in commandInfo.Attribute.GetIdentifiers())
            {
               if (IsEqual(alias, commandName))
               {
                  arguments.Remove(commandName);
                  DecreaseIndices(arguments);
                  return commandInfo;
               }
            }
         }

         return argumentInfo.DefaultCommand;
      }

      private static CommandLineArgument GetFirstArgument(IDictionary<string, CommandLineArgument> arguments)
      {
         CommandLineArgument lowestRemainingIndex = null;
         foreach (var argument in arguments.Values)
         {
            if (lowestRemainingIndex == null || lowestRemainingIndex.Index > argument.Index)
               lowestRemainingIndex = argument;
         }

         return lowestRemainingIndex;
      }

      private static bool IsEqual(string declaredNameOrAlias, string givenName)
      {
         return string.Equals(declaredNameOrAlias, givenName, StringComparison.InvariantCultureIgnoreCase);
      }

      private object CreateCommandInstance(CommandInfo commandInfo, IDictionary<string, CommandLineArgument> arguments)
      {
         var commandType = commandInfo.PropertyInfo.PropertyType;
         if (!ImplementsICommand(commandType))
            throw new ArgumentException(
               $"The type '{commandType}' of the property '{commandInfo.PropertyInfo.Name}' does not implement the {typeof(ICommand).FullName} interface");

         if (TryGetArgumentType(commandType, out var argumentType))
         {
            var argumentInstance = factory.CreateInstance(argumentType);

            var mapperType = typeof(ArgumentMapper<>);
            Type[] typeArgs = { argumentType };
            var genericType = mapperType.MakeGenericType(typeArgs);
            object mapper = factory.CreateInstance(genericType);

            try
            {
               var methodInfo = genericType.GetMethod(nameof(IArgumentMapper<T>.Map), new[] { typeof(IDictionary<string, CommandLineArgument>), argumentType });
               // ReSharper disable once PossibleNullReferenceException
               methodInfo.Invoke(mapper, new[] { arguments, argumentInstance });
            }
            catch (TargetInvocationException e)
            {
               if (e.InnerException is CommandLineArgumentException exception)
                  throw exception;
            }

            var command = factory.CreateInstance(commandType);
            var argumentsProperty = commandType.GetProperty(nameof(ICommand<T>.Arguments));
            if (argumentsProperty == null)
               throw new InvalidOperationException("The ICommand<T> implementation does not contain a Arguments property.");

            argumentsProperty.SetValue(command, argumentInstance);
            return command;
         }

         return factory.CreateInstance(commandType);
      }

      private bool ImplementsICommand(Type commandType)
      {
         return commandType.GetInterface(typeof(ICommand).FullName) != null;
      }

      /// <summary>Tries to map all the <see cref="arguments"/> to one of the specified commands.</summary>
      /// <param name="instance">The instance.</param>
      /// <param name="argumentInfo">The argument information.</param>
      /// <param name="arguments">The arguments.</param>
      private void MapArgumentsToCommand(T instance, ArgumentClassInfo argumentInfo, IDictionary<string, CommandLineArgument> arguments)
      {
         // Note: per definition the help command has to be the first command line argument
         var firstArgument = GetFirstArgument(arguments);

         // NOTE: if the argument class contains a command that has the IsDefaultCommand property set to true,
         // this call will always return a command!
         var commandToCreate = GetCommandByNameOrDefault(argumentInfo, firstArgument?.Name, arguments);
         if (commandToCreate == null)
            return;

         var command = CreateCommandInstance(commandToCreate, arguments);
         commandToCreate.PropertyInfo.SetValue(instance, command, null);

         MappedCommandLineArgument?.Invoke(this, new MapperEventArgs(firstArgument ?? new CommandLineArgument(), commandToCreate.PropertyInfo, instance));
      }

      private void MapHelpOnly(T instance, ArgumentClassInfo argumentInfo, IDictionary<string, CommandLineArgument> arguments, CommandLineArgument helpRequest)
      {
         var helpCommand = factory.CreateInstance<HelpCommand>();
         helpCommand.Arguments = new HelpCommandArguments { ArgumentInfos = argumentInfo, ArgumentDictionary = arguments };
         argumentInfo.HelpCommand.PropertyInfo.SetValue(instance, helpCommand);

         MappedCommandLineArgument?.Invoke(this, new MapperEventArgs(helpRequest ?? new CommandLineArgument(), argumentInfo.HelpCommand.PropertyInfo, instance));
      }

      private bool TryGetArgumentType(Type commandType, out Type argumentType)
      {
         var commandInterface = commandType.GetInterface(typeof(ICommand<>).FullName);
         if (commandInterface == null)
         {
            argumentType = null;
            return false;
         }

         argumentType = commandInterface.GenericTypeArguments[0];
         return true;
      }

      #endregion
   }
}