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
         Console.WindowWidth = 20;
         Text t = "Hello";
         //var panel = new StackPanel();
         //panel.Add(new Border(new Text($"Hello World {Environment.NewLine}from the Console")));
         //panel.Add(new Border(new Text($"Hello World {Environment.NewLine}from the Console")));
         Console.Render(new Border((Text)$"Hello World from but the text is so long oh my god the Console"));

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