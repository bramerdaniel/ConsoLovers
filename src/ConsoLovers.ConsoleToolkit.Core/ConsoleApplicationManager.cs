// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Diagnostics;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.BootStrappers;

   /// <summary>This class is the starting point for running an <see cref="IApplication"/> or <see cref="IApplication{T}"/></summary>
   public class ConsoleApplicationManager
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleApplicationManager"/> class.</summary>
      /// <param name="createApplication">The create application.</param>
      protected internal ConsoleApplicationManager(Func<Type, object> createApplication)
      {
         CreateApplication = createApplication ?? Activator.CreateInstance;
      }

      #endregion

      #region Properties

      /// <summary>Gets the function that creates the application object itselfe.</summary>
      protected Func<Type, object> CreateApplication { get; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Gets or sets the window title of the console window.</summary>
      protected internal string WindowTitle { get; set; }

      /// <summary>Gets or sets the height of the window.</summary>
      protected internal int? WindowHeight { get; set; }

      /// <summary>Gets or sets the width of the window.</summary>
      protected internal int? WindowWidth { get; set; }

      /// <summary>Creates a bootstrapper for the given type <see cref="T"/>.</summary>
      /// <typeparam name="T">The type of the application.</typeparam>
      /// <returns></returns>
      public static IBootstrapper<T> For<T>()
         where T : class, IApplication
      {
         return new GenericBootstrapper<T>();
      }

      /// <summary>Creates a none generic <see cref="IBootstrapper"/> instance, that can be used to configure the <see cref="IApplication"/> of the given <see cref="applicationType"/>.</summary>
      /// <param name="applicationType">Type of the application.</param>
      /// <returns>The created <see cref="IBootstrapper"/></returns>
      public static IBootstrapper For(Type applicationType)
      {
         return new DefaultBootstrapper(applicationType);
      }

      /// <summary>Runs the caller class. Caller must implement at least the <see cref="IApplication"/> interface</summary>
      /// <param name="args">The arguments to run the caller with.</param>
      /// <returns></returns>
      /// <exception cref="InvalidOperationException">Application type could not be detected from stack trace.</exception>
      /// <exception cref="System.InvalidOperationException">Application type could not be detected from stack trace.</exception>
      public static object RunThis(string[] args)
      {
         var callingMethod = new StackTrace().GetFrame(1).GetMethod();
         var applicationType = callingMethod.DeclaringType;
         if (applicationType == null)
            throw new InvalidOperationException("Application type could not be detected from stack trace.");

         return For(applicationType).Run(args);
      }

      /// <summary>Creates and runs an application of the given type with the given arguments.</summary>
      /// <param name="applicationType">Type of the application.</param>
      /// <param name="args">The arguments.</param>
      /// <returns>The application </returns>
      public IApplication Run(Type applicationType, string[] args)
      {
         SetTitle(applicationType);
         ApplySize(applicationType);

         var application = CreateApplicationInternal(applicationType);

         try
         {
            InitializeApplication(applicationType, application, args);
            return RunApplication(application);
         }
         catch (Exception exception)
         {
            // ReSharper disable once UsePatternMatching
            var handler = application as IExeptionHandler;
            if (handler == null || !handler.HandleException(exception))
               throw;

            return application;
         }
      }

      private void SetTitle(Type applicationType)
      {
         var title = WindowTitle ?? (applicationType.GetCustomAttribute(typeof(ConsoleWindowTitleAttribute)) as ConsoleWindowTitleAttribute)?.Title;
         if (title != null)
            Console.Title = title;
      }

      #endregion

      #region Methods

      internal void InitializeApplication(Type applicationType, IApplication application, string[] args)
      {
         try
         {
            if (IsArgumentInitializer(applicationType, out _))
            {
               // TODO Ensure functionality with unit tests
               var methodInfo = applicationType.GetMethod(nameof(IArgumentInitializer<Type>.CreateArguments));
               // ReSharper disable once PossibleNullReferenceException
               var argumentsInstance = methodInfo.Invoke(application, null);

               var initialize = applicationType.GetMethod(nameof(IArgumentInitializer<Type>.InitializeArguments));
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

      private T GetAttribute<T>(Type applicationType)
         where T : Attribute
      {
         return applicationType.GetCustomAttribute(typeof(T)) as T;
      }

      private void ApplySize(Type applicationType)
      {
         try
         {
            if (WindowHeight.HasValue)
            {
               Console.WindowHeight = Math.Min(WindowHeight.Value, Console.LargestWindowHeight);
            }
            else
            {
               var heightAttribute = GetAttribute<ConsoleWindowHeightAttribute>(applicationType);
               if (heightAttribute != null)
               {
                  if (Console.WindowHeight > heightAttribute.ConsoleHeight && !heightAttribute.AllowShrink)
                     return;

                  Console.WindowHeight = Math.Min(heightAttribute.ConsoleHeight, Console.LargestWindowHeight);
               }
            }
         }
         catch
         {
            // ignored
         }

         try
         {
            if (WindowWidth.HasValue)
            {
               Console.WindowWidth = Math.Min(WindowWidth.Value, Console.LargestWindowWidth);
            }
            else
            {
               var widthAttribute = GetAttribute<ConsoleWindowWidthAttribute>(applicationType);
               if (widthAttribute != null)
               {
                  if (Console.WindowWidth > widthAttribute.ConsoleWidth && !widthAttribute.AllowShrink)
                     return;

                  Console.WindowWidth = Math.Min(widthAttribute.ConsoleWidth, Console.LargestWindowWidth);
               }
            }
         }
         catch
         {
            // ignored
         }
      }

      private static bool IsArgumentInitializer(Type applicationType, out Type argumentType)
      {
         foreach (var interfaceType in applicationType.GetInterfaces())
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
      /// <exception cref="InvalidOperationException"></exception>
      /// <exception cref="System.InvalidOperationException"></exception>
      private IApplication CreateApplicationInternal(Type type)
      {
         var instance = CreateApplication(type);
         if (instance == null)
            throw new InvalidOperationException($"Could not create instance of type {type.FullName}");

         if (!(instance is IApplication application))
            throw new InvalidOperationException($"The application type {type.Name} to run must inherit the {typeof(IApplication).Name} interface");

         return application;
      }

      #endregion
   }
}