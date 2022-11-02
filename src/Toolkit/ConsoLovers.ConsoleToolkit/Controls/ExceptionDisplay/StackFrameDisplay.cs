// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackFrameDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;

public class StackFrameDisplay : InteractiveRenderable, IMouseInputHandler, IMouseAware, IKeyInputHandler
{
   #region Constants and Fields

   private bool isMouseOver;

   #endregion

   #region Constructors and Destructors

   public StackFrameDisplay([NotNull] StackFrame stackFrame)
   {
      if (stackFrame == null)
         throw new ArgumentNullException(nameof(stackFrame));

      FileName = stackFrame.GetFileName();
      LineNumber = stackFrame.GetFileLineNumber();
      MethodBase = stackFrame.GetMethod();
      InitializeSegments();
      RenderAllSegments();
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
         Invalidate();
      }
   }

   #endregion

   #region IMouseInputHandler Members

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (isMouseOver)
      {
         yield return new Segment(this, " → ", RenderingStyle.Default);
         foreach (var segment in RenderedSegments.Skip(1))
            yield return segment.Segment.OverrideStyle(Styles.MouseOver);
      }
      else
      {
         foreach (var segment in RenderedSegments)
            yield return segment.Segment;
      }
   }

   public void HandleMouseInput(IMouseInputContext context)
   {
      if (File.Exists(FileName))
      {
         // TODO make this customizable
         Process.Start("notepad", FileName);
      }
   }

   #endregion

   #region Public Properties

   public static IEnumerable<string> AvailableSegmentNames
   {
      get
      {
         yield return "FilePath";
         yield return "Namespace";
         yield return "TypeName";
         yield return "ParameterName";
         yield return "ParameterType";
      }
   }

   /// <summary>Gets the line number of the stack frame if available.</summary>
   public int LineNumber { get; }

   /// <summary>Gets the method the stack frame comes from.</summary>
   public MethodBase MethodBase { get; }

   public StackFrameRenderingStyles Styles { get; set; } = new();

   #endregion

   #region Properties

   private IList<NamedSegment> AvailableSegments { get; } = new List<NamedSegment>(20);

   private string FileName { get; }

   private IList<NamedSegment> RenderedSegments { get; set; }

   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(int availableWidth)
   {
      var width = RenderedSegments.Sum(x => x.Segment.Width);
      return new RenderSize { Height = 1, Width = width };
   }

   public void RemoveSegment(string segmentName)
   {
      var toRemove = RenderedSegments.Where(x => x.Name == segmentName).ToArray();
      foreach (var seg in toRemove)
         RenderedSegments.Remove(seg);
   }

   public void RenderAllSegments()
   {
      RenderedSegments = AvailableSegments.ToList();
   }

   #endregion

   #region Methods

   private void CreateFilePathAndName()
   {
      var fullName = FileName;
      if (string.IsNullOrWhiteSpace(fullName))
         return;

      CreateSegment("In", new Segment(this, " in ", Styles.NormalText));

      var directory = Path.GetDirectoryName(fullName);
      if (!string.IsNullOrWhiteSpace(directory))
         CreateSegment("FilePath", new Segment(this, directory + Path.DirectorySeparatorChar, Styles.FilePath));

      var fileName = Path.GetFileName(fullName);
      if (!string.IsNullOrWhiteSpace(fileName))
         CreateSegment("FileName", new Segment(this, fileName, Styles.FileName));

      CreateSegment("FileName", () => new Segment(this, ":", Styles.ControlCharacters));
      CreateSegment("FileName", () => new Segment(this, LineNumber.ToString(), Styles.LineNumber));
   }

   private Segment? CreateMethodName()
   {
      return new Segment(this, $"{MethodBase.Name}", Styles.MethodName);
   }

   private IEnumerable<Segment> CreateNamespace()
   {
      var typeNamespace = MethodBase.DeclaringType?.Namespace;
      if (typeNamespace != null)
      {
         yield return new Segment(this, $"{typeNamespace}", Styles.Namespaces);
         yield return new Segment(this, ".", Styles.ControlCharacters);
      }
   }

   private void CreateParameterSegment()
   {
      var parameters = MethodBase.GetParameters();
      for (var i = 0; i < parameters.Length; i++)
      {
         var parameter = parameters[i];
         CreateSegment("ParameterType", new Segment(this, parameter.ParameterType.AliasOrName(), Styles.Types));
         CreateSegment("ParameterName", new Segment(this, " ", Styles.ControlCharacters));
         CreateSegment("ParameterName", new Segment(this, parameter.Name, Styles.ParameterName));
         if (i != parameters.Length - 1)
            CreateSegment("ParameterType", new Segment(this, ", ", Styles.ControlCharacters));
      }
   }

   private void CreateSegment([NotNull] string name, [NotNull] Func<Segment?> create)
   {
      if (name == null)
         throw new ArgumentNullException(nameof(name));
      if (create == null)
         throw new ArgumentNullException(nameof(create));

      var segment = create();
      if (segment == null)
         return;

      AvailableSegments.Add(new NamedSegment { Name = name, Segment = segment.Value });
   }

   private void CreateSegment([NotNull] string name, [NotNull] Func<IEnumerable<Segment>> create)
   {
      if (name == null)
         throw new ArgumentNullException(nameof(name));
      if (create == null)
         throw new ArgumentNullException(nameof(create));

      foreach (var segment in create())
         AvailableSegments.Add(new NamedSegment { Name = name, Segment = segment });
   }

   private void CreateSegment([NotNull] string name, Segment segment)
   {
      if (name == null)
         throw new ArgumentNullException(nameof(name));

      AvailableSegments.Add(new NamedSegment { Name = name, Segment = segment });
   }

   private void InitializeSegments()
   {
      CreateSegment("Indent", new Segment(this, "   ", Styles.ControlCharacters));
      CreateSegment("At", new Segment(this, "at ", Styles.NormalText));
      if (MethodBase is MethodInfo methodInfo)
      {
         CreateSegment("ReturnType", new Segment(this, $"{methodInfo.ReturnType.AliasOrName()} ", Styles.Types));
         CreateSegment("Namespace", CreateNamespace);
         CreateSegment("TypeName", new Segment(this, $"{methodInfo.DeclaringType.AliasOrName()}.", Styles.Namespaces));
      }

      if (MethodBase is ConstructorInfo constructorInfo)
      {
         CreateSegment("Namespace", CreateNamespace);
         CreateSegment("$TypeName$", new Segment(this, $"{constructorInfo.DeclaringType.AliasOrName()}", Styles.Types));
      }

      CreateSegment("MethodName", CreateMethodName);
      CreateSegment("(", new Segment(this, "(", Styles.NormalText));
      CreateParameterSegment();
      CreateSegment(")", new Segment(this, ")", Styles.NormalText));

      CreateFilePathAndName();
   }

   #endregion

   public void HandleKeyInput(IKeyInputContext context)
   {
      var key = context.KeyEventArgs.Key;
      if (key == ConsoleKey.Enter || key == ConsoleKey.Escape)
         context.Accept();
   }
}