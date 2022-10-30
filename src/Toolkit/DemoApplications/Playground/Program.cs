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
         var list = new List(new Border(new Text("Oha")), (Text)"Yes", new Text($"Very {Environment.NewLine}long Text"), new Border(new Text($"Very {Environment.NewLine}long Text")), (Text)"No", (Text)"Cancel");
         //var list = new List((Text)"Yes", (Text)"No", (Text)"Cancel");
         Console.RenderInteractive(list);

         var panel = new StackPanel();
         var y = new Button(new Text("Yes"));
         y.Clicked += OnYesButtonClicked;
         panel.Add(y);
         var no = new Button(new Text("No "));
         no.Clicked += OnButtonClicked;
         panel.Add(no);

         Console.WriteLine("Click yes or no");
         Console.RenderInteractive(panel);

         //var panel = new StackPanel();
         //panel.Add(new Text("Say"));
         //panel.Add(new Text($"Hello{Environment.NewLine}World"));
         //Console.Render(panel);

         //var button = new Button(new Text("Click Me"));
         //button.Clicked += OnButtonClicked;
         //Console.Render(button);

         Console.ReadLine();
      }

      private static void OnYesButtonClicked(object sender, EventArgs e)
      {
         Console.WriteLine("Yes button clicked");
      }

      private static void OnButtonClicked(object sender, EventArgs e)
      {
         Console.WriteLine("No button clicked");

      }

      #endregion
   }
}