namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses
{
   using ConsoLovers.ConsoleToolkit.Core.DIContainer;

   /// <summary>Testclass with no attributes on its methods, properties, and constructors</summary>
   internal class MultipleConstructorAttributes
   {
      private readonly bool enabled;

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="MultipleConstructorAttributes"/> class.</summary>
      [InjectionConstructor]
      public MultipleConstructorAttributes()
      {
         Id = 0;
      }

      /// <summary>Initializes a new instance of the <see cref="MultipleConstructorAttributes"/> class.</summary>
      /// <param name="id">The name. </param>
      public MultipleConstructorAttributes(int id)
      {
         Id = 1;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="MultipleConstructorAttributes"/> class.
      /// </summary>
      /// <param name="id">The id.</param>
      /// <param name="name">The name.</param>
      [InjectionConstructor]
      public MultipleConstructorAttributes(int id, string name)
      {
         Id = 2;
         Name = name;
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="MultipleConstructorAttributes"/> class.
      /// </summary>
      /// <param name="id">The id.</param>
      /// <param name="name">The name.</param>
      /// <param name="enabled">if set to <c>true</c> [enabled].</param>
      public MultipleConstructorAttributes(int id, string name, bool enabled)
      {
         this.enabled = enabled;
         Id = 3;
         Name = name;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the id.</summary>
      public int Id { get; private set; }

      public string Name { get; set; }

      #endregion
   }
}