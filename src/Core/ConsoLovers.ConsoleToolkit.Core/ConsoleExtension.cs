// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleExtension.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Linq;

using JetBrains.Annotations;

public static class ConsoleExtension
{
   #region Public Methods and Operators

   public static ConsoleKey WaitForEscapeOrNewline([NotNull] this IConsole console)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return console.WaitForKey(ConsoleKey.Escape, ConsoleKey.Enter);
   }

   public static ConsoleKey WaitForEscapeOrNewline([NotNull] this IConsole console, string waitMessage)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      if (waitMessage != null)
         console.WriteLine(waitMessage);

      return console.WaitForKey(ConsoleKey.Escape, ConsoleKey.Enter);
   }

   public static ConsoleKey WaitForEscapeOrNewline()
   {
      return new ConsoleProxy().WaitForEscapeOrNewline();
   }

   public static ConsoleKey WaitForEscapeOrNewline(string waitMessage)
   {
      return new ConsoleProxy().WaitForEscapeOrNewline(waitMessage);
   }

   public static ConsoleKey WaitForKey(this IConsole console, [NotNull] params ConsoleKey[] keys)
   {
      if (keys == null)
         throw new ArgumentNullException(nameof(keys));

      while (true)
      {
         var keyInfo = console.ReadKey();
         if (keys.Contains(keyInfo.Key))
            return keyInfo.Key;
      }
   }

   public static ConsoleKey WaitForKey([NotNull] params ConsoleKey[] keys)
   {
      return new ConsoleProxy().WaitForKey(keys);
   }

   #endregion
}