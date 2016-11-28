namespace XCopyDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   internal class Question
   {
      private readonly string test;

      public Question(string test)
      {
         if (test == null)
            throw new ArgumentNullException(nameof(test));
         this.test = test;
      }

      public Answer Show()
      {
         Answer result = Answer.None;
         var consoleMenu = new ConsoleMenu { Header = test, CloseKeys = new[] { ConsoleKey.Escape }, Selector = string.Empty, ExecuteOnIndexSelection = true };
         consoleMenu.Add(new ConsoleMenuItem("Yes", m => { result = Answer.Yes; m.Menu.Close(); }));
         consoleMenu.Add(new ConsoleMenuItem("No", m => { result = Answer.No; m.Menu.Close(); }));
         consoleMenu.Show();
         return result;
      }
   }
}