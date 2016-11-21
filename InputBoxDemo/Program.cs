namespace InputBoxDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit;

   class Program : ConsoleApplication
   {
      static void Main(string[] args)
      {
         var text = new InputBox("Enter some long text: ").ReadLine();
         Console.WriteLine(text);

         ConsoleColor background = ConsoleColor.White;

         while (true)
         {
            var inputBox = new InputBox
            {
               Label = new InputLabel("Enter some text: ")
               {
                  Foreground = ConsoleColor.Blue, Background = ConsoleColor.Gray
               },
               InitialValue = "DefaultValue",
               Foreground = ConsoleColor.Red,
               Background = background
            };

            var input = inputBox.ReadLine(20, '.');

            if (Enum.TryParse(input, out background))
               continue;

            if (input == "exit")
               return;

            Console.WriteLine(input);
         }
      }
   }
}
