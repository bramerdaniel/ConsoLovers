// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuCommandManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System.Threading;
   using System.Threading.Tasks;

   /// <summary>Service that can show a consolovers menu from the defined command structure </summary>
   public interface IMenuCommandManager
   {
      #region Public Methods and Operators

      void Show<T>(CancellationToken cancellationToken);

      Task ShowAsync<T>(CancellationToken cancellationToken);

      void UseOptions(ICommandMenuOptions options);

      #endregion
   }
}