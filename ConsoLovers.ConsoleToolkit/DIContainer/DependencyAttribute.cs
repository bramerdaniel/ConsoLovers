// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencyAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;

   /// <summary>Attribute for a property that should be injected by the container</summary>
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
   public class DependencyAttribute : Attribute
   {
   }
}