// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PromptsDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Controls;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.Input;

   using Microsoft.Extensions.DependencyInjection;

   public static class Program
   {
      #region Methods

      private static readonly ConsoleProxy Console = new();

      private static void Main()
      {
         ShowYesNo();
         ShowComplexSelection();
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

      private static void ShowComplexSelection()
      {
         var yesTemplate = new Border(new CText($"Yes{Environment.NewLine}We will continue executing"));
         var noTemplate = new Border(new CText($"No{Environment.NewLine}We shut down immediately"));
         var cancelTemplate = new Border(new CText($"Cancel{Environment.NewLine}We surprise you"));

         var answer = Console.Choice<string>("Answer please ? ")
            .WithAnswer("Yes", yesTemplate)
            .WithAnswer("No", noTemplate)
            .WithAnswer("Cancel", cancelTemplate)
            .WithOrientation(Orientation.Horizontal, false)
            .AllowCancellation(false)
            .WithSelector("↑")
            .Show();

         Console.WriteLine(answer, ConsoleColor.Blue);
         Console.ReadLine();
      }

      #endregion
   }
}