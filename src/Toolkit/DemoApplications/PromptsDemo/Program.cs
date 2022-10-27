// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PromptsDemo
{
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Prompts;

   using Microsoft.Extensions.DependencyInjection;

   public static class Program
   {
      #region Methods

      private static void Main()
      {
         ConsoleApplication.WithArguments<PromptsDemoArgs>()
            .UseApplicationLogic(typeof(PromptsLogic))
            .AddService(s => s.AddSingleton<IRenderEngine, RenderEngine>())
            .Run();
      }

      #endregion
   }
}