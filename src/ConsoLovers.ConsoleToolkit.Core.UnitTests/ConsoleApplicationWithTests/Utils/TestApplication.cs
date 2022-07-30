// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestApplication.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   public class TestApplication<T> : ConsoleApplication<T>
      where T : class
   {
      #region Constants and Fields

      private readonly IApplicationVerification<T> application;

      #endregion

      #region Constructors and Destructors

      public TestApplication([NotNull] ICommandLineEngine commandLineEngine, [NotNull] IApplicationVerification<T> application)
         : base(commandLineEngine)
      {
         this.application = application ?? throw new ArgumentNullException(nameof(application));
      }

      #endregion

      #region Public Methods and Operators

      public override void InitializeFromString(T instance, string args)
      {
         try
         {
            CommandLineEngine.HandledCommandLineArgument += OnMappedParameter;
            CommandLineEngine.UnhandledCommandLineArgument += OnUnmappedParameter;
            base.InitializeFromString(instance, args);

            var info = ArgumentClassInfo.FromType<T>();
            foreach (var property in info.Properties)
               application.Argument(property.ParameterName, property.PropertyInfo.GetValue(instance));
         }
         finally
         {
            CommandLineEngine.HandledCommandLineArgument -= OnMappedParameter;
            CommandLineEngine.UnhandledCommandLineArgument -= OnUnmappedParameter;
         }

         void OnMappedParameter(object sender, CommandLineArgumentEventArgs e)
         {
            var value = e.PropertyInfo.GetValue(e.Instance);

            application.MappedCommandLineParameter(e.PropertyInfo);
            application.MappedCommandLineParameter(e.PropertyInfo, value);
            application.MappedCommandLineParameter(e.PropertyInfo.Name, value);
         }

         void OnUnmappedParameter(object sender, CommandLineArgumentEventArgs e)
         {
            application.UnmappedCommandLineParameter(e.Argument.Name, e.Argument.Value);
         }
      }

      public override Task RunWithAsync(T arguments, CancellationToken cancellationToken)
      {
         application.RunWithAsync(arguments);
         return base.RunWithAsync(arguments, cancellationToken);
      }

      #endregion

      #region Methods

      protected override Task OnCommandExecutedAsync(ICommandBase command)
      {
         application.RunWithCommand(command);
         return base.OnCommandExecutedAsync(command);
      }

      protected override Task RunWithoutArgumentsAsync(CancellationToken cancellationToken)
      {
         application.RunWithoutArguments();
         return base.RunWithoutArgumentsAsync(cancellationToken);
      }

      #endregion
   }
}