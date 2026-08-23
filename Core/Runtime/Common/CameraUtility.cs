using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyFramework
{
    public static class CameraUtility
    {
        public static float GetViewWidth(this Camera camera) => camera.GetViewHeight() * camera.aspect;

        public static float GetViewHeight(this Camera camera) => camera.orthographicSize * 2f;

        public static Vector2 GetViewSize(this Camera camera)
        {
            float height = camera.orthographicSize * 2f;
            float width = height * camera.aspect;
            return new Vector2(width, height);
        }

        public static Vector2 WordToAnchoredPosition(this Camera camera, Vector3 worldPosition, float width, float height)
        {
            Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
            return new Vector2((viewportPosition.x - 0.5f) * width, (viewportPosition.y - 0.5f) * height);
        }
    }

}
