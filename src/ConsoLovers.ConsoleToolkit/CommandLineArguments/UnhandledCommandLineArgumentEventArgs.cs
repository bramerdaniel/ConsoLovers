namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   using JetBrains.Annotations;

   /// <summary>
   /// Event args class for the <see cref="ICommandLineEngine.UnhandledCommandLineArgument"/> event
   /// </summary>
   /// <seealso cref="System.EventArgs" />
   public class UnhandledCommandLineArgumentEventArgs : EventArgs
   {
      public UnhandledCommandLineArgumentEventArgs([NotNull] CommandLineArgument argument)
      {
         Argument = argument ?? throw new ArgumentNullException(nameof(argument));
      }

      /// <summary>Gets the argument.</summary>
      public CommandLineArgument Argument { get; }
   }
}