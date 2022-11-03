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
         var content = new Button("1");
         var paddedBorder = new Padding(content){Value = new Thickness(1) };
         paddedBorder.Style = new RenderingStyle(null, ConsoleColor.Red);
         content.Clicked += (s, e) =>
         {
            var valueRight = paddedBorder.Value.Right + 1;
            if (content.Content is Text text)
               text.Value = valueRight.ToString().PadLeft(2).PadRight(3);

            paddedBorder.Value = new Thickness(valueRight, 1);
         };
         
         Console.RenderInteractive(paddedBorder);

         //System.Console.BufferWidth = 30;

         ////Console.Render(new CText("Simple text") { Alignment = Alignment.Right });
         //Console.WriteLine($"ForegroundColor = {Console.ForegroundColor}");
         //Console.WriteLine($"BackgroundColor = {Console.BackgroundColor}");
         //System.Console.Out.WriteLine("Test");

         //Console.ReadLine();


         var verticalPanel = new Panel
         {
            Orientation = Orientation.Vertical
         };
         for (int i = 0; i < 5; i++)
         {
            var row = new Panel();
            row.Add(new Border(new Text("Row " + i)){ Padding = new Thickness(1) });
            var link = new Link("Click me"){ LinkResolver = (s, a) => s.DisplayText += " !"  };
            row.Add(new Border(link));
            var button = new Button(new Padding(new Link("Button"),new Thickness(2,0)));
            button.Clicked += (sender, args) =>
            {
               row.Remove((IRenderable)sender);
               row.Add(new Border(new Text("I am not a button any longer")));
               Trace.WriteLine("Added");
            };

            row.Add(button);
            verticalPanel.Add(row);
         }

         var padding = new Padding(verticalPanel);
         Console.RenderInteractive(padding);
         Console.ReadLine();

         //var inner = new Panel{ Orientation = Orientation.Vertical };
         //inner.Add(new Border(new CText("Hello")));
         //inner.Add(new Border(new CText("World")));
         //Console.Render(inner);
         
         verticalPanel.Add(new Text("Simple text"));
         verticalPanel.Add(new Border(new Text("Simple text")));
         // verticalPanel.Add(inner);
         verticalPanel.Add(new Button(new Text("Button")));

         var hansi = new Link("Hansi");
         verticalPanel.Add(hansi);
         verticalPanel.Add(new Link("Click me I am a link"){ LinkResolver = (s, a) =>
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