// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MessageDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

public class MessageDisplay : InteractiveRenderable
{
   #region Constants and Fields

   private readonly string message;

   private readonly string title;

   private IList<string> messageLines;

   #endregion

   #region Constructors and Destructors

   public MessageDisplay([NotNull] string title, [NotNull] string message)
   {
      this.title = title ?? throw new ArgumentNullException(nameof(title));
      this.message = message ?? throw new ArgumentNullException(nameof(message));
   }

   #endregion

   #region Public Properties

   public RenderingStyle MessageStyle { get; set; } = DefaultStyles.ErrorMessageStyle;

   /// <summary>Gets or sets the separator between title and message.</summary>
   public string Separator { get; set; } = ": ";

   public RenderingStyle SeparatorStyle { get; set; } = DefaultStyles.ControlCharacterStyle;

   #endregion

   #region Public Methods and Operators

   public override IEnumerable<IRenderable> GetChildren()
   {
      yield break;
   }

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      var separatorLength = Separator?.Length ?? 0;
      var messageLength = availableWidth - title.Length - separatorLength;
      messageLines = message.Wrap(messageLength);
      int width = title.Length + separatorLength + messageLines.Max(x => x.Length);

      return new RenderSize
      {
         Height = messageLines.Count, 
         Width = width
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == 0)
      {
         yield return new Segment(this, title, Style);
         yield return new Segment(this, Separator, SeparatorStyle);
         yield return new Segment(this, messageLines[0], MessageStyle);
      }
      else if (line < messageLines.Count)
      {
         yield return new Segment(this, string.Empty.PadRight(title.Length + Separator.Length), Style);
         yield return new Segment(this, messageLines[line], MessageStyle);
      }
      else
      {
         throw new ArgumentOutOfRangeException(nameof(line), $"Can only render lines between {0} and {messageLines.Count - 1}");
      }
   }

   #endregion
}