using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class PositionConverter
{
    public static Vector2 ConvertWorldPosToAnchoredPos(Camera cam, Vector3 worldPos, RectTransform rectTransform, Vector2 anchor)
    {
        Vector3 viewPort = cam.WorldToViewportPoint(worldPos);
        //Vector2 size = rectTransform.sizeDelta;
        Vector2 size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        Vector2 screenPos = new Vector2(
            (viewPort.x - anchor.x) * size.x,
            (viewPort.y - anchor.y) * size.y
        );
        //Debug.Log($"ConvertWorldPosToAnchoredPos: worldPos {worldPos} | viewPort {viewPort} | size {size} | screenPos {screenPos}");
        return screenPos;
    }
}
