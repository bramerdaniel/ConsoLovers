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
         ShowTable();
         RenderPadding();

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
            row.Add(new Border(new Text("Row " + i)) { Padding = new Thickness(1) });
            var link = new Link("Click me") { LinkResolver = (s, a) => s.DisplayText += " !" };
            row.Add(new Border(link));
            var button = new Button(new Padding(new Link("Button"), new Thickness(2, 0)));
            button.Clicked += (sender, _) =>
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
         verticalPanel.Add(new Link("Click me I am a link")
         {
            LinkResolver = (_, _) =>
         {
            hansi.DisplayText = "Klaus";
            hansi.Style = hansi.Style.WithForeground(ConsoleColor.Cyan);
         }
         });
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

      private static void ShowTable()
      {
         Table table = new Table();
         table.AddColumns(new Text("First"), new Text("Second"), new Text("Third"));
         table.AddRow(new Text("1"), new Text("2"), new Text("3"));

         Console.Render(table);
         Console.ReadLine();
      }

      private static void RenderPadding()
      {
         var panel = new Panel();
         var padding = new Padding(panel, new Thickness(2, 1))
         {
            Style = new RenderingStyle(null, ConsoleColor.Red)
         };

         var increaseLeft = new Button("<+");
         increaseLeft.Clicked += (_, _) => padding.Left++;
         panel.Add(increaseLeft);

         var decreaseLeft = new Button("<-");
         decreaseLeft.Clicked += (_, _) =>
         {
            Console.Clear();
            padding.Left--;
         };
         panel.Add(decreaseLeft);

         panel.Add(new Rectangle(3, new RenderingStyle(ConsoleColor.DarkYellow, ConsoleColor.Red)){ Value = '█' });

         var decrease = new Button("->");
         decrease.Clicked += (_, _) =>
         {
            Console.Clear();
            padding.Right--;
         };
         panel.Add(decrease);

         var increase = new Button("+>");
         increase.Clicked += (_, _) => padding.Right++;
         panel.Add(increase);



         Console.RenderInteractive(padding);
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