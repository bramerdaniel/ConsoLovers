namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   internal enum ErrorReason
   {
      Unknown,
      ArgumentWithoutValue,
      OptionWithValue,

      NoValidatorImplementation,

      InvalidValidatorImplementation

   }
}