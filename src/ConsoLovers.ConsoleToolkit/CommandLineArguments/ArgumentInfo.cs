namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>Helper class providing information about a command line parameter,
   /// that was decorated with the <see cref="ArgumentAttribute"/></summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.CommandLineArguments.ParameterInfo" />
   public class ArgumentInfo : ParameterInfo
   {
      #region Constructors and Destructors

      internal ArgumentInfo([NotNull] PropertyInfo propertyInfo, [NotNull] ArgumentAttribute commandLineAttribute)
         : base(propertyInfo, commandLineAttribute)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the attribute.</summary>
      public ArgumentAttribute Attribute => (ArgumentAttribute)CommandLineAttribute;

      #endregion
   }
}