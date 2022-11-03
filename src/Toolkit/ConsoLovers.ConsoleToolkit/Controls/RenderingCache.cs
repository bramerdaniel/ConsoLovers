// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingCache.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;

internal class RenderingCache
{
   private readonly Dictionary<int, List<RenderInfo>> renderInfosByLocation = new();

   private readonly Dictionary<IRenderable, List<RenderInfo>> renderInfosByRendereable = new();
   
   private readonly Dictionary<IRenderable, RenderSize> renderSizes = new();

   public void Add(RenderInfo renderInfo)
   {
      AddForLocation(renderInfo);
      AddForRenderable(renderInfo);
   }

   private void AddForRenderable(RenderInfo renderInfo)
   {
      var renderable = renderInfo.Segment.Renderable;
      if (!renderInfosByRendereable.TryGetValue(renderable, out var lineRenderInfos))
      {
         lineRenderInfos = new List<RenderInfo>();
         renderInfosByRendereable.Add(renderable, lineRenderInfos);
      }

      lineRenderInfos.Add(renderInfo);
   }

   private void AddForLocation(RenderInfo renderInfo)
   {
      if (!renderInfosByLocation.TryGetValue(renderInfo.Line, out var lineRenderInfos))
      {
         lineRenderInfos = new List<RenderInfo>();
         renderInfosByLocation.Add(renderInfo.Line, lineRenderInfos);
      }

      lineRenderInfos.Add(renderInfo);
   }

   public IRenderable FindByLocation(int line, int column)
   {
      if (renderInfosByLocation.TryGetValue(line, out var lineInfos))
      {
         foreach (var renderInfo in lineInfos)
         {
            if (renderInfo.Column <= column && column <= renderInfo.EndColumn)
               return renderInfo.Segment.Renderable;
         }
      }

      return null;
   }

   public IEnumerable<IRenderable> GetRendered()
   {
      return renderInfosByRendereable.Keys;
   }

   public void Clear()
   {
      renderInfosByLocation.Clear();
      renderInfosByRendereable.Clear();
   }

   internal IEnumerable<RenderInfo> GetRenderInfo(IRenderable renderable)
   {
      if (renderInfosByRendereable.TryGetValue(renderable, out var renderInfos))
      {
         foreach (var renderInfo in renderInfos)
            yield return renderInfo;
      }
   }

   public void CacheSize(IRenderable renderable, RenderSize size)
   {
      renderSizes[renderable] = size;
   }

   public bool TryGetSize(IRenderable renderable, out RenderSize renderSize)
   {
      return renderSizes.TryGetValue(renderable, out renderSize);
   }
}