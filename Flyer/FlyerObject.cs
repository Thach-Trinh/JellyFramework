using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JellyFramework.FlyerSystem
{
    public class FlyerObject : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Image img;
        public RectTransform RectTransform => rectTransform;
        public Image Img => img;
        public void SetAnchoredPosition(Vector2 position) => rectTransform.anchoredPosition = position;
    }
}

