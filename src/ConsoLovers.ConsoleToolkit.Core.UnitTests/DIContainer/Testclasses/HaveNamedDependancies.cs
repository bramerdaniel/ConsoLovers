namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses
{
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   public class HaveNamedDependancies : IHaveDependencies
   {
      private readonly IDemo demo;

      /// <summary>Initializes a new instance of the <see cref="HaveDependancies"/> class.</summary>
      /// <param name="demo">The demo.</param>
      public HaveNamedDependancies([Dependency(Name = "Name")] IDemo demo)
      {
         this.demo = demo;
      }

      /// <summary>Gets the demo.</summary>
      public IDemo Demo
      {
         get { return demo; }
      }
   }
}