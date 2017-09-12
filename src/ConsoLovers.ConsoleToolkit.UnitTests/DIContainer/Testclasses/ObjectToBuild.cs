// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectToBuild.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.DIContainer.Testclasses
{
   using System;

   using ConsoLovers.ConsoleToolkit.DIContainer;

   /// <summary>Testclass for container tests</summary>
   public class ObjectToBuild
   {
      #region Constants and Fields

      private IDemo noSetterDemo;

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the container.</summary>
      [Dependency]
      public IContainer Container { get; set; }

      /// <summary>Gets or sets the demo.</summary>
      [Dependency]
      public IDemo Demo { get; set; }

      /// <summary>Gets the no setter demo.</summary>
      public IDemo NoSetterDemo
      {
         get
         {
            return noSetterDemo;
         }
      }

      /// <summary>Gets the private demo.</summary>
      [Dependency]
      public IDemo PrivateDemo { get; private set; }

      /// <summary>Gets or sets the protected demo.</summary>
      [Dependency]
      public IDemo ProtectedDemo { get; protected set; }

      /// <summary>Gets or sets the service provider.</summary>
      [Dependency]
      public IServiceProvider ServiceProvider { get; set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Sets the properties.</summary>
      public void SetProperties()
      {
         PrivateDemo = ProtectedDemo = noSetterDemo = Demo;
      }

      #endregion
   }
}