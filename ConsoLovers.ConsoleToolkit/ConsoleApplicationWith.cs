// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationWith.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Contracts;

   public abstract class ConsoleApplicationWith<T> : IApplication<T>, IArgumentInitializer<T>, IExeptionHandler
      where T : class

   {
      #region Constants and Fields

      private T arguments;

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
         arguments = CommandLineEngine.Map(args, instance);
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

      #region IApplication Members

      public void Run()
      {
         RunWith(arguments);
      }

      #endregion

      #region IApplication<T> Members

      public abstract void RunWith(T arguments);

      #endregion

      #region Public Properties

      public static IConsole Console { get; } = new ConsoleProxy();

      #endregion

      #region Properties

      protected ICommandLineEngine CommandLineEngine { get; set; } = new CommandLineEngine();

      #endregion

      #region Public Methods and Operators

      public static void WriteException(Exception exception)
      {
      }

      #endregion

      #region Methods

      protected void WaitForEnter(string waitText = "Press ENTER to continue.")
      {
         Console.WriteLine(waitText);
         Console.ReadLine();
      }

      #endregion
   }
}