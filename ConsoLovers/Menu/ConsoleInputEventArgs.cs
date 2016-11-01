namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   internal class ConsoleInputEventArgs : EventArgs
   {
      public ConsoleKeyInfo KeyInfo { get; set; }

      public string Input { get; set; }

      public ConsoleInputEventArgs(ConsoleKeyInfo keyInfo)
         :this(keyInfo, keyInfo.KeyChar.ToString())
      {
      }

      public ConsoleInputEventArgs(ConsoleKeyInfo keyInfo , string input)
      {
         KeyInfo = keyInfo;
         Input = input;
      }
   }
}