// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputHandlerFactory.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler;

using System.Runtime.InteropServices;

public sealed class InputHandlerFactory
{
   private static IInputHandler handler;

   public static IInputHandler GetInputHandler()
   {
      if (handler == null)
      {
         handler = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) 
            ? new WindowsConsoleInputHandler() 
            : new LinuxConsoleInputHandler();
      }
      
      return handler;
   }
}