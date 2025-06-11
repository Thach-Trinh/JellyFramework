using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace JellyFramework
{
    public static class PositionConverter
    {
        public static Vector2 FromWorldToCanvas(Vector3 worldPos, Camera cam, Rect rect, Vector2 anchor)
        {
            Vector3 viewport = cam.WorldToViewportPoint(worldPos);
            return FromViewportToCanvas(viewport, rect, anchor);
        }

        public static Vector2 FromScreenToCanvas(Vector3 screenPos, Camera cam, Rect rect, Vector2 anchor)
        {
            Vector3 viewport = cam.ScreenToViewportPoint(screenPos);
            return FromViewportToCanvas(viewport, rect, anchor);
        }

        private static Vector2 FromViewportToCanvas(Vector3 viewport, Rect rect, Vector2 anchor)
            => new Vector2((viewport.x - anchor.x) * rect.width, (viewport.y - anchor.y) * rect.height);
    }
}
