// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Linq;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.Exceptions;
   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   /// <summary><see cref="IArgumentMapper{T}"/> implementation that can also map commands</summary>
   /// <typeparam name="T">The type of the argument class</typeparam>
   /// <seealso cref="MapperBase"/>
   /// <seealso cref="IArgumentMapper{T}"/>
   public class CommandMapper<T> : MapperBase, IArgumentMapper<T>
      where T : class
   {
      #region Constants and Fields

      private readonly IServiceProvider serviceProvider;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="CommandMapper{T}"/> class.</summary>
      /// <param name="serviceProvider">The serviceProvider the command mapper should use.</param>
      /// <param name="argumentReflector"></param>
      /// <exception cref="System.ArgumentNullException">serviceProvider</exception>
      public CommandMapper([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector argumentReflector)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         ArgumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
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
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      public T Map(CommandLineArgumentList arguments)
      {
         var instance = serviceProvider.GetService<T>();
         return Map(arguments, instance);
      }

      public T Map(CommandLineArgumentList arguments, T instance)
      {
         var argumentInfo = ArgumentReflector.GetTypeInfo<T>();
         var helpRequest = GetHelpRequest(arguments, argumentInfo);
         if (helpRequest != null)
         {
            MapHelpOnly(instance, argumentInfo, arguments, helpRequest);
            return instance;
         }

         if (argumentInfo.HasCommands)
            MapArgumentsToCommand(instance, argumentInfo, arguments);

         foreach (var argument in arguments.Where(x => !x.Mapped))
            UnmappedCommandLineArgument?.Invoke(this, new MapperEventArgs(argument, null, instance));

         return instance;
      }

      #endregion

      #region Properties

      /// <summary>Gets the argument reflector.</summary>
      /// <value>The argument reflector.</value>
      internal IArgumentReflector ArgumentReflector { get; }

      #endregion

      #region Methods

      private static void DecreaseIndices(CommandLineArgumentList arguments)
      {
         foreach (var argument in arguments)
            argument.Index--;
      }

      private static CommandInfo GetCommandByNameOrDefault(ArgumentClassInfo argumentInfo, string commandName, CommandLineArgumentList arguments)
      {
         if (commandName == null)
            return argumentInfo.DefaultCommand;

         foreach (var commandInfo in argumentInfo.CommandInfos)
         {
            if (IsEqual(commandInfo.ParameterName, commandName))
            {
               arguments.RemoveFirst(commandName);
               DecreaseIndices(arguments);
               return commandInfo;
            }

            foreach (var alias in commandInfo.Attribute.GetIdentifiers())
            {
               if (IsEqual(alias, commandName))
               {
                  arguments.RemoveFirst(commandName);
                  DecreaseIndices(arguments);
                  return commandInfo;
               }
            }
         }

         return argumentInfo.DefaultCommand;
      }

      private static CommandLineArgument GetFirstArgument(CommandLineArgumentList arguments)
      {
         CommandLineArgument lowestRemainingIndex = null;
         foreach (var argument in arguments)
         {
            if (lowestRemainingIndex == null || lowestRemainingIndex.Index > argument.Index)
               lowestRemainingIndex = argument;
         }

         return lowestRemainingIndex;
      }

      private static CommandLineArgument GetHelpRequest(CommandLineArgumentList arguments, ArgumentClassInfo argumentInfo)
      {
         if (argumentInfo.HelpCommand == null)
            return null;

         foreach (var identifier in argumentInfo.HelpCommand.Attribute.GetIdentifiers())
         {
            if (arguments.TryGetValue(identifier, out var commandLineArgument))
            {
               arguments.Remove(commandLineArgument);
               return commandLineArgument;
            }
         }

         return null;
      }

      private static bool IsEqual(string declaredNameOrAlias, string givenName)
      {
         return string.Equals(declaredNameOrAlias, givenName, StringComparison.InvariantCultureIgnoreCase);
      }

      private object CreateCommandInstance(CommandInfo commandInfo, CommandLineArgumentList arguments)
      {
         var commandType = commandInfo.PropertyInfo.PropertyType;
         if (!ImplementsICommand(commandType))
            throw new ArgumentException(
               $"The type '{commandType}' of the property '{commandInfo.PropertyInfo.Name}' does not implement the {typeof(ICommandBase).FullName} interface");

         if (TryGetArgumentType(commandType, out var argumentType))
         {
            var argumentInstance = serviceProvider.GetRequiredService(argumentType);

            CreateMapper(argumentType, out var mapper, out var genericType);

            var eventInfo = genericType.GetEvent(nameof(IArgumentMapper.MappedCommandLineArgument));
            EventHandler<MapperEventArgs> handler = OnMappedCommandLineArgument;

            try
            {
               eventInfo?.AddEventHandler(mapper, handler);

               var methodInfo = genericType.GetMethod(nameof(IArgumentMapper<T>.Map),
                  new[] { typeof(CommandLineArgumentList), argumentType });
               // ReSharper disable once PossibleNullReferenceException
               methodInfo.Invoke(mapper, new[] { arguments, argumentInstance });
            }
            catch (TargetInvocationException e)
            {
               if (e.InnerException is CommandLineArgumentException exception)
                  throw exception;
            }
            finally
            {
               eventInfo?.RemoveEventHandler(mapper, handler);
            }

            var command = serviceProvider.GetService(commandType);
            var argumentsProperty = commandType.GetProperty(nameof(ICommand<T>.Arguments));
            if (argumentsProperty == null)
               throw new InvalidOperationException("The ICommand<T> implementation does not contain a Arguments property.");

            argumentsProperty.SetValue(command, argumentInstance);
            return command;
         }

         return serviceProvider.GetService(commandType);
      }

      private void CreateMapper(Type argumentType, out object mapper, out Type genericType)
      {
         var argumentClassInfo = ArgumentReflector.GetTypeInfo(argumentType);
         Type mapperType;
         if (argumentClassInfo.HasCommands)
         {
            mapperType = typeof(CommandMapper<>);
         }
         else
         {
            mapperType = typeof(ArgumentMapper<>);
         }

         Type[] typeArgs = { argumentType };
         genericType = mapperType.MakeGenericType(typeArgs);
         mapper = ActivatorUtilities.GetServiceOrCreateInstance(serviceProvider, genericType);
      }

      private bool ImplementsICommand(Type commandType)
      {
         return commandType.GetInterface(typeof(ICommandBase).FullName!) != null;
      }

      private void MapApplicationArguments(T instance, CommandLineArgumentList arguments)
      {
         var defaultMapper = new ArgumentMapper<T>(serviceProvider);
         try
         {
            // Here we do not care about unmapped arguments, because they could get mapped to the command later
            defaultMapper.MappedCommandLineArgument += OnMappedCommandLineArgument;
            defaultMapper.Map(arguments, instance);
         }
         finally
         {
            defaultMapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
         }
      }

      /// <summary>Tries to map all the <see cref="arguments"/> to one of the specified commands.</summary>
      /// <param name="instance">The instance.</param>
      /// <param name="argumentInfo">The argument information.</param>
      /// <param name="arguments">The arguments.</param>
      private void MapArgumentsToCommand(T instance, ArgumentClassInfo argumentInfo, CommandLineArgumentList arguments)
      {
         // Note: per definition the help command has to be the first command line argument
         var firstArgument = GetFirstArgument(arguments);

         // NOTE: if the argument class contains a command that has the IsDefaultCommand property set to true,
         // this call will always return a command!
         var commandToCreate = GetCommandByNameOrDefault(argumentInfo, firstArgument?.Name, arguments);

         // no mapper if we could not create a command but we try to map the arguments to the application arguments first.
         // if there are remaining or shared argument, we map them to the command arguments later !
         MapApplicationArguments(instance, arguments);

         if (commandToCreate != null)
         {
            // Now that we found a command, we create it and map the remaining (and shared) parameters to the commands arguments class
            var command = CreateCommandInstance(commandToCreate, arguments);
            commandToCreate.PropertyInfo.SetValue(instance, command, null);

            var commandLineArgument = firstArgument ?? new CommandLineArgument();
            MappedCommandLineArgument?.Invoke(this, new MapperEventArgs(commandLineArgument, commandToCreate.PropertyInfo, instance));
         }
      }

      private void MapHelpOnly(T instance, ArgumentClassInfo argumentInfo, CommandLineArgumentList arguments, CommandLineArgument helpRequest)
      {
         var helpCommand = serviceProvider.GetRequiredService<HelpCommand>();
         helpCommand.Arguments = new HelpCommandArguments { ArgumentInfos = argumentInfo, ArgumentDictionary = arguments };
         argumentInfo.HelpCommand.PropertyInfo.SetValue(instance, helpCommand);

         MappedCommandLineArgument?.Invoke(this,
            new MapperEventArgs(helpRequest ?? new CommandLineArgument(), argumentInfo.HelpCommand.PropertyInfo, instance));
      }

      private void OnMappedCommandLineArgument(object sender, MapperEventArgs e)
      {
         MappedCommandLineArgument?.Invoke(this, e);
      }

      private bool TryGetArgumentType(Type commandType, out Type argumentType)
      {
         var commandInterface = commandType.GetInterface(typeof(ICommandArguments<>).FullName!);
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