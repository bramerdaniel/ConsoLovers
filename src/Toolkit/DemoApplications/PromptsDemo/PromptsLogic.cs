// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromptsLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PromptsDemo;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.Core;

internal class PromptsLogic : IApplicationLogic<PromptsDemoArgs>
{
   #region Constants and Fields

   private readonly RenderingStyle blue;

   private readonly IConsole console;

   private readonly RenderingStyle red;

   private readonly IRenderEngine renderEngine;

   #endregion

   #region Constructors and Destructors

   public PromptsLogic(IRenderEngine renderEngine, IConsole console)
   {
      this.renderEngine = renderEngine ?? throw new ArgumentNullException(nameof(renderEngine));
      this.console = console ?? throw new ArgumentNullException(nameof(console));

      red = new RenderingStyle(ConsoleColor.Red);
      blue = new RenderingStyle(ConsoleColor.Blue);
   }

   #endregion

   #region IApplicationLogic<PromptsDemoArgs> Members

   public Task ExecuteAsync(PromptsDemoArgs arguments, CancellationToken cancellationToken)
   {

      var text = new Text("Hello" + Environment.NewLine + "World");
      var panel = new Border(text) { Padding = new Thickness(2) };
      renderEngine.Render(panel);

      console.ReadLine();
      return Task.CompletedTask;
   }

   private void Old()
   {
      renderEngine.Render(new Rule("Hello World") { Style = blue, RuleCharacter = '*' });
      renderEngine.Render(new Rule("Hello World") { Style = red, TextAlignment = Alignment.Right, TextOffset = 20 });
      renderEngine.Render(new Rule("Hello World") { TextAlignment = Alignment.Center });

      var text = new Text("Hello World");
      var panel = new Border(text) { Padding = new Thickness(3) };
      renderEngine.Render(panel);

      text = new Text("Hello World");
      panel = new Border(text) { Alignment = Alignment.Right };
      renderEngine.Render(panel);

      text = new Text("Hello World");
      panel = new Border(text) { Alignment = Alignment.Left };
      renderEngine.Render(panel);

      text = new Text("Hello World");
      panel = new Border(text) { Alignment = Alignment.Center, Padding = new Thickness(3, 0, 10, 0) };
      renderEngine.Render(panel);

      panel = new Border(panel) { Alignment = Alignment.Center, Style = red };
      renderEngine.Render(panel);

      panel = new Border(new Rule("Hello World") { MaxWidth = 60, TextAlignment = Alignment.Center });
      renderEngine.Render(panel);
      renderEngine.Render(new Rule("Hello World") { MaxWidth = 60, TextAlignment = Alignment.Center });
   }

   #endregion

   #region Methods


   #endregion
}