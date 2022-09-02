// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetupBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups
{
   using System;
   using System.Collections.Generic;

   /// <summary>Base class for setting up a test object using a fluent dependency registration</summary>
   /// <typeparam name="T"></typeparam>
   public abstract class SetupBase<T>
      where T : class
   {
      #region Constants and Fields

      private readonly Dictionary<string, object> fields = new Dictionary<string, object>();

      #endregion

      #region Methods

      internal T Done()
      {
         var instance = CreateInstance();
         SetupInstance(instance);
         return instance;
      }

      protected bool Add<FT>(string name, FT value)
      {
         if (!fields.TryGetValue(name, out var list))
         {
            Set<IList<FT>>(name, new List<FT> { value });
            return true;
         }

         if (list is IList<FT> typedList)
         {
            typedList.Add(value);
            return true;
         }

         return false;
      }

      protected bool Contains<FT>(string name)
      {
         if (fields.TryGetValue(name, out var untypedValue))
         {
            try
            {
               // ReSharper disable once UnusedVariable
               var value = (FT)untypedValue;
               return true;
            }
            catch (Exception)
            {
               return false;
            }
         }

         return false;
      }

      /// <summary>Creates the instance of the setup object.// </summary>
      /// <returns>The created instance</returns>
      protected abstract T CreateInstance();

      protected FT Get<FT>(string name)
      {
         if (fields.TryGetValue(name, out var value))
            return (FT)value;

         throw new InvalidOperationException($"A field with the name {name} was not specified");
      }

      protected FT Get<FT>(string name, Func<FT> defaultValue)
      {
         if (fields.TryGetValue(name, out var value))
            return (FT)value;

         return defaultValue();
      }

      protected FT Get<FT>(string name, FT defaultValue)
      {
         if (fields.TryGetValue(name, out var value))
            return (FT)value;

         return defaultValue;
      }

      protected IList<FT> GetMany<FT>(string name, Func<IList<FT>> defaultValue)
      {
         if (fields.TryGetValue(name, out var value))
            return value as IList<FT>;

         if (defaultValue != null)
            return defaultValue();

         throw new InvalidOperationException($"A field with the name {name} was not specified");
      }

      protected void Remove(string name)
      {
         fields.Remove(name);
      }

      protected void Set<FT>(string name, FT value)
      {
         fields.Add(name, value);
      }

      protected virtual void SetupInstance(T instance)
      {
      }

      protected bool TryGet<FT>(string name, out FT value)
      {
         if (fields.TryGetValue(name, out var untypedValue))
         {
            value = (FT)untypedValue;
            return true;
         }

         value = default(FT);
         return false;
      }

      #endregion
   }
}