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
      private static readonly ConsoleProxy Console = new();

      #region Methods

      private static void Main()
      {
         ShowFirst();
         Console.Clear();
         ShowSecond();

         var panel = new CPanel();
         var y = new CButton(new CText("Yes"));
         y.Clicked += OnYesButtonClicked;
         panel.Add(y);
         var no = new CButton(new CText("No "));
         no.Clicked += OnButtonClicked;
         panel.Add(no);

         Console.WriteLine("Click yes or no");
         Console.RenderInteractive(panel);

         //var panel = new CPanel();
         //panel.Add(new CText("Say"));
         //panel.Add(new CText($"Hello{Environment.NewLine}World"));
         //Console.Render(panel);

         //var button = new CButton(new CText("Click Me"));
         //button.Clicked += OnButtonClicked;
         //Console.Render(button);

         Console.ReadLine();
      }

      private static void ShowFirst()
      {
         Console.WriteLine("Continue ?");
         var list = new CSelector<bool?> { Selector = string.Empty, Orientation = Orientation.Horizontal };
         list.Add(true, "Yes");
         list.Add(false, "No");
         list.Add(null, "Unsure");

         Console.RenderInteractive(list);
         Console.WriteLine("Selected item was " + list.SelectedValue);
      }

      private static void ShowSecond()
      {
         var list = new CSelector<int?>() { Selector = ">> ", Orientation = Orientation.Horizontal };
         list.Add(1, new CBorder(new CText("One")));
         list.Add(2, new CBorder(new CText("Two")));
         list.Add(3, new CBorder(new CText("Three")));
         list.Add(4, "Four");
         list.Add(null, "null");

         Console.RenderInteractive(list);
         Console.WriteLine("Selected item was " + list.SelectedValue);
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