// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Diagnostics;

   public static class ConsoleApplicationManager
   {
      #region Public Methods and Operators

      public static void Run<T>(string[] args) where T : IRunable
      {
         Run(typeof(T), args);
      }

      public static void Run(Type applicationType, string[] args)
      {
         var application = CreateApplicationInstance(applicationType);

         InitializeInstance(applicationType, application, args);

         RunInstance(application, args);
      }

      /// <summary>Runs the caller class. Caller must implement at least the <see cref="IRunable"/> interface</summary>
      /// <param name="args">The arguments to run the caller with.</param>
      /// <exception cref="System.InvalidOperationException">Application type could not be detected from stack trace.</exception>
      public static void RunThis(string[] args)
      {
         var callingMethod = new StackTrace().GetFrame(1).GetMethod();
         var applicationType = callingMethod.DeclaringType;
         if (applicationType == null)
            throw new InvalidOperationException("Application type could not be detected from stack trace.");

         Run(applicationType, args);
      }

      #endregion

      #region Methods

      internal static void InitializeInstance(Type applicationType, IRunable application, string[] args)
      {
         Type argumentType;
         if (IsArgumentInitializer(applicationType, out argumentType))
         {
            var methodInfo = applicationType.GetMethod("CreateArguments"); // TODO Ensure functionality with unit tests
            var argumentsInstance = methodInfo.Invoke(application, null);

            var initialize = applicationType.GetMethod("Initialize");
            initialize.Invoke(application, new[] { argumentsInstance, args });
         }
      }

      private static IRunable CreateApplicationInstance(Type type)
      {
         var instance = Activator.CreateInstance(type);
         if (instance == null)
            throw new InvalidOperationException($"Could not create instance of type {type.FullName}");

         var runable = instance as IRunable;
         if (runable == null)
            throw new InvalidOperationException($"The application type {type.Name} to run must inherit the {typeof(IRunable).Name} interface");

         return runable;
      }

      private static bool IsArgumentInitializer(Type applicationType, out Type argumentType)
      {
         foreach (Type interfaceType in applicationType.GetInterfaces())
         {
            if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IArgumentInitializer<>))
            {
               argumentType = interfaceType.GetGenericArguments()[0];
               return true;
            }
         }

         argumentType = null;
         return false;
      }

      private static void RunInstance(IRunable application, string[] args)
      {
         try
         {
            application.Run();
         }
         catch (Exception exception)
         {
            var handler = application as IExeptionHandler;
            if (handler == null || !handler.ExceptionHandled(exception))
               throw;
         }
      }

      #endregion
   }
}