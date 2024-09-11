// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLinerParserSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;

   public class CommandLinerParserSetup : Setups.SetupBase<CommandLineArgumentParser>
   {
      #region Methods

      protected override CommandLineArgumentParser CreateInstance()
      {
         var parser = new CommandLineArgumentParser();
         if (Options != null)
            parser.Options = Options;

         return parser;
      }
      
      #endregion

      public CommandLinerParserSetup WithOptions(ICommandLineOptions options)
      {
         Options = options;
         return this;
      }

      protected ICommandLineOptions Options { get; set; }
   }
}