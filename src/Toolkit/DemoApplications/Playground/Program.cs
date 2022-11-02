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
      #region Constants and Fields

      private static readonly ConsoleProxy Console = new();

      #endregion

      #region Methods

      private static void Main()
      {
         //Console.Render(new CText("Simple text") { Alignment = Alignment.Right });

         var verticalPanel = new Panel{ Orientation = Orientation.Vertical };
         //var inner = new Panel{ Orientation = Orientation.Vertical };
         //inner.Add(new Border(new CText("Hello")));
         //inner.Add(new Border(new CText("World")));
         //Console.Render(inner);
         
         verticalPanel.Add(new CText("Simple text"));
         verticalPanel.Add(new Border(new CText("Simple text")));
         // verticalPanel.Add(inner);
         verticalPanel.Add(new CButton(new CText("Button")));
         verticalPanel.Add(new Link("Click me I am a link"));
         // verticalPanel.Add(new Border(new CText("I can not be clicked")));
         Console.Render(verticalPanel);

         //var panel = new Panel();
         //var y = new CButton(new CText("Yes"));
         //y.Clicked += OnYesButtonClicked;
         //panel.Add(y);
         //var no = new CButton(new CText("No "));
         //no.Clicked += OnButtonClicked;
         //panel.Add(no);

         //Console.WriteLine("Click yes or no");
         //Console.RenderInteractive(panel);

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