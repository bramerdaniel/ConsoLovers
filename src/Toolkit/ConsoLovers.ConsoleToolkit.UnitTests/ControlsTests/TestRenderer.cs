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
   private readonly StringBuilder contentBuilder;

   private readonly StringBuilderConsole console;

   public TestRenderer()
   {
      contentBuilder = new StringBuilder();
      console = new StringBuilderConsole(contentBuilder);
   }

   public int ConsoleWidth
   {
      get => console.WindowWidth;
      set => console.WindowWidth = value;
   }

   public string Render(IRenderable renderable)
   {
      var engine = new RenderEngine(console);
      engine.Render(renderable);
      return contentBuilder.ToString().TrimEnd('\r','\n');

   }
}