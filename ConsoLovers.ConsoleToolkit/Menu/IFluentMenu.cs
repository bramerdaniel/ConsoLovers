namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   public interface IFluentMenu : ICanAddMenuItems
   {
      IFluentMenu Where(Action<IConsoleMenuOptions> propertySetter);
   }
}