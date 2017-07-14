namespace ConsoLovers.ConsoleToolkit.Progress
{
   using System;
   using System.Collections.Concurrent;
   using System.Threading;

   public abstract class ProgressBarBase
   {
      protected readonly DateTime _startDate = DateTime.Now;
      private int _maxTicks;
      private int currentTick;
      private string _message;

      protected ProgressBarBase(int maxTicks, string message, ProgressBarOptions options)
      {
         _maxTicks = Math.Max(0, maxTicks);
         _message = message;
         Options = options ?? ProgressBarOptions.Default;
      }

      internal ProgressBarOptions Options { get; }
      internal ConcurrentBag<ChildProgressBar> Children { get; } = new ConcurrentBag<ChildProgressBar>();

      public DateTime? EndTime { get; protected set; }

      public ConsoleColor ForeGroundColor => 
         EndTime.HasValue ? Options.ForeGroundColorDone ?? Options.ForeGroundColor : Options.ForeGroundColor;

      public int CurrentTick => currentTick;

      public int MaxTicks => _maxTicks;

      public string Message => _message;

      public double Percentage
      {
         get
         {
            var percentage = Math.Max(0, Math.Min(100, (100.0 / _maxTicks) * currentTick));
            // Gracefully handle if the percentage is NaN due to division by 0
            if (double.IsNaN(percentage) || percentage < 0) percentage = 100;
            return percentage;
         }
      }

      public bool Collapse => EndTime.HasValue && Options.CollapseWhenFinished;

      protected abstract void DisplayProgress();

      public ChildProgressBar Spawn(int maxTicks, string message, ProgressBarOptions options = null)
      {
         var pbar = new ChildProgressBar(maxTicks, message, DisplayProgress, options, Grow);
         Children.Add(pbar);
         DisplayProgress();
         return pbar;
      }

      protected virtual void Grow(ProgressBarHeight direction)
      {

      }
      protected virtual void OnDone()
      {

      }

      public void Tick(string message = null)
      {
         Interlocked.Increment(ref currentTick);
         if (message != null)
            Interlocked.Exchange(ref _message, message);

         if (currentTick >= _maxTicks)
         {
            EndTime = DateTime.Now;
            OnDone();
         }
         DisplayProgress();
      }

      public void UpdateMaxTicks(int maxTicks)
      {
         Interlocked.Exchange(ref _maxTicks, maxTicks);
      }

      public void UpdateMessage(string message)
      {
         Interlocked.Exchange(ref _message, message);

         DisplayProgress();
      }
   }
}