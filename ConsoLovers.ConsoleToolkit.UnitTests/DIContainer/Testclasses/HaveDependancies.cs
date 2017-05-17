namespace ConsoLovers.UnitTests.DIContainer.Testclasses
{
   public class HaveDependancies : IHaveDependencies
   {
      private readonly IDemo demo;

      /// <summary>Initializes a new instance of the <see cref="HaveDependancies"/> class.</summary>
      /// <param name="demo">The demo.</param>
      public HaveDependancies(IDemo demo)
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