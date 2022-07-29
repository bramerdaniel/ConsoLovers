// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedCommandTests.Types.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.IntegrationTests;

using System.Diagnostics;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public partial class EngineIntegrationTests
{
   [DebuggerDisplay("DeletePermissionArgs")]
   [UsedImplicitly]
   internal class DeletePermissionArgs
   {
      #region Public Properties

      [Argument("permission", Index = 0)] 
      public string Permission { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("DeleteRoleArgs")]
   [UsedImplicitly]
   internal class DeleteRoleArgs
   {
      #region Public Properties

      [Argument("name")] 
      public string RoleName { get; [UsedImplicitly] set; }

      [Option("force")]
      [UsedImplicitly]
      public bool Force { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("DeleteUserArgs")]
   [UsedImplicitly]
   internal class DeleteUserArgs
   {
      #region Public Properties

      [Argument("name")] public string UserName { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("DeleteArgs")]
   [UsedImplicitly]
   class DeleteArgs
   {
      #region Public Properties

      [Command("permission")]
      [UsedImplicitly]
      public CommandMock<DeletePermissionArgs> Permission { get; set; }

      [Command("role")]
      [UsedImplicitly]
      public CommandMock<DeleteRoleArgs> Role { get; set; }

      [Command("user")]
      [UsedImplicitly]
      public CommandMock<DeleteUserArgs> User { get; set; }

      #endregion
   }

   [DebuggerDisplay("RootArgs")]
   [UsedImplicitly]
   class RootArgs
   {
      #region Public Properties

      [Command("delete")]
      [UsedImplicitly]
      public CommandMock<DeleteArgs> Delete { get; [UsedImplicitly] set; }

      [Command("add")]
      [UsedImplicitly]
      public CommandMock<AddArgs> Add { get; [UsedImplicitly] set; }
      
      [Option("force", Shared = true)]
      [UsedImplicitly]
      public bool Force { get; [UsedImplicitly] set; }



      #endregion
   }

   [DebuggerDisplay("AddArgs")]
   [UsedImplicitly]
   private class AddArgs
   {
      #region Public Properties

      [Command("permission")]
      [UsedImplicitly]
      public CommandMock<AddPermissionArgs> Permission { get; set; }

      [Command("role")]
      [UsedImplicitly]
      public CommandMock<AddRoleArgs> Role { get; set; }

      [Command("user")]
      [UsedImplicitly]
      public CommandMock<AddUserArgs> User { get; set; }

      #endregion
   }

   [DebuggerDisplay("AddPermissionArgs")]
   [UsedImplicitly]
   internal class AddPermissionArgs
   {
      #region Public Properties

      [Argument("permission")]
      [UsedImplicitly]
      public string Permission { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("AddRoleArgs")]
   [UsedImplicitly]
   internal class AddRoleArgs
   {
      #region Public Properties

      [Argument("name")]
      [UsedImplicitly]
      public string RoleName { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("AddUserArgs")]
   [UsedImplicitly]
   internal class AddUserArgs
   {
      #region Public Properties

      [Argument("name", Index = 0)]
      [UsedImplicitly] 
      public string UserName { get; [UsedImplicitly] set; }

      #endregion
   }
}