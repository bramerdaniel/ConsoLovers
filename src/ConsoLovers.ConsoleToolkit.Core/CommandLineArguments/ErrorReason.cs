namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
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