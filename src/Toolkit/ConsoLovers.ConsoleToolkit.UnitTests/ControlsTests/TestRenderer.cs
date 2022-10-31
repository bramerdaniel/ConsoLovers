// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

using System.Text;

using ConsoLovers.ConsoleToolkit.Controls;

public class TestRenderer
{
   #region Constants and Fields

   private readonly StringBuilderConsole console;

   private readonly StringBuilder contentBuilder;

   #endregion

   #region Constructors and Destructors

   public TestRenderer()
   {
      contentBuilder = new StringBuilder();
      console = new StringBuilderConsole(contentBuilder);
   }

   #endregion

   #region Public Properties

   public int ConsoleWidth
   {
      get => console.WindowWidth;
      set => console.WindowWidth = value;
   }

   #endregion

   #region Public Methods and Operators

   public string Render(IRenderable renderable)
   {
      var engine = new RenderEngine(console);
      engine.Render(renderable);
      return contentBuilder.ToString().TrimEnd('\r', '\n');
   }

   public void Reset()
   {
      contentBuilder.Clear();
   }

   #endregion
}