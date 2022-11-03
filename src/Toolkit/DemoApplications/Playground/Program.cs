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

   
   public static class Program
   {
      #region Constants and Fields

      private static readonly ConsoleProxy Console = new();

      #endregion

      #region Methods

      private static void Main()
      {
         //Console.Render(new CText("Simple text") { Alignment = Alignment.Right });

         var verticalPanel = new Panel
         {
            Orientation = Orientation.Vertical
         };
         for (int i = 0; i < 5; i++)
         {
            var row = new Panel();
            row.Add(new Border(new CText("Row " + i)));
            row.Add(new Border(new Link("Click me")));
            var button = new CButton(new Link("Button"));
            button.Clicked += (sender, args) =>
            {
               row.Remove((IRenderable)sender);
               row.Add(new Border(new CText("I am not a button")));
               Trace.WriteLine("Added");
            };

            row.Add(button);
            verticalPanel.Add(row);
         }

         Console.RenderInteractive(verticalPanel);
         Console.ReadLine();

         //var inner = new Panel{ Orientation = Orientation.Vertical };
         //inner.Add(new Border(new CText("Hello")));
         //inner.Add(new Border(new CText("World")));
         //Console.Render(inner);
         
         verticalPanel.Add(new CText("Simple text"));
         verticalPanel.Add(new Border(new CText("Simple text")));
         // verticalPanel.Add(inner);
         verticalPanel.Add(new CButton(new CText("Button")));

         var hansi = new Link("Hansi");
         verticalPanel.Add(hansi);
         verticalPanel.Add(new Link("Click me I am a link"){ LinkResolver = s =>
         {
            hansi.DisplayText = "Klaus";
            hansi.Style = hansi.Style.WithForeground(ConsoleColor.Cyan);
         } });
         // verticalPanel.Add(new Border(new CText("I can not be clicked")));
         Console.RenderInteractive(verticalPanel);

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