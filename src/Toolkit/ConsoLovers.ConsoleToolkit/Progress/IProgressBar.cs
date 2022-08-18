namespace ConsoLovers.ConsoleToolkit.Progress
{
   using System;

   public interface IProgressBar : IDisposable
   {
      ChildProgressBar Spawn(int maxTicks, string message, ProgressBarOptions options = null);

      void Tick(string message = "");

      double Percentage { get; }

      int CurrentTick { get; }

      int MaxTicks { get; }

      ConsoleColor ForeGroundColor { get; }
   }
}