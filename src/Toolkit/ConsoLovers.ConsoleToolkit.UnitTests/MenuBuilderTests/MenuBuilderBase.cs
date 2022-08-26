// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBuilderBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.MenuBuilderTests;

using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

public class MenuBuilderBase
{
   #region Methods

   internal IMenuNode[] BuildMenu<T>(IMenuBuilderOptions options)
      where T : class
   {
      var target = Setup.MenuBuilder()
         .WithOptions(options)
         .Done();

      var nodes = target.Build<T>().ToArray();
      return nodes;
   }

   #endregion

   public class CommandsOnly
   {
      #region Public Properties

      [Command("run")]
      public Command<RunArgs> Run { get; set; }

      [Command("exit")]
      public Command<ExitArgs> Exit { get; set; }

      #endregion

      public class RunArgs
      {
         #region Public Properties

         [Argument("name")]
         public string Name { get; set; }

         [Argument("count")]
         public int Count{ get; set; }


         #endregion
      }
      public class ExitArgs
      {
         #region Public Properties

         [Argument("force")]
         public bool Force { get; set; }

         #endregion
      }
   }

   protected class MenuCommandsOnly
   {
      #region Public Properties

      [MenuCommand("Run it")]
      public Command<RunArgs> Run { get; set; }

      [MenuCommand("Close it")]
      public Command<CloseArgs> Close { get; set; }

      #endregion

      public class RunArgs
      {
         #region Public Properties

         [Argument("count")]
         public int Count { get; set; }

         [Argument("name")]
         public string Name { get; set; }

         #endregion
      }

      public class CloseArgs
      {
         #region Public Properties

         [Argument("force")]
         public bool Force{ get; set; }

         [Argument("exitCode")]
         public int ExitCode{ get; set; }
         
         [Argument("message")]
         public string Message { get; set; }

         #endregion
      }
   }
}