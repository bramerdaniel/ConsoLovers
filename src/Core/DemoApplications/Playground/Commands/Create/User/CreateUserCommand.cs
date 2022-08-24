// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateUserCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;

public class CreateUserCommand : ICommand<CreateUserArgs>
{
   public void Execute()
   {
      Console.WriteLine($"Create user {Arguments.UserName}");
   }

   public CreateUserArgs Arguments { get; set; } = null!;
}