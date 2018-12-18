namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   public class TestApplication<T> : ConsoleApplication<T>
      where T : class
   {
      private readonly IApplicationVerification<T> application;

      public override void RunWith(T arguments)
      {
         application.RunWith(arguments);
      }

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

      public override void Run()
      {
         application.Run();
         base.Run();
      }

      public override void RunWithCommand(ICommand command)
      {
         application.RunWithCommand(command);
         base.RunWithCommand(command);
      }

      protected override void RunWithoutArguments()
      {
         application.RunWithoutArguments();
         base.RunWithoutArguments();
      }

      public TestApplication([NotNull] ICommandLineEngine commandLineEngine, [NotNull] IApplicationVerification<T> application)
         : base(commandLineEngine)
      {
         this.application = application ?? throw new ArgumentNullException(nameof(application));
      }
   }
}