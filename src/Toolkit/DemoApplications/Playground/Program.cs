// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.Diagnostics;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Controls;
   using ConsoLovers.ConsoleToolkit.Core;

   using Playground.SomeWeirdLongNamespace.ThatDoesNotMakeSense;

   public static class Program
   {
      #region Constants and Fields

      private static readonly ConsoleProxy Console = new();

      #endregion

      #region Methods

      private static void Main()
      {
         // Console.WindowWidth = 210;
         try
         {

            var thrower = new Thrower(Invoker, 34);
         }
         catch (Exception e)
         {
            ShowException(e);
         }

         var border = new Border(new Link("https://spectreconsole.net")) { CharSet = Borders.Doubled };

         Console.RenderInteractive(border);
         
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
         
         var panel = new Panel();
         var y = new CButton(new CText("Yes"));
         y.Clicked += OnYesButtonClicked;
         panel.Add(y);
         var no = new CButton(new CText("No "));
         no.Clicked += OnButtonClicked;
         panel.Add(no);

         Console.WriteLine("Click yes or no");
         Console.RenderInteractive(panel);

         Console.ReadLine();

         static void Invoker()
         {
            Action action = () => throw new InvalidOperationException("This sounds wrong, but the text of this exception "
                                                                      + "needs to be quite long as it must be wrapped to multiple lines. To archive this is must write more and more text.");

            action();
         }

      }

      private static void ShowException(Exception e)
      {
         var display = new ExceptionDisplay(e);
         Console.RenderInteractive(display);
         return;
         var stackTrace = new StackTrace(e, true);
         foreach (var stackFrame in stackTrace.GetFrames())
         {
            var frameDisplay = new StackFrameDisplay(stackFrame);
            Console.Render(frameDisplay);
         }

         Console.ReadLine();
         Console.WriteLine(e.ToString());
         Console.ReadLine();
      }

      private static void OnButtonClicked(object sender, EventArgs e)
      {
         Console.WriteLine("No button clicked");
      }

      private static void OnYesButtonClicked(object sender, EventArgs e)
      {
         Console.WriteLine("Yes button clicked");
      }

      #endregion
   }
}