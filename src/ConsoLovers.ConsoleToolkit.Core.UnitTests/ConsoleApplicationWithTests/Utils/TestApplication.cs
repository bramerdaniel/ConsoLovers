namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using JetBrains.Annotations;

   public class TestApplication<T> : ConsoleApplication<T>
      where T : class
   {
      private readonly IApplicationVerification<T> application;

      public override Task RunAsync()
      {
         application.RunAsync();
         return base.RunAsync();
      }

      public override void RunWith(T arguments)
      {
         application.RunWith(arguments);
         base.RunWith(arguments);
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
      

      public override Task RunWithAsync(T arguments)
      {
         application.RunWithAsync(arguments);
         return base.RunWithAsync(arguments);
      }

      public override Task RunWithCommandAsync(ICommandBase command)
      {
         application.RunWithCommand(command);
         return base.RunWithCommandAsync(command);
      }

      protected override Task RunWithoutArgumentsAsync()
      {
         application.RunWithoutArguments();
         return base.RunWithoutArgumentsAsync();
      }


      public TestApplication([NotNull] ICommandLineEngine commandLineEngine, [NotNull] IApplicationVerification<T> application)
         : base(commandLineEngine)
      {
         this.application = application ?? throw new ArgumentNullException(nameof(application));
      }
   }
}