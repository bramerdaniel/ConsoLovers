namespace ConsoLovers.ConsoleToolkit.Core.Exceptions
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