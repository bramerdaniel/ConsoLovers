// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PromptsLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PromptsDemo;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Prompts;

internal class PromptsLogic : IApplicationLogic<PromptsDemoArgs>
{
   private readonly IRenderEngine renderEngine;

   private readonly IConsole console;

   public PromptsLogic(IRenderEngine renderEngine, IConsole console)
   {
      this.renderEngine = renderEngine ?? throw new ArgumentNullException(nameof(renderEngine));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public Task ExecuteAsync(PromptsDemoArgs arguments, CancellationToken cancellationToken)
   {
      var line = new Line();
      line.Add(new Text("Hello ", new RenderingStyle(ConsoleColor.Red, ConsoleColor.Green))
      {
         MinWidth = 10, Alignment = Alignment.Right
      });
      line.Add(new Text("World "));

      renderEngine.Render(line);
      console.ReadLine();
      return Task.CompletedTask;
      
   }
}