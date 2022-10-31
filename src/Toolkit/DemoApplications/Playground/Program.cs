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
   using ConsoLovers.ConsoleToolkit.Core.Input;

   public static class Program
   {
      private static readonly ConsoleProxy Console = new();

      #region Methods

      private static void Main()
      {
         var border = new Border(new CText($"Hello{Environment.NewLine}world"))
         {
            CharSet = Borders.Doubled
         };

         Console.Render(border);

         ShowYesNo();

         var count = Console.Choice<int>("Choose a number: ")
            .WithAnswer(23)
            .WithAnswer(7)
            .WithAnswer(1572)
            .Show();

         var answer = Console.Choice<string>("Answer please ? ")
            .WithAnswer("Yes")
            .WithAnswer("No")
            .WithAnswer("Cancel", new CText("Cancel", RenderingStyle.Default.WithForeground(ConsoleColor.Red)))
            .WithOrientation(Orientation.Horizontal, true)
            .AllowCancellation(false)
            .WithSelector("↑")
            .Show();

         Console.WriteLine($"Answer was {answer}");
         Console.ReadLine();
         Console.Clear();

         ShowFirst();
         ShowSecond();
         ShowThird();
         ShowFourth();

         var panel = new CPanel();
         var y = new CButton(new CText("Yes"));
         y.Clicked += OnYesButtonClicked;
         panel.Add(y);
         var no = new CButton(new CText("No "));
         no.Clicked += OnButtonClicked;
         panel.Add(no);

         Console.WriteLine("Click yes or no");
         Console.RenderInteractive(panel);

         Console.ReadLine();
      }

      private static void ShowYesNo()
      {
         try
         {
            if (!Console.YesNo("Continue ?"))
               throw new OperationCanceledException();
         }
         catch (InputCanceledException)
         {
            // We ignore
         }
      }

      private static void ShowFirst()
      {
         var panel = new CPanel();
         var list = new CSelector<bool?> { Orientation = Orientation.Horizontal };
         list.Add(true, "Yes");
         list.Add(false, "No");
         list.Add(null);
         panel.Add(new CText("Continue ? : "));
         panel.Add(list);

         Console.RenderInteractive(panel);

         Console.WriteLine($"Selected item was {list.SelectedValue}");
         Console.ReadLine();
         Console.Clear();
      }
      private static void ShowThird()
      {
         Console.WriteLine("Continue ?");
         var list = new CSelector<bool?>
         {
            Selector = "→ ",
            SelectionStyle = new RenderingStyle(ConsoleColor.Blue),
            MouseOverStyle = new RenderingStyle(ConsoleColor.DarkCyan)
         };
         list.Add(true, "Yes");
         list.Add(false, "No");
         list.Add(null, "Unsure");

         Console.RenderInteractive(list);
         Console.WriteLine("Selected item was " + list.SelectedValue);
         Console.ReadLine();
      }

      private static void ShowFourth()
      {
         Console.WriteLine("Continue ?");
         var list = new CSelector<bool?>();
         list.Add(true, new Border(new CText("Yes ")));
         list.Add(false, new Border(new CText("No  ")));
         list.Add(null, new Border(new CText("No sure yet")));

         Console.RenderInteractive(list);
         Console.WriteLine("Selected item was " + list.SelectedValue);
         Console.ReadLine();
      }

      private static void ShowSecond()
      {
         var list = new CSelector<int?>() { Selector = "↑", Orientation = Orientation.Horizontal };
         list.Add(1, new Border(new CText("One")));
         list.Add(2, new Border(new CText("Two")));
         list.Add(3, new Border(new CText("Three")));
         list.Add(4, "Four");
         list.Add(null, "null");

         Console.RenderInteractive(list);
         Console.WriteLine("Selected item was " + list.SelectedValue);
         Console.ReadLine();
         Console.Clear();
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