// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FrameParts.cs" company="ConsoLovers">
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

[Obsolete]
public class FrameParts
{
   #region Constants and Fields

   private readonly MethodBase methodBase;

   private readonly IRenderable owner;

   private readonly StackFrame stackFrame;

   #endregion

   #region Constructors and Destructors

   public FrameParts([NotNull] StackFrame stackFrame, [NotNull] IRenderable owner)
   {
      this.stackFrame = stackFrame ?? throw new ArgumentNullException(nameof(stackFrame));
      this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
      methodBase = stackFrame.GetMethod();
      
      CreateSegment("Indent", new Segment(owner, "   ", Styles.ControlCharacters));
      CreateSegment("At", new Segment(owner, "at ", Styles.NormalText));
      CreateSegment("ReturnType", CreateReturnType);
      CreateSegment("Namespace", CreateNamespace);
      //CreateSegment("Class", CreateClass);
      CreateSegment("MethodName", CreateMethodName);
      CreateSegment("(", new Segment(owner, "(", Styles.NormalText));
      CreateParameterSegment();
      CreateSegment(")", new Segment(owner, ")", Styles.NormalText));
      CreateSegment("In", new Segment(owner, " in ", Styles.NormalText));
      CreateFilePathAndName();

      Reset();
   }

   #endregion

   #region Public Properties

   public IEnumerable<Segment> SegmentsToRender => RenderedSegments.Select(x => x.Segment);

   public StackFrameRenderingStyles Styles { get; set; } = new();

   #endregion

   #region Properties

   private IList<NamedSegment> Segments { get; } = new List<NamedSegment>();

   private IList<NamedSegment> RenderedSegments { get; set; }

   #endregion

   #region Public Methods and Operators

   public RenderSize Measure()
   {
      var width = RenderedSegments.Sum(x => x.Segment.Width);
      return new RenderSize { Height = 1, Width = width };
   }

   public void RemovePart(string segmentName)
   {
      Remove(RenderedSegments, segmentName);
   }

   public void Reset()
   {
      RenderedSegments = Segments.ToList();
   }

   #endregion

   #region Methods

   private Segment? CreateClass()
   {
      if (methodBase is MethodInfo methodInfo)
      {
         var declaringType = methodInfo.DeclaringType;
         if (declaringType != null)
            return new Segment(owner, $"{declaringType.Name}.", owner.Style);
      }

      return null;
   }

   private void CreateFilePathAndName()
   {
      var fullName = stackFrame.GetFileName();
      if (string.IsNullOrWhiteSpace(fullName))
         return;

      var directory = Path.GetDirectoryName(fullName);
      if (directory != null)
         CreateSegment("FilePath", new Segment(owner, directory + Path.DirectorySeparatorChar, Styles.FilePath));

      var fileName = Path.GetFileName(fullName);
      if (!string.IsNullOrWhiteSpace(fileName))
         CreateSegment("FileName", new Segment(owner, fileName, Styles.FileName));

      var lineNumber = stackFrame.GetFileLineNumber();
      CreateSegment("FileName", () => new Segment(owner, ":", Styles.ControlCharacters));
      CreateSegment("FileName", () => new Segment(owner, lineNumber.ToString(), Styles.LineNumber));
   }

   private Segment? CreateMethodName()
   {
      return new Segment(owner, $"{methodBase.Name}", Styles.MethodName);
   }

   private Segment? CreateNamespace()
   {
      var typeNamespace = methodBase.DeclaringType?.Namespace;
      if (typeNamespace != null)
         return new Segment(owner, $"{typeNamespace}.", owner.Style);
      return null;
   }

   private void CreateParameterSegment()
   {
      var parameters = methodBase.GetParameters();
      for (var i = 0; i < parameters.Length; i++)
      {
         var parameter = parameters[i];
         CreateSegment("ParameterType", () => new Segment(owner, parameter.ParameterType.AliasOrName(), Styles.Types));
         CreateSegment("ParameterName", () => new Segment(owner, " ", Styles.ControlCharacters));
         CreateSegment("ParameterName", () => new Segment(owner, parameter.Name, Styles.ParameterName));
         if (i != parameters.Length - 1)
            CreateSegment("ParameterType", () => new Segment(owner, ", ", Styles.ControlCharacters));
      }
   }

   private Segment? CreateReturnType()
   {
      if (methodBase is MethodInfo methodInfo)
         return new Segment(owner, $"{methodInfo.ReturnType.AliasOrName()} ", owner.Style.WithForeground(ConsoleColor.DarkCyan));
      if (methodBase is ConstructorInfo ctorInfo)
         return new Segment(owner, $"{ctorInfo.DeclaringType.AliasOrName()}", owner.Style.WithForeground(ConsoleColor.DarkCyan));
      return null;
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

      Segments.Add(new NamedSegment { Name = name, Segment = segment.Value });
   }

   private void CreateSegment([NotNull] string name, Segment segment)
   {
      if (name == null)
         throw new ArgumentNullException(nameof(name));

      Segments.Add(new NamedSegment { Name = name, Segment = segment });
   }

   private void Remove(IList<NamedSegment> namedSegments, string nameToRemove)
   {
      var toRemove = namedSegments.Where(x => x.Name == nameToRemove).ToArray();
      foreach (var seg in toRemove)
         namedSegments.Remove(seg);
   }

   #endregion


}