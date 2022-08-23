// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteFileArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.Files;

using ConsoLovers.ConsoleToolkit.Core;

public class DeleteFileArgs
{
   [Argument("files", Index = 0)]
   public string[] Files { get; set; } = null!;
}