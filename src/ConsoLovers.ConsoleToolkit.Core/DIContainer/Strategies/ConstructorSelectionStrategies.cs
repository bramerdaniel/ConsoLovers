// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstructorSelectionStrategies.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies
{
   public class ConstructorSelectionStrategies
   {
      #region Constants and Fields

      private static readonly CombinedSelectionStrategy withCombinedLogic = new CombinedSelectionStrategy();

      private static readonly AttributSelectionStrategy withInjectionConstructorAttribute = new AttributSelectionStrategy();

      private static readonly MostParametersSelectionStrategy withMostParameters = new MostParametersSelectionStrategy();

      #endregion

      #region Public Properties

      /// <summary>Gets a strategy that combines <see cref="AttributSelectionStrategy"/> with the <see cref="MostParametersSelectionStrategy"/>.</summary>
      public static CombinedSelectionStrategy WithCombinedLogic
      {
         get
         {
            return withCombinedLogic;
         }
      }

      /// <summary>Gets a strategy for selection the constructor with the most parameters.</summary>
      public static AttributSelectionStrategy WithInjectionConstructorAttribute
      {
         get
         {
            return withInjectionConstructorAttribute;
         }
      }

      /// <summary>Gets a strategy for selection the constructor with the most parameters.</summary>
      public static MostParametersSelectionStrategy WithMostParameters
      {
         get
         {
            return withMostParameters;
         }
      }

      #endregion
   }
}