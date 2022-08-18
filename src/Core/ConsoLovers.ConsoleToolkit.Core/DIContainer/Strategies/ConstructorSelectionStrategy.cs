namespace ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies
{
   using System;
   using System.Reflection;

   /// <summary>Class responsible for selecting the constructor that is used to build a type.</summary>
   public abstract class ConstructorSelectionStrategy
   {
      /// <summary>Selects the costructor.</summary>
      /// <param name="type">The type.</param>
      /// <returns>The selected <see cref="ConstructorInfo"/> or null of no constructor matched the stategies selection conditions.</returns>
      public abstract ConstructorInfo SelectCostructor(Type type);
   }
}