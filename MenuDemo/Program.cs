// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenuDemo
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.Threading;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Menu;
   using ConsoLovers.ConsoleToolkit.PInvoke;

   using ShellProgressBar;

   class Program
   {
      #region Constants and Fields

      private static string userName;

      #endregion

      #region Methods

      private static bool CanConnectToServer()
      {
         return userName != null;
      }

      private static void ChangeMenu()
      {
         var menu = new ConsoleMenu { Header = "This is a sub menu", Selector = ">>" };
         menu.Add(new ConsoleMenuItem("Go home", ShowProgress));
         menu.Add(new ConsoleMenuItem("Go to bed", InsertName));
         menu.Add(new ConsoleMenuItem("Back to main menu", x => menu.Close()));
         menu.Theme.MenuItem.SelectedForeground = Color.Green;
         menu.Theme.MenuItem.SelectedBackground = Color.Red;
         menu.Show();
      }

      private static void ConnectToServer(ConsoleMenuItem sender)
      {
         if (userName == null)
            return;

         var progressBar = new ProgressBar(
            100,
            "Connecting to server with username " + userName,
            new ProgressBarOptions { BackgroundColor = ConsoleColor.Blue, ForeGroundColor = ConsoleColor.Cyan, ProgressBarOnBottom = false, ProgressCharacter = '*' });
         for (int i = 0; i < 100; i++)
         {
            progressBar.Tick();
            Thread.Sleep(10);
         }

         userName = null;
      }

      private static ConsoleMenuItem CreatCircularSelectionMenu(ConsoleMenu menu)
      {
         return new ConsoleMenuItem(
            $"CircularSelection = {menu.CircularSelection}",
            x =>
            {
               x.Menu.CircularSelection = !x.Menu.CircularSelection;
               x.Text = $"CircularSelection = {x.Menu.CircularSelection}";
            });
      }

      private static ConsoleMenuItem CreatClearOnExecutionMenu(bool initialValue)
      {
         return new ConsoleMenuItem(
            $"ClearOnExecution = {initialValue}",
            x =>
            {
               x.Menu.ClearOnExecution = !x.Menu.ClearOnExecution;
               x.Text = $"ClearOnExecution = {x.Menu.ClearOnExecution}";
            });
      }

      private static ConsoleMenuItem CreateColorMenu()
      {
         var crazyTheme = new MenuColorTheme();
         crazyTheme.Selector.Foreground = Color.Yellow;
         crazyTheme.MenuItem.Foreground = Color.DarkRed;
         crazyTheme.MenuItem.DisabledForeground = Color.Magenta;
         crazyTheme.MenuItem.SelectedForeground = Color.Green;
         crazyTheme.MenuItem.SelectedBackground = Color.Blue;
         crazyTheme.MenuItem.DisabledSelectedForeground = Color.Blue;
         crazyTheme.MenuItem.DisabledSelectedBackground = Color.DarkGray;
         crazyTheme.Expander.Foreground = Color.Magenta;
         crazyTheme.Expander.Background = Color.Green;
         crazyTheme.HeaderForeground = Color.Black;
         crazyTheme.HeaderBackground = Color.Yellow;

         var chooseDefaultTheme = new ConsoleMenuItem("Default", m => m.Menu.Theme = new MenuColorTheme());
         var chooseBlueTheme = new ConsoleMenuItem("Blue", m => m.Menu.Theme = ConsoleMenuThemes.Blue);
         var chooseRedTheme = new ConsoleMenuItem("Red", m => m.Menu.Theme = ConsoleMenuThemes.Red);
         var choosePinkTheme = new ConsoleMenuItem("Pink", m => m.Menu.Theme = ConsoleMenuThemes.Pink);
         var chooseCrazyTheme = new ConsoleMenuItem("Crazy", m => m.Menu.Theme = crazyTheme);
         return new ConsoleMenuItem(
            "Choose color theme",
            chooseBlueTheme,
            chooseRedTheme,
            choosePinkTheme,
            chooseCrazyTheme,
            chooseDefaultTheme,
            new ConsoleMenuItem("Bahama", m => m.Menu.Theme = ConsoleMenuThemes.Bahama),
            new ConsoleMenuItem("CloseOptions", new ConsoleMenuItem("Exit application but use a long long name", x => Environment.Exit(0))),
            new ConsoleMenuItem("A disabled menu ittem using a long name"));
      }

      private static ConsoleMenuItem CreateSelectionStrechMenu()
      {
         return new ConsoleMenuItem(
            "Change selection strech",
            new ConsoleMenuItem("None", x => x.Menu.SelectionStrech = SelectionStrech.None),
            new ConsoleMenuItem("UnifiedLength", x => x.Menu.SelectionStrech = SelectionStrech.UnifiedLength),
            new ConsoleMenuItem("FullLine", x => x.Menu.SelectionStrech = SelectionStrech.FullLine));
      }

      private static ConsoleMenuItem CreateSelectorMenu(ConsoleMenu menu)
      {
         return new ConsoleMenuItem(
            "Change selector",
            new ConsoleMenuItem("None", x => x.Menu.Selector = string.Empty),
            new ConsoleMenuItem("Arrow ( => )", x => x.Menu.Selector = "=>"),
            new ConsoleMenuItem("Star ( * )", x => x.Menu.Selector = "*"),
            new ConsoleMenuItem("Big Double ( >> )", x => x.Menu.Selector = ">>"),
            new ConsoleMenuItem("Small Double ( » )", x => x.Menu.Selector = "»"),
            new ConsoleMenuItem(
               "Enter custom selector",
               x =>
               {
                  Console.WriteLine("Enter selector");
                  menu.Selector = Console.ReadLine();
               }));
      }

      private static ConsoleMenuItem CreatExecuteOnIndexSelectionMenu(ConsoleMenu menu)
      {
         return new ConsoleMenuItem(
            $"ExecuteOnIndexSelection = {menu.ExecuteOnIndexSelection}",
            x =>
            {
               x.Menu.ExecuteOnIndexSelection = !x.Menu.ExecuteOnIndexSelection;
               x.Text = $"ExecuteOnIndexSelection = {x.Menu.ExecuteOnIndexSelection}";
            });
      }

      private static ConsoleMenuItem CreatIndexMenuItemsMenu(bool initialValue)
      {
         return new ConsoleMenuItem(
            $"IndexMenuItems = {initialValue}",
            x =>
            {
               x.Menu.IndexMenuItems = !x.Menu.IndexMenuItems;
               x.Text = $"IndexMenuItems = {x.Menu.IndexMenuItems}";
            });
      }

      private static void DoCrash(ConsoleMenuItem sender)
      {
         throw new InvalidOperationException("Some invalid operartion was performed");
      }

      private static void HandleCrash(ConsoleMenu menu)
      {
         menu.ExecutionError += OnError;
      }

      private static void InsertName(ConsoleMenuItem sender)
      {
         Console.WriteLine("Enter the user name");
         userName = Console.ReadLine();
      }

      private static IEnumerable<ConsoleMenuItem> LazyLoadChildren()
      {
         yield return new ConsoleMenuItem("Child 1", x => { });
         Thread.Sleep(500);
         yield return new ConsoleMenuItem("Child 2", x => { });
         Thread.Sleep(500);
         yield return new ConsoleMenuItem("Child 3", x => { });
         Thread.Sleep(500);
         yield return new ConsoleMenuItem("Child 4", x => { });
         Thread.Sleep(400);
         yield return new ConsoleMenuItem("Child 5", x => { });
      }

      static void Main(string[] args)
      {
         // ConsoleWindow.HideMinimizeAndMaximizeButtons();
         ConsoleWindow.DisableMinimize();
         ConsoleWindow.DisableMaximize();


         ////ShowArgs(args);
         ////ShowArgs(new CommandLineArgumentParser().NormalizeArguments(args));

         Console.CursorSize = 4;
         Console.WindowHeight = 40;
         string header = @"    ___                     _        __ __                  ___           _                     
   |  _> ___ ._ _  ___ ___ | | ___  |  \  \ ___ ._ _  _ _  | __>__   ___ | | ___  _ _  ___  _ _ 
   | <__/ . \| ' |<_-</ . \| |/ ._> |     |/ ._>| ' || | | | _> \ \/| . \| |/ . \| '_>/ ._>| '_>
   `___/\___/|_|_|/__/\___/|_|\___. |_|_|_|\___.|_|_|`___| |___>/\_\|  _/|_|\___/|_|  \___.|_|  
                                                                    |_|                         ";

         var footer = Environment.NewLine + "THIS COULD BE YOUR FOOTER";



         var menu = new ConsoleMenu { Header = header, Footer = footer, CircularSelection = false, Selector = "» ", Theme = ConsoleMenuThemes.Bahama };

         menu.SelectionStrech = SelectionStrech.UnifiedLength;
         // menu.Expander = new ExpanderDescription { Collapsed = "►", Expanded = "▼" };
         menu.Add(CreateColorMenu());
         menu.Add(CreateSelectionStrechMenu());
         menu.Add(CreatCircularSelectionMenu(menu));
         menu.Add(CreatIndexMenuItemsMenu(menu.IndexMenuItems));
         menu.Add(CreatClearOnExecutionMenu(menu.ClearOnExecution));
         menu.Add(CreateSelectorMenu(menu));
         menu.Add(CreatExecuteOnIndexSelectionMenu(menu));
         menu.Add(new ConsoleMenuItem("Disabled without command"));
         menu.Add(
            new ConsoleMenuItem(
               "Remove until 9 remain",
               x =>
               {
                  while (menu.Count >= 10)
                     menu.RemoveAt(menu.Count - 1);
               }));
         menu.Add(new ConsoleMenuItem("Show Progress", ShowProgress));
         menu.Add(new ConsoleMenuItem("Set user name", InsertName));
         menu.Add(new ConsoleMenuItem("Connect to server", ConnectToServer, CanConnectToServer) { DisabledHint = "Set username first" });
         menu.Add(new ConsoleMenuItem("Register crash event handler", x => HandleCrash(menu)));
         menu.Add(new ConsoleMenuItem("Simulate Crash", DoCrash));
         menu.Add(new ConsoleMenuItem("LazyLoadChildren", LazyLoadChildren, true));
         menu.Add(new ConsoleMenuItem("Close menu", x => menu.Close()));
         menu.Add(new ConsoleMenuItem("Exit", x => Environment.Exit(0)));
         menu.Show();
      }

      private static void ShowArgs(IEnumerable<string> args)
      {
         Console.WriteLine("Arguments:");
         foreach (var arg in args)
         {
            Console.WriteLine("   {0}", arg);
         }

         Console.ReadLine();
      }

      private static void OnError(object sender, ExceptionEventArgs e)
      {
         Console.WriteLine(e.Exception.Message);
         e.Handled = true;
      }

      private static void ShowProgress(ConsoleMenuItem sender)
      {
         var progressBar = new ProgressBar(100, "Some long running process", ConsoleColor.DarkYellow);
         for (int i = 0; i < 100; i++)
         {
            progressBar.Tick();
            Thread.Sleep(50);
         }
      }

      #endregion
   }
}