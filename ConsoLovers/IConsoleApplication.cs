namespace ConsoLovers.ConsoleToolkit
{
   using System;

   public interface IConsoleApplication : IRunable
   {
      void Initialize(string[] args);

      int Exit();
   }

   public interface IRunable
   {
      void Run();
   }

   public interface IInitializer
   {
      void Initialize(string[] args);
   }

   public interface IArgumentInitializer<T> where T : class
   {
      T CreateArguments();

      void Initialize(T instance, string[] args);
   }

   public interface IExeptionHandler
   {
      bool ExceptionHandled(Exception exception);
   }
}