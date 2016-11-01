namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   public interface IRootMenuBuilder : IMenuBuilder
   {
      IRootMenuBuilder WithHeader(string header);

      IRootMenuBuilder WithFooter(string footer);

      IRootMenuBuilder CloseOn(ConsoleKey closeKey);

      IRootMenuBuilder WithItem(ConsoleMenuItem item);

      IRootMenuBuilder WithItem(string text, Action<ConsoleMenuItem> execute);

      ISubMenuBuilder WithSubMenu(string text);
   }

}