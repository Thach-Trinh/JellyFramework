using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyFramework
{
    public static class MathUtility
    {
        public static float Remap(float value, float inMin, float inMax, float outMin, float outMax)
            => outMin + (value - inMin) * (outMax - outMin) / (inMax - inMin);

        public static Vector3 Bezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }
    }
}

