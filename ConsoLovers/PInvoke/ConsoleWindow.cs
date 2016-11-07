namespace ConsoLovers.ConsoleToolkit.PInvoke
{
   using System;

   public class ConsoleWindow
   {
      public static void HideMinimizeAndMaximizeButtons()
      {
         const int GWL_STYLE = -16;

         IntPtr hwnd = NativeMethods.GetConsoleWindow();
         long value = NativeMethods.GetWindowLong(hwnd, GWL_STYLE);

         var button = -0x20001;
         NativeMethods.SetWindowLong(hwnd, GWL_STYLE, (int)(value & button & -65537));

      }

      public static void DisableMaximize()
      {
         const int GWL_STYLE = -16;

         IntPtr hwnd = NativeMethods.GetConsoleWindow();
         long value = NativeMethods.GetWindowLong(hwnd, GWL_STYLE);

         NativeMethods.SetWindowLong(hwnd, GWL_STYLE, (int)(value  & -65537));
      }

      public static void DisableMinimize()
      {
         const int GWL_STYLE = -16;

         IntPtr hwnd = NativeMethods.GetConsoleWindow();
         long value = NativeMethods.GetWindowLong(hwnd, GWL_STYLE);

         NativeMethods.SetWindowLong(hwnd, GWL_STYLE, (int)(value & -0x20001));
      }

      public static void DisableCloseButton()
      {
         NativeMethods.DeleteMenu(NativeMethods.GetSystemMenu(NativeMethods.GetConsoleWindow(), false), NativeMethods.SC_CLOSE, NativeMethods.MF_GRAYED);
      }

      public static void DisableMaximizeButton()
      {
         NativeMethods.DeleteMenu(NativeMethods.GetSystemMenu(NativeMethods.GetConsoleWindow(), false), NativeMethods.SC_MAXIMIZE, NativeMethods.MF_ENABLED);
      }
      public static void DisableMinimizeButton()
      {
         NativeMethods.DeleteMenu(NativeMethods.GetSystemMenu(NativeMethods.GetConsoleWindow(), false), NativeMethods.SC_MINIMIZE, NativeMethods.MF_GRAYED);
      }
   }
}