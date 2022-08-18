// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMock.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System.Diagnostics;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

/// <summary>unit test helper command</summary>
/// <typeparam name="TArgs">The type of the arguments.</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ICommand&lt;TArgs&gt;"/>
[DebuggerDisplay("{ComputeName()}")]
public class CommandMock<TArgs> : ICommand<TArgs>
{
   #region ICommand<TArgs> Members

   public void Execute()
   {
      WasExecute = true;
   }

   private string ComputeName()
   {
      var name = typeof(TArgs).Name;
      return name.Replace("Args", "Command")
         .Replace("Arguments", "Command");
   }

   public TArgs Arguments { get; set; }

   #endregion

   #region Public Properties

   public bool WasExecute { get; private set; }

   #endregion
}