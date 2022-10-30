// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Controls;
   using ConsoLovers.ConsoleToolkit.Core;

   public static class Program
   {
      private static readonly ConsoleProxy Console = new ConsoleProxy();

      #region Methods

      private static void Main()
      {

         Console.Render(new Border(new Text($"Hello{Environment.NewLine}Console")) { Alignment = Alignment.Left});
         Console.Render(new Border(new Text($"Hello{Environment.NewLine}Console")) { Alignment = Alignment.Center, Padding = new Thickness(5)});
         Console.Render(new Border(new Text($"Hello{Environment.NewLine}Console")) { Alignment = Alignment.Right });

         //var panel = new StackPanel();
         //panel.Add(new Text("Say"));
         //panel.Add(new Text($"Hello{Environment.NewLine}World"));
         //Console.Render(panel);

         //var button = new Button(new Text("Click Me"));
         //button.Clicked += OnButtonClicked;
         //Console.Render(button);

         Console.ReadLine();
      }

      private static void OnButtonClicked(object sender, EventArgs e)
      {
         Console.WriteLine("Button clicked");

      }

      #endregion
   }
}