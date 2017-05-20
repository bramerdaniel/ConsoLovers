// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InjectionConstructorAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;

   /// <summary>Attribute for the constructor that should be used, if an <see cref="IContainer"/> creates the instance.</summary>
   [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = true)]
   public class InjectionConstructorAttribute : Attribute
   {
   }
}