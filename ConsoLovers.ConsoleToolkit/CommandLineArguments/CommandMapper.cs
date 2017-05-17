// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;

   using JetBrains.Annotations;

   public class CommandMapper<T> : MapperBase, IArgumentMapper<T>
      where T : class
   {
      #region Constants and Fields

      private readonly IEngineFactory engineFactory;

      #endregion

      #region Constructors and Destructors

      public CommandMapper([NotNull] IEngineFactory engineFactory)
      {
         if (engineFactory == null)
            throw new ArgumentNullException(nameof(engineFactory));

         this.engineFactory = engineFactory;
      }

      #endregion

      #region IArgumentMapper<T> Members

      public T Map(IDictionary<string, CommandLineArgument> arguments, T instance)
      {
         var argumentInfo = new ArgumentClassInfo(typeof(T));

         if (argumentInfo.HasCommands)
            MapCommands(instance, argumentInfo, arguments);

         if (arguments.Any())
         {
            var defaultMapper = new ArgumentMapper<T>(engineFactory);
            defaultMapper.Map(arguments, instance);
         }

         return instance;
      }

      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      public T Map(IDictionary<string, CommandLineArgument> arguments)
      {
         var instance = engineFactory.CreateArgumentInstance<T>();
         return Map(arguments, instance);
      }

      #endregion

      #region Methods

      private static CommandArgumentInfo GetCommandByName(ArgumentClassInfo argumentInfo, string commandName, IDictionary<string, CommandLineArgument> arguments)
      {
         if (commandName != null)
         {
            foreach (var command in argumentInfo.CommandProperties)
            {
               var commandAttribute = command.CommandAttribute;

               if (IsEqual(commandAttribute.Name, commandName))
               {
                  arguments.Remove(commandName);
                  return command;
               }

               foreach (var alias in commandAttribute.Aliases)
               {
                  if (IsEqual(alias, commandName))
                  {
                     arguments.Remove(commandName);
                     return command;
                  }
               }
            }
         }

         return argumentInfo.CommandProperties.FirstOrDefault(c => c.CommandAttribute.IsDefaultCommand);
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

      private object CreateCommandInstance(CommandArgumentInfo commandArgumentInfo, IDictionary<string, CommandLineArgument> arguments)
      {
         var commandType = commandArgumentInfo.PropertyInfo.PropertyType;
         if (!ImplementsICommand(commandType))
            throw new ArgumentException(
               $"The type '{commandType}' of the property '{commandArgumentInfo.PropertyInfo.Name}' does not implemente the {typeof(ICommand).FullName} interface");

         Type argumentType;
         if (TryGetArgumentType(commandType, out argumentType))
         {
            var argumentInstance = engineFactory.CreateArgumentInstance(argumentType);

            var mapperType = typeof(ArgumentMapper<>);
            Type[] typeArgs = { argumentType };
            var genericType = mapperType.MakeGenericType(typeArgs);
            object mapper = engineFactory.CreateArgumentInstance(genericType);

            var methodInfo = genericType.GetMethod("Map", new[] { typeof(IDictionary<string, CommandLineArgument>), argumentType });
            methodInfo.Invoke(mapper, new[] { arguments, argumentInstance });

            var command = engineFactory.CreateArgumentInstance(commandType);
            commandType.GetProperty("Arguments").SetValue(command, argumentInstance);
            return command;
         }

         return engineFactory.CreateArgumentInstance(commandType);
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

      private bool ImplementsICommand(Type commandType)
      {
         return commandType.GetInterface(typeof(ICommand).FullName) != null;
      }

      private void MapCommands(T instance, ArgumentClassInfo argumentInfo, IDictionary<string, CommandLineArgument> arguments)
      {
         // Note: per definition the command has to be the first command line argument
         var firstArgument = GetFirstArgument(arguments);

         // TODO make some checks here ???

         var commandToCreate = GetCommandByName(argumentInfo, firstArgument?.Name, arguments);
         if (commandToCreate == null)
            return;

         var command = CreateCommandInstance(commandToCreate, arguments);
         commandToCreate.PropertyInfo.SetValue(instance, command, null);
      }

      #endregion
   }
}