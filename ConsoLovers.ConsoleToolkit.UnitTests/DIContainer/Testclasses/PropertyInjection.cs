namespace ConsoLovers.UnitTests.DIContainer.Testclasses
{
   using ConsoLovers.ConsoleToolkit.DIContainer;

   internal class PropertyInjection 
   {
      /// <summary>Gets or sets the attribute.</summary>
      [Dependency]
      public IDemo Attribute { get; set; }   
      
      /// <summary>Gets or sets the attribute.</summary>
      [Dependency]
      public IDemo PrivateAttribute { get; private set; }

      /// <summary>Gets or sets the no attribute.</summary>
      public IDemo NoAttribute { get; set; }
   }
}