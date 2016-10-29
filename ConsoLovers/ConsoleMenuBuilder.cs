
namespace ConsoLovers
{
   using System;

   using ConsoLovers.Menu;

   public class ConsoleMenuBuilder
   {
      private ConsoleMenu menu;

      public ConsoleMenuBuilder()
      {
         menu = new ConsoleMenu();
      }

      public ConsoleMenu Done()
      {
         return menu;
      }

      public void Show()
      {
         Done().Show();
      }

      public ConsoleMenuBuilder WithItem(ConsoleMenuItem item)
      {
         menu.Add(item);
         return this;
      }

      public ConsoleMenuBuilder WithHeader(string header)
      {
         menu.Header = header;
         return this;
      }

      public ConsoleMenuBuilder WithFooter(string footer)
      {
         menu.Footer = footer;
         return this;
      }

      public ConsoleMenuBuilder CloseOn(ConsoleKey key)
      {
         menu.CloseKeys = new[] { key };
         return this;
      }
   }
}