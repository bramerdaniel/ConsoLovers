// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Diagnostics;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;

   public abstract class ConsoleApplication : IConsoleApplication
   {
      public virtual void Initialize(string[] args)
      {
      }

      private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
      {

      }

      public abstract void Run();

      public int Exit()
      {
         return 0;
      }

      public static IConsole Console { get; } = new ConsoleProxy();

      public static void WriteException(Exception exception)
      {

      }

   }

   public abstract class ConsoleApplicationWith<T> : IRunable, IArgumentInitializer<T>
      where T : class

   {
      public static IConsole Console { get; } = new ConsoleProxy();

      public static void WriteException(Exception exception)
      {

      }

      public abstract void Run();

      public T CreateArguments()
      {
         if (GetType() == typeof(T))
            return this as T;

         return Activator.CreateInstance<T>();
      }

      public void Initialize(T instance, string[] args)
      {
         new CommandLineEngine().Map(args, instance);
      }
   }
}