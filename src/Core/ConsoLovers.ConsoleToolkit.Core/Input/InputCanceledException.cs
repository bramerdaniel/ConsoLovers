// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputCanceledException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Input;

using System;

/// <summary>Exception that is thrown by the default cancellation handler of the <see cref="InputBox{T}"/></summary>
/// <seealso cref="System.Exception"/>
public class InputCanceledException : Exception
{
   #region Constructors and Destructors

   public InputCanceledException(string currentInput)
      : base("Input was canceled by user")
   {
      CurrentInput = currentInput;
   }

   #endregion

   #region Public Properties

   /// <summary>Gets the last input before the cancellation happened.</summary>
   public string CurrentInput { get; }

   #endregion
}