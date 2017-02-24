namespace InputBoxDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit;

   class Program
   {
      static void Main()
      {
         int inty = new InputBox("Enter an integer: ").ReadLine<int>();
         Console.WriteLine(inty);

         var text = new InputBox("Enter some long text: ").ReadLine();
         Console.WriteLine(text);

         ConsoleColor background = ConsoleColor.White;
         ConsoleColor foreground = ConsoleColor.Red;

         while (true)
         {
            var inputBox = new InputBox
            {
               Label = new InputLabel("Enter some text: ")
               {
                  Foreground = ConsoleColor.Blue,
                  Background = ConsoleColor.Gray
               },
               InitialValue = GetDefaultValue(),
               Foreground = foreground,
               Background = background
            };

            var input = inputBox.ReadLine(26, '.');
            var colors = input.Split(' ');

            if (Enum.TryParse(colors[0], out background))
            {
               if (colors.Length > 1)
                  Enum.TryParse(colors[1], out foreground);

               continue;
            }

            if (input == "exit")
               return;

            Console.WriteLine(input);
         }
      }

      private static string GetDefaultValue()
      {
         var colorNames = Enum.GetNames(typeof(ConsoleColor));
         var random = new Random(DateTime.Now.Millisecond);
         var backgound = random.Next(0, colorNames.Length - 1);
         var foreground = random.Next(0, colorNames.Length - 1);

         return colorNames[backgound] + " " + colorNames[foreground];
      }
   }
}
