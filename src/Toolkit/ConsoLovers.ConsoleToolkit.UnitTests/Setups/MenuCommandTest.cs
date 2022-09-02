// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuCommandTest.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using System;
using System.Linq;
using System.Reflection;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

internal class MenuCommandTest<TArgs>
   where TArgs : class
{
   #region Constants and Fields

   private readonly ArgumentInitializationModes argumentInitialization;

   private readonly IInputReader inputReader;

   #endregion

   #region Constructors and Destructors

   public MenuCommandTest(ArgumentInitializationModes argumentInitialization, IInputReader inputReader)
   {
      this.argumentInitialization = argumentInitialization;
      this.inputReader = inputReader;
      InitializeTest();
   }

   #endregion

   #region Public Properties

   public MenuCommandManager CommandManager { get; private set; }

   public IServiceProvider ServiceProvider { get; private set; }

   #endregion

   #region Public Methods and Operators

   public MenuExecutionContext Execute(string command)
   {
      var commandNode = FindCommandNode(command);
      return CommandManager.ExecuteNode(commandNode, null);
   }

   internal TCommand ExecuteCommand<TCommand>(string command)
   where TCommand : ICommandBase
   {
      var context = Execute(command);
      return (TCommand)context.Command;
   }

   public MenuExecutionContext Initialize(string command)
   {
      var commandNode = FindCommandNode(command);
      return CommandManager.InitializeArguments(commandNode, null);
   }

   public T InitializeArguments<T>(string command)
   {
      var commandNode = FindCommandNode(command);
      var context = CommandManager.InitializeArguments(commandNode, null);

      var propertyInfo = context.Command.GetType().GetProperty(nameof(ICommandArguments<Type>.Arguments));
      return (T)propertyInfo?.GetValue(context.Command);
   }

   #endregion

   #region Methods

   private ICommandNode FindCommandNode(string command)
   {
      var nodes = CommandManager.CreateNodes<TArgs>().ToArray();
      var commandNode = FindCommandNode(nodes, command);
      return commandNode;
   }

   private ICommandNode FindCommandNode(IMenuNode[] nodes, string command)
   {
      foreach (var node in nodes.OfType<ICommandNode>())
      {
         if (node.DisplayName == command)
            return node;
         if (node.PropertyInfo.Name == command)
            return node;
      }

      return null;
   }

   private void InitializeTest()
   {
      var builder = ConsoleApplication.WithArguments<TArgs>()
         .AddService(s => s.AddSingleton(inputReader))
         .UseMenuWithoutArguments(o => { o.BuilderOptions.ArgumentInitializationMode = argumentInitialization; });

      ServiceProvider = builder.GetOrCreateServiceProvider();

      CommandManager = (MenuCommandManager)ServiceProvider.GetService(typeof(IMenuCommandManager));
      Assert.IsNotNull(CommandManager);
   }

   #endregion
}

internal static class Helpers
{
   #region Methods

   internal static IServiceProvider GetOrCreateServiceProvider<T>(this IApplicationBuilder<T> builder)
      where T : class
   {
      var serviceProviderMethod = builder.GetType().GetMethod("GetOrCreateServiceProvider", BindingFlags.Instance | BindingFlags.NonPublic);
      if (serviceProviderMethod != null)
         return (IServiceProvider)serviceProviderMethod.Invoke(builder, null);

      throw new AssertFailedException($"Could not resolve the GetOrCreateServiceProvider method");
   }

   #endregion
}