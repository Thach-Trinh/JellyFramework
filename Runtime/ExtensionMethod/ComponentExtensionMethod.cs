using UnityEngine;
using UnityEngine.UI;

public static class ComponentExtensionMethod
{
    public static void SetAlpha(this Graphic graphic, float alpha)
    {
        Color color = graphic.color;
        color.a = alpha;
        graphic.color = color;
    }

    //public static float GetDistanceOnXYPlane(this Vector3 vector)
    //{
    //    return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
    //}
}
