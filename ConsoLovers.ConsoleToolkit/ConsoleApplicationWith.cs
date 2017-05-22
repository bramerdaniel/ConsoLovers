// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationWith.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.DIContainer;

   using JetBrains.Annotations;

   /// <summary>Base class for console applications with command line parameters</summary>
   /// <typeparam name="T">The type of the parameter class</typeparam>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IApplication{T}"/>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IArgumentInitializer{T}"/>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IExeptionHandler"/>
   public abstract class ConsoleApplicationWith<T> : IApplication<T>, IArgumentInitializer<T>, IExeptionHandler
      where T : class

   {
      #region Constants and Fields

      private readonly IDependencyInjectionContainer container;

      private ICommandLineEngine commandLineEngine;

      #endregion

      #region Constructors and Destructors

      protected ConsoleApplicationWith()
         : this(new Factory())
      {
      }

      [InjectionConstructor]
      protected ConsoleApplicationWith([NotNull] IDependencyInjectionContainer container)
      {
         if (container == null)
            throw new ArgumentNullException(nameof(container));

         this.container = container;
      }

      #endregion

      #region IApplication Members

      public virtual void Run()
      {
         bool continueExecution = false;
         ICommand command;

         if (TryGetCommand(out command))
            continueExecution = RunWithCommand(command);

         if(continueExecution)
         {
            if (HasArguments)
            {
               RunWith(Arguments);
            }
            else
            {
               RunWithoutArguments();
            }
         }
      }

      #endregion

      #region IApplication<T> Members

      public abstract void RunWith(T arguments);

      #endregion

     
      #region IArgumentInitializer<T> Members

      public virtual T CreateArguments()
      {
         if (GetType() == typeof(T))
            return this as T;

         return Activator.CreateInstance<T>();
      }

      public virtual void InitializeArguments(T instance, string[] args)
      {
         HasArguments = args != null && args.Length > 0;
         Arguments = CommandLineEngine.Map(args, instance);

         OnArgumentsInitialized();
      }

      #endregion

      #region IExeptionHandler Members

      public virtual bool HandleException(Exception exception)
      {
         var missingArgumentException = exception as MissingCommandLineArgumentException;
         if (missingArgumentException != null)
         {
            Console.WriteLine("Invalid command line arguments", ConsoleColor.Yellow);
            Console.WriteLine(missingArgumentException.Message, ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("[ARGUMENT HELP]");
            CommandLineEngine.PrintHelp<T>(null);
            Console.WriteLine();
            WaitForEnter();
            return true;
         }

         return false;
      }

      #endregion

      #region Public Properties

      public static IConsole Console { get; } = new ConsoleProxy();

      protected ICommandLineEngine CommandLineEngine => commandLineEngine ?? (commandLineEngine = container.CreateInstance<CommandLineEngine>());

      /// <summary>Gets a value indicating whether this application was called with arguments.</summary>
      public bool HasArguments { get; private set; }

      protected T Arguments { get; private set; }

      #endregion

      #region Public Methods and Operators

      public virtual bool RunWithCommand(ICommand command)
      {
         command.Execute();
         return false;
      }

      #endregion

      #region Methods

      /// <summary>Called when after the arguments were initialized. This is the first method the arguments can be accessed</summary>
      protected virtual void OnArgumentsInitialized()
      {
      }

      protected virtual void RunWithoutArguments()
      {
         RunWith(Arguments);
      }

      protected void WaitForEnter(string waitText = "Press ENTER to continue.")
      {
         Console.WriteLine(waitText);
         Console.ReadLine();
      }

      protected bool TryGetCommand(out ICommand command)
      {
         foreach (var propertyInfo in typeof(T).GetProperties())
         {
            if (propertyInfo.PropertyType.GetInterface(typeof(ICommand).FullName) != null)
            {
               var value = propertyInfo.GetValue(Arguments) as ICommand;
               if (value != null)
               {
                  command = value;
                  return true;
               }
            }
         }

         command = null;
         return false;
      }

      #endregion
   }
}