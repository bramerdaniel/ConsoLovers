// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockSetupBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups.Mocks
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq.Expressions;

   using Moq;

   /// <summary>Base class for mocks</summary>
   /// <typeparam name="T"></typeparam>
   [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
   public abstract class MockSetupBase<T>
      where T : class
   {
      #region Constants and Fields

      protected Mock<T> MockInstance;

      private readonly List<MockBehaviour> behaviours = new List<MockBehaviour>();

      private readonly List<Action<T>> completionActions = new List<Action<T>>();

      private readonly Dictionary<string, MockBehaviour> fields = new Dictionary<string, MockBehaviour>();

      #endregion

      #region Public Methods and Operators

      public T Done()
      {
         MockInstance = CreateMock();
         CreateSetups(MockInstance);
         SetupMock(MockInstance);
         var result = DoneOverride(MockInstance);
         foreach (var completionAction in completionActions)
            completionAction(result);
         return result;
      }

      #endregion

      #region Methods

      protected bool Contains(string name)
      {
         return fields.ContainsKey(name);
      }

      protected virtual Mock<T> CreateMock()
      {
         return new Mock<T>();
      }

      protected virtual T DoneOverride(Mock<T> mockInstance)
      {
         return mockInstance.Object;
      }

      protected TField GetValue<TField>(string name)
      {
         var setupData = fields[name];
         return (TField)setupData.Value;
      }

      protected TField GetValue<TField>(string name, TField defaultValue)
      {
         if (fields.TryGetValue(name, out var setupData))
         {
            return (TField)setupData.Value;
         }

         return defaultValue;
      }

      protected void SetupBehaviour(Action<Mock<T>> mockSetup)
      {
         if (mockSetup == null)
            throw new ArgumentNullException(nameof(mockSetup));

         var setupData = new MockBehaviour { SetupMethodWithoutParameter = mockSetup };
         behaviours.Add(setupData);
      }

      protected void SetupBehaviour<TField>(TField value, Action<Mock<T>, TField> mockSetup)
      {
         if (mockSetup == null)
            throw new ArgumentNullException(nameof(mockSetup));

         var setupData = new MockBehaviour
         {
            Value = value, SetupMethodWithParameter = (mock, objectValue) => { mockSetup(mock, (TField)objectValue); }
         };
         behaviours.Add(setupData);
      }

      protected void SetupCompletionAction(Action<T> action)
      {
         if (action == null)
            throw new ArgumentNullException(nameof(action));
         completionActions.Add(action);
      }

      protected virtual void SetupMock(Mock<T> mockInstance)
      {
      }

      protected void SetupThrowBehaviour<TException>(Expression<Action<T>> functionThatThrows)
         where TException : Exception, new()
      {
         SetupBehaviour(cs => cs.Setup(functionThatThrows).Throws<TException>());
      }

      protected void SetValue<TField>(string name, TField value)
      {
         if (fields.ContainsKey(name))
            throw new InvalidOperationException($"The field {name} was already set");

         var data = new MockBehaviour { Value = value };
         fields.Add(name, data);
      }

      protected bool TryGet<TField>(string name, out TField value)
      {
         if (fields.TryGetValue(name, out var setupData))
         {
            value = (TField)setupData.Value;
            return true;
         }

         value = default;
         return false;
      }

      private void CreateSetups(Mock<T> mockInstance)
      {
         foreach (var mockBehaviour in behaviours)
         {
            mockBehaviour.SetupMethodWithoutParameter?.Invoke(mockInstance);
            mockBehaviour.SetupMethodWithParameter?.Invoke(mockInstance, mockBehaviour.Value);
         }
      }

      #endregion

      private class MockBehaviour
      {
         #region Public Properties

         public Action<Mock<T>> SetupMethodWithoutParameter { get; set; }

         public Action<Mock<T>, object> SetupMethodWithParameter { get; set; }

         public object Value { get; set; }

         #endregion
      }
   }
}