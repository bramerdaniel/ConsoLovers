// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Diagnostics;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class ConsoleApplicationManager<T> : ConsoleApplicationManager
      where T : IApplication
   {
      #region Constructors and Destructors

      internal ConsoleApplicationManager(Func<Type, object> createApplication)
         : base(createApplication)
      {
      }

      internal ConsoleApplicationManager()
         : this(new DefaultFactory().CreateInstance)
      {
      }

      #endregion

      #region Public Methods and Operators

      public T Run(string[] args)
      {
         return (T)Run(typeof(T), args);
      }

      #endregion
   }

   public class ConsoleApplicationManager
   {
      #region Constructors and Destructors

      protected internal ConsoleApplicationManager(Func<Type, object> createApplication)
      {
         CreateApplication = createApplication ?? Activator.CreateInstance;
      }

      #endregion

      #region Properties

      protected Func<Type, object> CreateApplication { get; }

      #endregion

      #region Public Methods and Operators

      public static ConsoleApplicationBootstrapper For<T>()
      {
         return new ConsoleApplicationBootstrapper(typeof(T));
      }

      public static ConsoleApplicationBootstrapper For(Type applicationType)
      {
         return new ConsoleApplicationBootstrapper(applicationType);
      }

      /// <summary>Runs the caller class. Caller must implement at least the <see cref="IApplication"/> interface</summary>
      /// <param name="args">The arguments to run the caller with.</param>
      /// <exception cref="System.InvalidOperationException">Application type could not be detected from stack trace.</exception>
      public static void RunThis(string[] args)
      {
         var callingMethod = new StackTrace().GetFrame(1).GetMethod();
         var applicationType = callingMethod.DeclaringType;
         if (applicationType == null)
            throw new InvalidOperationException("Application type could not be detected from stack trace.");

         ConsoleApplicationManager.For(applicationType).Run(args);
      }

      /// <summary>Creates and runs an application of the given type with the given arguments.</summary>
      /// <param name="applicationType">Type of the application.</param>
      /// <param name="args">The arguments.</param>
      /// <returns>The application </returns>
      public IApplication Run(Type applicationType, string[] args)
      {
         ApplyAttributes(applicationType);

         var application = CreateApplicationInternal(applicationType);

         try
         {
            InitializeApplication(applicationType, application, args);
            return RunApplication(application);
         }
         catch (Exception exception)
         {
            var handler = application as IExeptionHandler;
            if (handler == null || !handler.HandleException(exception))
               throw;

            return application;
         }
      }

      #endregion

      #region Methods

      internal void InitializeApplication(Type applicationType, IApplication application, string[] args)
      {
         try
         {
            Type argumentType;
            if (IsArgumentInitializer(applicationType, out argumentType))
            {
               var methodInfo = applicationType.GetMethod("CreateArguments"); // TODO Ensure functionality with unit tests
               var argumentsInstance = methodInfo.Invoke(application, null);

               var initialize = applicationType.GetMethod("InitializeArguments");
               if (initialize == null)
                  throw new InvalidOperationException($"The InitializeArguments method of the {typeof(IArgumentInitializer<>).FullName} could not be found.");

               initialize.Invoke(application, new[] { argumentsInstance, args });
            }
         }
         catch (TargetInvocationException ex)
         {
            if (ex.InnerException != null)
               throw ex.InnerException;
            throw;
         }
      }

      private static void ApplyAttributes(Type applicationType)
      {
         var title = applicationType.GetCustomAttribute(typeof(ConsoleWindowTitleAttribute)) as ConsoleWindowTitleAttribute;
         if (title != null)
            System.Console.Title = title.Title;

         try
         {
            var height = applicationType.GetCustomAttribute(typeof(ConsoleWindowHeightAttribute)) as ConsoleWindowHeightAttribute;
            if (height != null)
            {
               if (System.Console.WindowWidth > height.ConsoleHeight && !height.AllowShrink)
                  return;

               System.Console.WindowHeight = height.ConsoleHeight;
            }
         }
         catch
         {
            // ignored
         }

         try
         {
            var width = applicationType.GetCustomAttribute(typeof(ConsoleWindowWidthAttribute)) as ConsoleWindowWidthAttribute;
            if (width != null)
            {
               if (System.Console.WindowWidth > width.ConsoleWidth && !width.AllowShrink)
                  return;

               System.Console.WindowWidth = width.ConsoleWidth;
            }
         }
         catch
         {
            // ignored
         }
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

      private static IApplication RunApplication(IApplication application)
      {
         application.Run();
         return application;
      }

      /// <summary>Creates the instance of the application to run. Override this method to create the <see cref="IApplication"/> instance by your own.</summary>
      /// <param name="type">The type of the application to run.</param>
      /// <returns>The created uninitialized application</returns>
      /// <exception cref="System.InvalidOperationException"></exception>
      private IApplication CreateApplicationInternal(Type type)
      {
         var instance = CreateApplication(type);
         if (instance == null)
            throw new InvalidOperationException($"Could not create instance of type {type.FullName}");

         var application = instance as IApplication;
         if (application == null)
            throw new InvalidOperationException($"The application type {type.Name} to run must inherit the {typeof(IApplication).Name} interface");

         return application;
      }

      #endregion
   }
}