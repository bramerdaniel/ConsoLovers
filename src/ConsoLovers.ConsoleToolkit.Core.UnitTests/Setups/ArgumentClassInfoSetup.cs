// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentClassInfoSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ArgumentClassInfoSetup : SetupBase<ArgumentClassInfo>
{
   #region Properties

   private Type ArgumentType { get; set; }

   #endregion

   #region Public Methods and Operators

   public ArgumentClassInfoSetup FromType(Type type)
   {
      ArgumentType = type;
      return this;
   }

   #endregion

   #region Methods

   protected override ArgumentClassInfo CreateInstance()
   {
      return ArgumentClassInfo.FromType(ArgumentType);
   }

   #endregion
}