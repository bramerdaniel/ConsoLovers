namespace ProgressDemo
{
   public class ProgressInfo
   {
      #region Constructors and Destructors

      public ProgressInfo(double progressValue, int availableWidth, string text)
      {
         ProgressValue = progressValue;
         AvailableWidth = availableWidth;
         Text = text;
      }

      #endregion

      #region Public Properties

      public int AvailableWidth { get; private set; }

      public string Text { get; }

      public double ProgressValue { get; private set; }

      #endregion
   }
}