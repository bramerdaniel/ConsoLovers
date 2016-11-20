// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IColoredConsole.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Contracts
{
   using System;
   using System.Drawing;

   /// <summary>ColoredConsole abstraction that adds some methods for easy colorizing the console</summary>
   /// <seealso cref="IConsole"/>
   public interface IColoredConsole : IConsole
   {
      #region Public Methods and Operators

      /// <summary>Clears the console with the given <see cref="ConsoleColor"/>.</summary>
      /// <param name="clearingColor">The <see cref="ConsoleColor"/> to clear the console with.</param>
      void Clear(Color clearingColor);

      void WriteLine(bool value, Color color);

      void WriteLine(char value, Color color);

      void WriteLine(char[] value, Color color);

      void WriteLine(decimal value, Color color);

      void WriteLine(double value, Color color);

      void WriteLine(float value, Color color);

      void WriteLine(int value, Color color);

      void WriteLine(long value, Color color);

      void WriteLine(object value, Color color);

      void WriteLine(string value, Color color);

      void WriteLine(uint value, Color color);

      void WriteLine(ulong value, Color color);

      void WriteLine(string format, object arg0, Color color);

      void WriteLine(string format, Color color, params object[] args);

      void WriteLine(char[] buffer, int index, int count, Color color);

      void WriteLine(string format, object arg0, object arg1, Color color);

      void WriteLine(string format, object arg0, object arg1, object arg2, Color color);

      void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color);

      #endregion
   }
}