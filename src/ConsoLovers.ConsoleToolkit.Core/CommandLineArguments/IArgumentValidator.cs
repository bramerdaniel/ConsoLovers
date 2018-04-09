// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentValidator.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   public interface IArgumentValidator<T> 
   {
      void Validate(T value);
   }
}