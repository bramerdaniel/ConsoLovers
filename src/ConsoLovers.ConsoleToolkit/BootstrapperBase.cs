﻿namespace ConsoLovers.ConsoleToolkit
{
   /// <summary>Base class for the bootstrappers</summary>
   internal class BootstrapperBase
   {
      /// <summary>Gets or sets the height of the window.</summary>
      protected int? WindowHeight { get; set; }

      /// <summary>Gets or sets the window title.</summary>
      protected string WindowTitle { get; set; }

      /// <summary>Gets or sets the width of the window.</summary>
      protected int? WindowWidth { get; set; }
   }
}