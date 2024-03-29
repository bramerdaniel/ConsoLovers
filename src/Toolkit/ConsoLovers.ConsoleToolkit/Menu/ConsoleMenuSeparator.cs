namespace ConsoLovers.ConsoleToolkit.Menu
{
   public class ConsoleMenuSeparator : PrintableItem
   {
      /// <summary>Gets or sets the text of the separator.</summary>
      public string Text { get; set; }

      public string Label { get; set; }

      public static string DefaultText => "----";

      internal string GetText()
      {
         if (string.IsNullOrEmpty(Text))
         {
            if(Label != null)
               return $"-- {Label} --";

            return  DefaultText;
         }

         return Text;
      }
   }
}