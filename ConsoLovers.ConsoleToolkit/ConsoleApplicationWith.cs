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

   /// <summary>Base class for console applications with command line parameters</summary>
   /// <typeparam name="T">The type of the parameter class</typeparam>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IApplication{T}" />
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IArgumentInitializer{T}" />
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IExeptionHandler" />
   public abstract class ConsoleApplicationWith<T> : IApplication<T>, IArgumentInitializer<T>, IExeptionHandler
      where T : class

   {
      #region Constants and Fields

      private T arguments;


      #endregion

      #region IApplication Members

      public virtual void Run()
      {
         if (HasArguments)
         {
            RunWithoutArguments();
         }
         else
         {
            RunWith(arguments);
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

      public void Initialize(T instance, string[] args)
      {
         HasArguments = args != null && args.Length > 0;
         arguments = CommandLineEngine.Map(args, instance);
         OnArgumentsInitialized(arguments);
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

      #endregion

      #region Properties

      protected ICommandLineEngine CommandLineEngine { get; set; } = new CommandLineEngine();

      /// <summary>Gets a value indicating whether this application was called with arguments.</summary>
      public bool HasArguments { get; private set; }

      #endregion

      #region Public Methods and Operators

      public static void WriteException(Exception exception)
      {
      }

      #endregion

      #region Methods

      protected virtual void OnArgumentsInitialized(T ar)
      {
      }

      protected virtual void RunWithoutArguments()
      {
         RunWith(arguments);
      }

      protected void WaitForEnter(string waitText = "Press ENTER to continue.")
      {
         Console.WriteLine(waitText);
         Console.ReadLine();
      }

      #endregion
   }
}