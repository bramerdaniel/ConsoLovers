// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Link.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

using JetBrains.Annotations;

public class Link : InteractiveRenderable, IMouseAware, IMouseInputHandler, IKeyInputHandler
{
   #region Constants and Fields

   private string displayText;

   private bool isMouseOver;

   private Action<string> linkResolver = ResolveLink;

   #endregion

   #region Constructors and Destructors

   public Link([NotNull] string address)
   {
      Address = address ?? throw new ArgumentNullException(nameof(address));
   }

   #endregion

   #region IKeyInputHandler Members

   public void HandleKeyInput(IKeyInputContext context)
   {
      var key = context.KeyEventArgs.Key;
      if (key == ConsoleKey.Enter || key == ConsoleKey.Escape)
         context.Accept();
   }

   #endregion

   #region IMouseAware Members

   /// <summary>Gets or sets a value indicating whether this instance is mouse over.</summary>
   bool IMouseAware.IsMouseOver
   {
      get => isMouseOver;
      set
      {
         if (isMouseOver == value)
            return;

         isMouseOver = value;
         NotifyStyleChanged();
      }
   }

   #endregion

   #region IMouseInputHandler Members

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line > 0)
         throw new ArgumentOutOfRangeException(nameof(line), "Links can only render single line text");

      yield return new Segment(this, DisplayText, isMouseOver ? MouseOverStyle : Style);
   }

   public void HandleMouseInput(IMouseInputContext context)
   {
      LinkResolver.Invoke(Address);
   }

   #endregion

   #region Public Properties

   public string Address { get; set; }

   public string DisplayText
   {
      get => displayText ?? Address;
      set
      {
         if(displayText == value)
            return;

         displayText = value;
         Invalidate(InvalidationScope.All);
      }
   }

   public Action<string> LinkResolver
   {
      get => linkResolver ?? ResolveLink;
      set => linkResolver = value;
   }

   public RenderingStyle MouseOverStyle { get; set; } = DefaultStyles.ActiveLinkStyle;

   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      return new RenderSize { Height = 1, Width = DisplayText.Length };
   }

   #endregion

   #region Methods

   private static void OpenBrowser(string address)
   {
      // I found this solution here
      // https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/

      try
      {
         Process.Start(address);
      }
      catch
      {
         // hack because of this: https://github.com/dotnet/corefx/issues/10361
         if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
         {
            address = address.Replace("&", "^&");
            Process.Start(new ProcessStartInfo("cmd", $"/c start {address}") { CreateNoWindow = true });
         }
         else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
         {
            Process.Start("xdg-open", address);
         }
         else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
         {
            Process.Start("open", address);
         }
         else
         {
            throw;
         }
      }
   }

   private static void ResolveLink(string address)
   {
      OpenBrowser(address);
   }

   #endregion
}