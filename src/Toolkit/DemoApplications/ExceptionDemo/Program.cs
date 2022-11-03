// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ExceptionDemo
{
   using System;
   using System.Runtime.InteropServices;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Controls;
   using ConsoLovers.ConsoleToolkit.Core;

   using Playground.SomeWeirdLongNamespace;

   public static class Program
   {
      #region Methods

      private static readonly ConsoleProxy Console = new();

      private static void Main()
      {
         if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
         {
            SetSize(200);

            for (int i = 0; i < 5; i++)
            {
               ThrowAndShow(true);
               Console.Clear();
               SetSize(Console.WindowWidth / 2);
            }
         }
         else
         {
            ThrowAndShow(false);
         }
      }

      private static void SetSize(int width)
      {
         if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
         {
            Console.WindowWidth = width;
         }
      }

      private static void ThrowAndShow(bool interactive)
      {
         try
         {
            var thrower = new Thrower(Invoker, 34);
            Console.WriteLine(thrower.ToString());
         }
         catch (Exception e)
         {
            var display = new ExceptionDisplay(e);
            if (interactive)
            {
               Console.RenderInteractive(display);
            }
            else
            {
               Console.Render(display);
            }
         }
      }

      static void Invoker()
      {
         Action action = () => throw new InvalidOperationException("This sounds wrong, but the text of this exception "
                                                                   + "needs to be quite long as it must be wrapped to multiple lines. To archive this is must write more and more text.");

         action();
      }

      private static void ShowVerticalWithBorders()
      {
         var result = Console.Choice<bool?>("Continue ?")
            .WithAnswer(true, new Border(new Text("Yes ")))
            .WithAnswer(false, new Border(new Text("No  ")))
            .WithAnswer(null, new Border(new Text("No sure yet")))
            .Show();

         Console.WriteLine("Selected item was " + result);
         Console.ReadLine();
         Console.Clear();
      }
      private static void ShowVerticalWithMultilineText()
      {
         var result = Console.Choice<bool?>("Continue ?")
            .WithAnswer(true, new Text($"Yes{Environment.NewLine}I want it"))
            .WithAnswer(false, new Text($"No{Environment.NewLine}Let me go"))
            .WithAnswer(null, new Text($"?{Environment.NewLine}Who cares"))
            .Show();

         Console.WriteLine("Selected item was " + result);
         Console.ReadLine();
         Console.Clear();
      }

      private static Orientation OrientationChoice()
      {
         return Console.Choice<Orientation>("What demos to show ?")
            .WithAnswer(Orientation.Horizontal, "Horizontal")
            .WithAnswer(Orientation.Vertical, "Vertical")
            .Show();
      }

      private static void ShowComplexHorizontalSelection()
      {
         var result = Console.Choice<int?>("Select a number")
            .WithAnswer(1, new Border(new Text("One")))
            .WithAnswer(2, new Border(new Text("Two")))
            .WithAnswer(3, new Border(new Text("Three")))
            .WithAnswer(4, "Four")
            .WithAnswer(null, "null")
            .WithOrientation(Orientation.Horizontal)
            .WithSelector("↑")
            .Show();

         Console.WriteLine("Selected item was " + result);
         Console.ReadLine();
         Console.Clear();
      }

      private static void ShowVerticalWithByHand()
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
         Console.Clear();
      }


      private static void ShowYesNo()
      {
         var value = Console.YesNoCancel("Continue ?");
         if (value.HasValue)
         {
            Console.WriteLine(value.Value ? "Yes" : "No");
         }
         else
         {
            Console.WriteLine("Cancel");
         }

         Console.ReadLine();
      }

      private static void ShowBoolean()
      {
         // shoes how to setup a list inside a panel 

         var panel = new Panel();
         var list = new CSelector<bool?> { Orientation = Orientation.Horizontal };
         list.Add(true);
         list.Add(false);
         list.Add(null);
         panel.Add(new Text("Continue by hand ? : "));
         panel.Add(list);

         Console.RenderInteractive(panel);

         Console.WriteLine($"Selected item was {list.SelectedValue}");
         Console.ReadLine();
         Console.Clear();
      }

      private static void ShowComplexSelection()
      {
         var yesTemplate = new Border(new Text($"Yes{Environment.NewLine}We will continue executing"));
         var noTemplate = new Border(new Text($"No{Environment.NewLine}We shut down immediately"));
         var cancelTemplate = new Border(new Text($"Cancel{Environment.NewLine}We surprise you"));

         var answer = Console.Choice<string>("Answer please ? ")
            .WithAnswer("Yes", yesTemplate)
            .WithAnswer("No", noTemplate)
            .WithAnswer("Cancel", cancelTemplate)
            .WithOrientation(Orientation.Horizontal, false)
            .AllowCancellation(false) // The user can not press escape to cancel
            .WithSelector("↑")
            .Show();

         Console.WriteLine(answer, ConsoleColor.Blue);
         Console.ReadLine();
      }

      #endregion
   }
}